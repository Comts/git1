using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
#pragma warning restore 0649
    private Coroutine mCraftCountDown;
    private IEnumerator CraftCountDown(float time)
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        while (time > 0)
        {
            yield return frame;
            time -= Time.fixedDeltaTime;
        }
        switch (GemGrade)    
        {
            case 0:
                GameController.Instance.AddAmoutGem_A[GemID]++;
                break;
            case 1:
                GameController.Instance.AddAmoutGem_S[GemID]++;
                break;
            case 2:
                GameController.Instance.AddAmoutGem_SS[GemID]++;
                break;
            case 3:
                GameController.Instance.AddAmoutGem_SSS[GemID]++;
                break;
            default:
                Debug.LogError("GemGrade Error " + GemGrade);
                break;
        }
        UIController.Instance.Popwindow(3);
        mCraftCountDown = null;
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
        mTouchPower = GameController.Instance.ManPower;
        mCraftTime = 60;
        Load();
    }
    private void Update()
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
                        "1000EA",
                        StartCraft);


            mElementList.Add(element);
            if (GameController.Instance.AddAmoutGem_O[i] < 1000)
            {
                mElementList[i].SetCraftButtonActive(false);
            }
        }
    }
    public void StartCraft(int id, int amount)
    {
        GameController.Instance.AddAmoutGem_O[id] -= amount;
        //CraftLastProgress = Gem.Instance.SetShiftGap(id);
        CurrentProgress = 0;
        mCraftCountDown = StartCoroutine(CraftCountDown(mCraftTime));
        GemID = id;
        GemGrade = 0;
        CraftLastProgress = Gem.Instance.SetShiftGap(id);
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
            GameController.Instance.AddAmoutGem_SSS[GemID]++;
            StopCoroutine(mCraftCountDown);
            UIController.Instance.Popwindow(3);
            mCraftCountDown = null;
        }
        else
        {
            CurrentProgress += mTouchPower;
            if(CurrentProgress > CraftLastProgress)
            {
                CurrentProgress = CraftLastProgress;
            }
            GemGrade = Gem.Instance.SetProgress(CurrentProgress);
        }
        ShowGaugeBar(CurrentProgress, CraftLastProgress);
    }
}
