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

        CalcData(id);

        mElementList[id].Refresh(mInfoArr[id].CurrentLevel.ToString(),
                      string.Format(mTextInfoArr[id].ContentsFormat,
                                    UnitSetter.GetUnitStr(mInfoArr[id].ValueCurrent)),
                      UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent));
    }
        private void CalcData(int id)
    {
        mInfoArr[id].CostCurrent = mInfoArr[id].CostBase *
                                Math.Pow(mInfoArr[id].CostWeight, mInfoArr[id].CurrentLevel);
        mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase *
                            Math.Pow(mInfoArr[id].ValueWeight, mInfoArr[id].CurrentLevel);
    }
}
