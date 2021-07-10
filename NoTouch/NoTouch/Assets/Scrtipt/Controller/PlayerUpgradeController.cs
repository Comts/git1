using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgradeController : InformationLoader
{
    public static PlayerUpgradeController Instance;

    private int[] mLevelArr;
    [SerializeField]
    private PlayerStat[] mInfoArr;
    [SerializeField]
    private PlayerStatText[] mTextInfoArr;
    private Sprite[] mIconArr;
#pragma warning disable 0649
    [SerializeField]
    private UIElement[] mElementArr;
#pragma warning restore 0649
    public PlayerStatText[] GetTextInfoArr()
    {
        return mTextInfoArr;
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


    // Start is called before the first frame update
    void Start()
    {
        LoadJson(out mInfoArr, Paths.PLAYER_TABLE);
        LoadJson(out mTextInfoArr,
            Paths.PLAYER_TEXT_TABLE +
            Paths.LANGUAGE_TYPE_ARR[GameController.Instance.LanguageType]);

        mIconArr = Resources.LoadAll<Sprite>(Paths.PLAYER_ICON);
        ReStart();

    }
    public void ReStart()
    {

        mLevelArr = GameController.Instance.GetPlayerLevelArr();
        //mElementArr = new UIElement[Constants.PLAYER_STAT_COUNT];
        for (int i = 0; i < mInfoArr.Length; i++)
        {

            mInfoArr[i].CurrentLevel = mLevelArr[i];

            CalcData(i);
        }
        Load();
    }

        
    private void Load()
    {
        for (int i = 0; i < mInfoArr.Length; i++)
        {
            mElementArr[i].Init(i, mIconArr[i],
                      mTextInfoArr[i].Title,
                      mInfoArr[i].CurrentLevel.ToString(),
                      string.Format(mTextInfoArr[i].ContentsFormat,
                                    UnitSetter.GetUnitStr(mInfoArr[i].ValueCurrent),
                                    mInfoArr[i].Duration.ToString()),
                      UnitSetter.GetUnitStr(Math.Round( mInfoArr[i].CostCurrent)),
                      LevelUP);
        }
    }
    public void LevelUP(int id, int amount)
    {
        Delegates.VoidCallback callback = () => { LevelUpCallback(id, amount); };

        GameController.Instance.GoldCallback = callback;
        double cost = Math.Round(mInfoArr[id].CostCurrent);
        GameController.Instance.Gold -= cost;
    }
    public void LevelUpCallback(int id, int level)
    {

        mInfoArr[id].CurrentLevel += level;
        if (mInfoArr[id].CurrentLevel == mInfoArr[id].MaxLevel)
        {
            mElementArr[id].SetButtonActive(false);
        }
        mLevelArr[id] = mInfoArr[id].CurrentLevel;
        CalcData(id);

        mElementArr[id].Refresh(mInfoArr[id].CurrentLevel.ToString(),
                      string.Format(mTextInfoArr[id].ContentsFormat,
                                    UnitSetter.GetUnitStr(mInfoArr[id].ValueCurrent),
                                    mInfoArr[id].Duration.ToString()),
                      UnitSetter.GetUnitStr(Math.Round(mInfoArr[id].CostCurrent)));
    }
    private void CalcData(int id)
    {
        mInfoArr[id].CostCurrent = mInfoArr[id].CostBase *
                                Math.Pow(mInfoArr[id].CostWeight, mInfoArr[id].CurrentLevel);

        mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase + (mInfoArr[id].CurrentLevel + 1) * mInfoArr[id].CurrentLevel * mInfoArr[id].ValueWeight / 2;
        GameController.Instance.ManPower = mInfoArr[id].ValueCurrent;
    }  
}
