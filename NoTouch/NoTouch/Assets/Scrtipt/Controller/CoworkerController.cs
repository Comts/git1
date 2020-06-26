using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoworkerController : InformationLoader
{
    public static CoworkerController Instance;


    private CoworkerInfo[] mInfoArr;
    private CoworkerTextInfo[] mTextInfoArr;

    private int[] mLevelArr;

    //textdata class
#pragma warning disable 0649
    [SerializeField]
    private int mCoworkerWork=2;
    [SerializeField]
    private Sprite[] mIconArr;

    [SerializeField]
    private Coworker[] mCoworkerArr;

    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;
#pragma warning restore 0649
    private List<UIElement> mElementList;
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
    private void Load()
    {

        for (int i = 0; i < mInfoArr.Length; i++)
        {
            if (mLevelArr[i] < 0)
            { continue; }
            mInfoArr[i].CurrentLevel = mLevelArr[i];
            CalcData(i);
            if (mInfoArr[i].CurrentLevel > 0)
            {
                mCoworkerArr[i].gameObject.SetActive(true);
                mCoworkerArr[i].StartWork(i, mInfoArr[i].PeriodCurrent);
            }
            UIElement element = Instantiate(mElementPrefab, mElementArea);
            element.Init(i, mIconArr[i],
                        mTextInfoArr[i].Title,
                        mInfoArr[i].CurrentLevel.ToString(),
                        string.Format(mTextInfoArr[i].ContentsFormat,
                                    UnitSetter.GetUnitStr(mInfoArr[i].ValueCurrent)),
                        UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent),
                        LevelUP);


            mElementList.Add(element);
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
        Load();
    }

    public void JobFinish(int id)//TODO FX, Vector3 effectPos)
    {
        double AddAmount = mInfoArr[id].CurrentLevel * mCoworkerWork;
        GameController.Instance.AddAmoutGem_O[id]+= (AddAmount * ItemUseController.Instance.GetGemMulti[1]);
        //TODO FX
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
        if(mInfoArr[id].CurrentLevel == 1)
        {
            mCoworkerArr[id].gameObject.SetActive(true);

        }
        if (mInfoArr[id].CurrentLevel >= 10 && GameController.Instance.Stage > id && id<mInfoArr.Length)
        {
            int nextID = id + 1;

            if (mElementList.Count <= nextID)
            {
                mLevelArr[nextID] = mInfoArr[nextID].CurrentLevel = 0;
                CalcData(nextID);

                UIElement element = Instantiate(mElementPrefab, mElementArea);

                element.Init(nextID, mIconArr[nextID],
                            mTextInfoArr[nextID].Title,
                            mInfoArr[nextID].CurrentLevel.ToString(),
                            string.Format(mTextInfoArr[nextID].ContentsFormat,
                                        UnitSetter.GetUnitStr(mInfoArr[nextID].ValueCurrent)),
                            UnitSetter.GetUnitStr(mInfoArr[nextID].CostCurrent),
                            LevelUP);

                mElementList.Add(element);
            }
        }


        CalcData(id);

        mCoworkerArr[id].StartWork(id, mInfoArr[id].PeriodCurrent);

        mElementList[id].Refresh(mInfoArr[id].CurrentLevel.ToString(),
                      string.Format(mTextInfoArr[id].ContentsFormat,
                                    UnitSetter.GetUnitStr(mInfoArr[id].ValueCurrent)),
                      UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent));
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
        mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase *
                            Math.Pow(mInfoArr[id].ValueWeight, mInfoArr[id].CurrentLevel);
        float periodsSub = mInfoArr[id].PeriodUpgradeAmount *
                           (int)(mInfoArr[id].CurrentLevel / mInfoArr[id].PeriodLevelStep);
        if (mInfoArr[id].CurrentLevel > 0)
        {
            mInfoArr[id].PeriodCurrent = mInfoArr[id].PeriodBase - periodsSub;
        }
    }
}
