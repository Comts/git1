using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoworkerController : InformationLoader
{
    public static CoworkerController Instance;


    private CoworkerInfo[] mInfoArr;
    private CoworkerTextInfo[] mTextInfoArr;

    private int[] mLevelArr;

    //textdata class
#pragma warning disable 0649
    [SerializeField]
    private int mCoworkerWork;
    [SerializeField]
    private Sprite[] mIconArr;

    [SerializeField]
    private Coworker[] mCoworkerArr;

    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private SleepUIElement mSleepElementPrefab;
    [SerializeField]
    private Transform mElementArea,mSleepElementArea;
    [SerializeField]
    private int mTimePeriod;
    [SerializeField]
    private int mCoworkerBuffLevel;
    [SerializeField]
    private Text BuffText;
    [SerializeField]
    private BuffUIElement[] mBuffUIElement;
    [SerializeField]
    private BuffUIElement mDoubleBuffUIElement;
    [SerializeField]
    private Sprite mBlockIcon,mDoubleBuffIcon;
    [SerializeField]
    private Image mBuffWindow;
#pragma warning restore 0649
    private List<UIElement> mElementList;
    private List<SleepUIElement> mSleepElementList;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadJson(out mInfoArr, Paths.COWORKER_INFO_TABLE);
        LoadJson(out mTextInfoArr,
            Paths.COWORKER_TEXT_INFO_TABLE +
            Paths.LANGUAGE_TYPE_ARR[GameController.Instance.LanguageType]);


        mLevelArr = GameController.Instance.GetCoworkerLevelArr();
        mElementList = new List<UIElement>();
        mSleepElementList = new List<SleepUIElement>();
        Load();
    }
    public void ReStart()
    {

        mLevelArr = GameController.Instance.GetCoworkerLevelArr();
        for (int i = 0; i < mElementList.Count; i++)
        {
            mCoworkerArr[i].StopWork();
            Destroy(mElementList[i].gameObject);
        }

        mElementList.Clear();

        Load();
    }
    private void Load()
    {
        GameController.Instance.Buff_Coworker = 0;
        GameController.Instance.BuffCoworker_Double = 0;
        mDoubleBuffUIElement.Init(mBlockIcon, "A to Z", "버프 2배");
        for (int i = 0; i < mInfoArr.Length; i++)
        {
            mBuffUIElement[i].Init(mBlockIcon, mTextInfoArr[i].Title, string.Format(100 * mInfoArr[i].BuffAmount + "%"));
            if (mLevelArr[i] < 0)
            { continue; }
            mInfoArr[i].CurrentLevel = mLevelArr[i];
            CalcData(i);
            if (mInfoArr[i].CurrentLevel > 0)
            {
                mCoworkerArr[i].gameObject.SetActive(true);
                mCoworkerArr[i].StartWork(i, mInfoArr[i].PeriodCurrent);

                if (i == mInfoArr.Length-1)
                {
                    GameController.Instance.BuffCoworker_Double = 1;
                    mDoubleBuffUIElement.ShowBuff(mDoubleBuffIcon);
                }
                if (mInfoArr[i].CurrentLevel >= mCoworkerBuffLevel)
                {
                    GameController.Instance.Buff_Coworker += mInfoArr[i].BuffAmount;
                    mBuffUIElement[i].ShowBuff(mIconArr[i]);
                }
            }
            UIElement element = Instantiate(mElementPrefab, mElementArea);
            element.Init(i, mIconArr[i],
                        mTextInfoArr[i].Title,
                        string.Format("레벨 : {0}", mInfoArr[i].CurrentLevel.ToString()),
                        string.Format(mTextInfoArr[i].ContentsFormat,
                                    mInfoArr[i].PeriodCurrent,
                                    UnitSetter.GetUnitStr(mInfoArr[i].ValueCurrent)),
                        UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent),
                        LevelUP);


            mElementList.Add(element);
            if (mInfoArr[i].CurrentLevel >= mInfoArr[i].MaxLevel)
            {
                mElementList[i].SetButtonActive(false);
            }
        }
        ShowCoworkerBuff();
    }

    public void ShowCoworkerBuff()
    {
        GameController.Instance.ManPower = GameController.Instance.CalBuffManPower();
        string BuffStr = string.Format("+ {0} %", GameController.Instance.Buff_Coworker * 100 * (1+GameController.Instance.BuffCoworker_Double));

        BuffText.text = BuffStr;
    }

    public void JobFinish(int id)//TODO FX, Vector3 effectPos)
    {
        double AddAmount = mInfoArr[id].ValueCurrent;
        GameController.Instance.AddAmoutGem_O[id]+= (AddAmount * ItemUseController.Instance.GetGemMulti[1]);
        GemSellController.Instance.RefreshGemData();
        //TODO FX
    }

    public void SleepJob(int num)
    {
        for (int i = 0; i < mInfoArr.Length; i++)
        {
            if (mCoworkerArr[i].gameObject.activeInHierarchy)
            {
                double AddAmount = mInfoArr[i].ValueCurrent;
                GameController.Instance.AddAmoutGem_O[i] += AddAmount * (int)((GameController.Instance.TimeLag / (mInfoArr[i].PeriodCurrent * mTimePeriod)))* num;
                GemSellController.Instance.RefreshGemData();

                SleepUIElement Sleepelement = Instantiate(mSleepElementPrefab, mSleepElementArea);
                Sleepelement.Init(mIconArr[i], mTextInfoArr[i].Title, mTextInfoArr[i].GemName, UnitSetter.GetUnitStr(AddAmount * (int)((GameController.Instance.TimeLag / (mInfoArr[i].PeriodCurrent * mTimePeriod))) * num));


                mSleepElementList.Add(Sleepelement);
            }
        }
    }
    public bool CheckSleepJob()
    {
        bool check = false;
        for (int i = 0; i < mInfoArr.Length; i++)
        {
            if (mCoworkerArr[i].gameObject.activeInHierarchy)
            {
                if ((int)((GameController.Instance.TimeLag / (mInfoArr[i].PeriodCurrent * mTimePeriod))) >= 1)
                {
                    check = true;
                }
            }
        }
        return check;
    }


    public void LevelUP(int id, int amount)
    {
        Delegates.VoidCallback callback = () => { LevelUpCallback(id, amount); };
        GameController.Instance.GoldCallback = callback;
        double cost = mInfoArr[id].CostCurrent;
        GameController.Instance.Gold -= cost;
    }
    public void LevelUpCallback(int id, int level)
    {
        mInfoArr[id].CurrentLevel += level;
        if (mInfoArr[id].CurrentLevel == mInfoArr[id].MaxLevel)
        {
            mElementList[id].SetButtonActive(false);
        }
        mLevelArr[id] = mInfoArr[id].CurrentLevel;

        if (mInfoArr[id].CurrentLevel == 1)
        {
            mCoworkerArr[id].gameObject.SetActive(true);
            StageController.Instance.CoworkerActive(id);
            if (id == mInfoArr.Length-1)
            {
                GameController.Instance.BuffCoworker_Double = 1;
                mDoubleBuffUIElement.ShowBuff(mDoubleBuffIcon);
                ShowCoworkerBuff();
                PlayerUpgradeController.Instance.ReSetSlider();
            }
        }

        if (mInfoArr[id].CurrentLevel == mCoworkerBuffLevel)
        {
            GameController.Instance.Buff_Coworker += mInfoArr[id].BuffAmount;
            mBuffUIElement[id].ShowBuff(mIconArr[id]);
            ShowCoworkerBuff();
            PlayerUpgradeController.Instance.ReSetSlider();
            SoundController.Instance.FXSound(12);
            PointController.Instance.ShowPlayerPoint(true);
            mBuffWindow.gameObject.SetActive(true);
        }
        AddCowerker(id);

        CalcData(id);

        mCoworkerArr[id].StartWork(id, mInfoArr[id].PeriodCurrent);

        mElementList[id].Refresh(
                                    string.Format("레벨 : {0}", mInfoArr[id].CurrentLevel.ToString()),
                                    string.Format(mTextInfoArr[id].ContentsFormat,
                                    mInfoArr[id].PeriodCurrent,
                                    UnitSetter.GetUnitStr(mInfoArr[id].ValueCurrent)),
                      UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent));
        Quest_CowerkorLevelSum.Instance.CheckQuest();
    }

    public void AddCowerker(int id)
    {
        if (mInfoArr[id].CurrentLevel >= 10 && GameController.Instance.Stage > id && id < mInfoArr.Length)
        {
            int nextID = id + 1;

            if (mElementList.Count <= nextID)
            {
                mLevelArr[nextID] = mInfoArr[nextID].CurrentLevel = 0;
                CalcData(nextID);

                UIElement element = Instantiate(mElementPrefab, mElementArea);

                element.Init(nextID, mIconArr[nextID],
                            mTextInfoArr[nextID].Title,
                            string.Format("레벨 : {0}", mInfoArr[nextID].CurrentLevel.ToString()),
                            string.Format(mTextInfoArr[nextID].ContentsFormat,
                                    mInfoArr[nextID].PeriodCurrent,
                                        UnitSetter.GetUnitStr(mInfoArr[nextID].ValueCurrent)),
                            UnitSetter.GetUnitStr(mInfoArr[nextID].CostCurrent),
                            LevelUP);

                mElementList.Add(element);
                PointController.Instance.ShowCoworkerPoint(true);
            }
        }
    }

    private void CalcData(int id)
    {
        for(int i=0;i<=mInfoArr[id].CurrentLevel;i++)
        {
            if (i == 0)
            {
                mInfoArr[id].CostCurrent = Math.Round(mInfoArr[id].CostBase *
                                        Math.Pow((1 + (mInfoArr[id].CostWeight * i)), 2),0);
            }
            else
            {
                mInfoArr[id].CostCurrent = Math.Round(mInfoArr[id].CostCurrent *
                                        Math.Pow((1 + (mInfoArr[id].CostWeight * i)), 2),0);
            }
            //Debug.Log(i + "번째 금액" + mInfoArr[id].CostCurrent);
        }

        //Debug.Log(id + "번째" + mInfoArr[id].CostCurrent);
        //Debug.Log(id + "번째 costbase" + mInfoArr[id].CostBase);
        //Debug.Log(id + "번째 costweight" + mInfoArr[id].CostWeight);
        //Debug.Log(id + "번째 CurrentLevel" + mInfoArr[id].CurrentLevel);
        mInfoArr[id].ValueCurrent = mInfoArr[id].CurrentLevel * mInfoArr[id].ValueWeight;
        float periodsSub = mInfoArr[id].PeriodUpgradeAmount *
                           (int)(mInfoArr[id].CurrentLevel / mInfoArr[id].PeriodLevelStep);
        if (mInfoArr[id].CurrentLevel >= 0)
        {
            mInfoArr[id].PeriodCurrent = mInfoArr[id].PeriodBase - periodsSub;
        }
    }
}
