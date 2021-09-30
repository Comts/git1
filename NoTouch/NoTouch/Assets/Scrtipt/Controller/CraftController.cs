using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftController : InformationLoader
{
    public static CraftController Instance;
    [SerializeField]
    private CraftInfo[] mInfoArr;
    [SerializeField]
    private CraftTextInfo[] mTextInfoArr;
    private Sprite[] mIconArr;
    private double CraftLastProgress;
    private double CurrentProgress;
    private double mTouchPower;
    private int GemID,GemGrade;
#pragma warning disable 0649
    [SerializeField]
    private CraftUIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;

    [SerializeField]
    private float mCraftTime;
    private List<CraftUIElement> mElementList;

    [SerializeField]
    private GaugeBar mGaugeBar;
    [SerializeField]
    private Image mPopWindow;
    [SerializeField]
    private Transform mGemArea;
    [SerializeField]
    private GemPool mGemPool;
    [SerializeField]
    private Image mButtonBlock;
    [SerializeField]
    private Text TimeText;
#pragma warning restore 0649
    [SerializeField]
    private Gem mCurrentGem;
    private Coroutine mCraftCountDown;
    private IEnumerator CraftCountDown(float time)
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        while (time > 0)
        {
            yield return frame;
            time -= Time.fixedDeltaTime;
            int sec = (int)(time);
            TimeText.text = sec.ToString();
        }
        CraftFinish();
    }

    public void CraftFinish()
    {
        switch (GemGrade)
        {
            case 0:
                GameController.Instance.AddAmoutGem_O[GemID]+=1000;
                break;
            case 1:
                GameController.Instance.AddAmoutGem_B[GemID]++;

                if (GameController.Instance.Achive_Ame == 0)
                {
                    QuestController.Instance.Achive_Ame();
                }
                break;
            case 2:
                GameController.Instance.AddAmoutGem_A[GemID]++;
                break;
            case 3:
                GameController.Instance.AddAmoutGem_S[GemID]++;
                
                if (GameController.Instance.Achive_Coal == 0)
                {
                    QuestController.Instance.Achive_Coal();
                }
                if (GameController.Instance.Achive_Gold == 0)
                {
                    QuestController.Instance.Achive_Gold();
                }
                if (GameController.Instance.Achive_Dia == 0)
                {
                    QuestController.Instance.Achive_Dia();
                }
                if (GameController.Instance.Achive_Vib == 0)
                {
                    QuestController.Instance.Achive_Vib();
                }
                break;
            default:
                Debug.LogError("GemGrade Error " + GemGrade);
                break;
        }
        mPopWindow.gameObject.SetActive(false);
        if (mCurrentGem != null)
        {
            mCurrentGem.gameObject.SetActive(false);
        }
        mCurrentGem = null;
        UIController.Instance.Popwindow(3);
        if (mCraftCountDown != null)
        {
            StopCoroutine(mCraftCountDown);
        }
        mCraftCountDown = null;
        mButtonBlock.gameObject.SetActive(false);

        Quest_CraftGem.Instance.CheckQuest();
        GemSellController.Instance.RefreshGemData();
    }

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
    void Start()
    {

        LoadJson(out mInfoArr, Paths.CRAFT_INFO_TABLE);
        LoadJson(out mTextInfoArr,
            Paths.CRAFT_TEXT_INFO_TABLE +
            Paths.LANGUAGE_TYPE_ARR[GameController.Instance.LanguageType]);

        mIconArr = Resources.LoadAll<Sprite>(Paths.CRAFT_ICON);

        mElementList = new List<CraftUIElement>();
        mCraftTime = Constants.CRAFT_TIME;
        Load();
    }
    public void CheckCraftButton()
    {
        for (int i = 0; i < mInfoArr.Length; i++) 
        { 
            if (GameController.Instance.AddAmoutGem_O[i] < 1000)
            {
                mElementList[i].SetCraftButtonActive(false);
            }
            else
            {
                mElementList[i].SetCraftButtonActive(true);
            }
        }
    }
    private void Load()
    {
        for (int i = 0; i < mInfoArr.Length; i++)
        {



            CraftUIElement element = Instantiate(mElementPrefab, mElementArea);
            element.Init(i, mIconArr[i],
                        mTextInfoArr[i].Title,
                        mTextInfoArr[i].ContentsFormat,
                        //string.Format("{0}EA", GameController.Instance.AddAmoutGem_A[i]),
                        StartCraft);


            mElementList.Add(element);
            if (GameController.Instance.AddAmoutGem_O[i] < 1000)
            {
                mElementList[i].SetCraftButtonActive(false);
            }
            if (i <= GameController.Instance.Stage)
            {
                mElementList[i].gameObject.SetActive(true);
            }
        }
        CheckCraftButton();
    }
    public void ReStart()
    {

        for (int i = 0; i < mInfoArr.Length; i++)
        {
            mElementList[i].gameObject.SetActive(false);
        }
        mElementList[0].gameObject.SetActive(true);
        CheckCraftButton();
    }
    public void SetCraft(int num)
    {
        mElementList[num].gameObject.SetActive(true);
    }
    public void StartCraft(int id, int amount)
    {
        GameController.Instance.AddAmoutGem_O[id] -= amount;
        CurrentProgress = 0;
        mCraftCountDown = StartCoroutine(CraftCountDown(mCraftTime));
        GemID = id;
        GemGrade = 0;
        mCurrentGem = mGemPool.GetFromPool(GemID);
        CraftLastProgress = mCurrentGem.SetShiftGap(id);
        mTouchPower = GameController.Instance.ManPower;
        ShowGaugeBar(CurrentProgress, CraftLastProgress);
        mButtonBlock.gameObject.SetActive(true);
    }
    public void ShowGaugeBar(double current, double max)
    {
        string progressStr = string.Format("{0} / {1}",
                                            UnitSetter.GetUnitStr(current),
                                            UnitSetter.GetUnitStr(max));
        float progress = (float)(current / max);
        mGaugeBar.ShowGaugeBar(progress, progressStr);
    }
    public void Touch()
    {
        if(CurrentProgress >= CraftLastProgress)
        {
            CraftFinish();
        }
        else
        {
            CurrentProgress += (mTouchPower * ItemUseController.Instance.GetGemMulti[0]);
            if (CurrentProgress > CraftLastProgress)
            {
                CurrentProgress = CraftLastProgress;
            }
            GemGrade = mCurrentGem.SetProgress(CurrentProgress);
        }
        ShowGaugeBar(CurrentProgress, CraftLastProgress);
    }
}
