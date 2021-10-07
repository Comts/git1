using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgradeController : InformationLoader
{
    public static PlayerUpgradeController Instance;

    [SerializeField]
    private PlayerStat mInfo;
    [SerializeField]
    private PlayerStatText mTextInfo;
#pragma warning disable 0649
    [SerializeField]
    private Sprite mIcon;
    [SerializeField]
    private UIElement mElement;
    [SerializeField]
    private Slider mSlider;
    [SerializeField]
    private Text mPowerAmountText,mPowerText;
#pragma warning restore 0649
    public PlayerStatText GetTextInfo()
    {
        return mTextInfo;
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
        LoadJson(out mInfo, Paths.PLAYER_TABLE);
        LoadJson(out mTextInfo,
            Paths.PLAYER_TEXT_TABLE +
            Paths.LANGUAGE_TYPE_ARR[GameController.Instance.LanguageType]);
        ReStart();
        Load();

    }
    public void ReStart()
    {

        //mElementArr = new UIElement[Constants.PLAYER_STAT_COUNT];

        mInfo.CurrentLevel = GameController.Instance.GetPlayerLevel;

        if (mInfo.CurrentLevel < mInfo.MaxLevel)
        {
            mElement.SetButtonActive(true);
        }
        else
        {
            mElement.SetButtonActive(false);
        }
        Refresh();
    }


    private void Load()
    {
        mElement.Init(0, mIcon,
                      mTextInfo.Title,
                      string.Format("레벨 : {0}",mInfo.CurrentLevel.ToString()),
                      string.Format(mTextInfo.ContentsFormat,
                                    UnitSetter.GetUnitStr(mInfo.ValueCurrent),
                                    mInfo.Duration.ToString()),
                      UnitSetter.GetUnitStr(Math.Round(mInfo.CostCurrent)),
                      LevelUP);
    }
    public void LevelUP(int id, int amount)
    {
        Delegates.VoidCallback callback = () => { LevelUpCallback(id, amount); };

        GameController.Instance.GoldCallback = callback;
        double cost = Math.Round(mInfo.CostCurrent);
        GameController.Instance.Gold -= cost;
    }
    public void LevelUpCallback(int id, int level)
    {

        mInfo.CurrentLevel += level;
        if (mInfo.CurrentLevel == mInfo.MaxLevel)
        {
            mElement.SetButtonActive(false);
        }
        GameController.Instance.GetPlayerLevel = mInfo.CurrentLevel;
        Refresh();
        Quest_PlayerLevel.Instance.CheckQuest();
    }
    private void Refresh()
    {
        CalcData();

        mElement.Refresh(string.Format("레벨 : {0}", mInfo.CurrentLevel.ToString()),
                      string.Format(mTextInfo.ContentsFormat,
                                    UnitSetter.GetUnitStr(mInfo.ValueCurrent),
                                    mInfo.Duration.ToString()),
                      UnitSetter.GetUnitStr(Math.Round(mInfo.CostCurrent)));
    }
    private void CalcData()
    {
        mInfo.CostCurrent = mInfo.CostBase *
                                Math.Pow(mInfo.CostWeight, mInfo.CurrentLevel);

        mInfo.ValueCurrent = mInfo.ValueBase + (mInfo.CurrentLevel + 1) * mInfo.CurrentLevel * mInfo.ValueWeight / 2;
        GameController.Instance.MaxManPower = mInfo.ValueCurrent;
        GameController.Instance.ManPower = GameController.Instance.CalBuffManPower();
        ReSetSlider();
    }
    public void ReSetSlider()
    {

        mSlider.value = 1;
        mPowerAmountText.text = string.Format("현재 노동력 : {0}", UnitSetter.GetUnitStr(GameController.Instance.ManPower)); 
    }
    public  float  PowerVolume
    {

        get
        {
            return PowerVolume;
        }
        set
        {
            GameController.Instance.ManPower = Math.Round(value * GameController.Instance.CalBuffManPower(), 2);
            mPowerText.text = string.Format("{0}%", Math.Round(value * 100));
            mPowerAmountText.text = string.Format("현재 노동력 : {0}", UnitSetter.GetUnitStr(GameController.Instance.ManPower));
        }
    }
}
