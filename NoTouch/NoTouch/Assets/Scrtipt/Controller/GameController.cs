using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : SaveDataController
{
    public static GameController Instance;

    public int LanguageType { get; set; }
    //TODO Text imageArr 추가
    //[SerializeField]
    //private Sprite[] mGemArr;
#pragma warning disable 0649
    [SerializeField]
    private Transform mTextEffectPos;
    [SerializeField]
    private float WorkIncrese = 1.2f;
    [SerializeField]
    private double GemCostIncrese = 1.3;
    [SerializeField]
    private Toggle mScrollToggle;
#pragma warning restore 0649
    private double[] mFloorProgress, mFloorProgressCal;
    private double[] mFloorGemCost, mFloorGemCostCal;
    [SerializeField]
    private double mManPower,mMaxManPower,mBuffAchieve,mBuffCoworker,mBuffCoworker_Double;
    private double mTimeLag;
    private int mCheckClickAmount;
    public Delegates.VoidCallback GoldCallback;
    public double Gold
    {
        get { return mUser.Gold; }
        set
        {
            if(value >=0)
            {
                mUser.Gold = value;
                if (GoldCallback != null)
                {
                    GoldCallback();
                }
            }
            else
            {
                Debug.Log("not enough gold");
            }
            GoldCallback = null;
            UIController.Instance.ShowMoney();
            StageController.Instance.CheckDigButton();
        }
    }
    #region Gem Count
    public double[] AddAmoutGem_O
    {
        get { return mUser.AmoutGem_O; }
        set
        {
            mUser.AmoutGem_O = value;
        }
    }
    public double[] AddAmoutGem_B
    {
        get { return mUser.AmoutGem_B; }
        set
        {
            mUser.AmoutGem_B = value;
        }
    }
    public double[] AddAmoutGem_A
    {
        get { return mUser.AmoutGem_A; }
        set
        {
                mUser.AmoutGem_A = value;
        }
    }
    public double[] AddAmoutGem_S
    {
        get { return mUser.AmoutGem_S; }
        set
        {
            mUser.AmoutGem_S = value;
        }
    }
    #endregion
    public double[] GetGemCost
    {
        get { return mFloorGemCost; }
    }
    public double[] GetRequireProgress
    {
        get { return mFloorProgress; }
    }
    public int Stage
    {
        get { return mUser.Stage; }
        set
        {
            mUser.Stage = value;
        }

    }
    public int PlayerPos
    {
        get { return mUser.PlayerPos; }
        set
        {
            mUser.PlayerPos = value;
        }
    }
    public bool[] CheckAutoSell
    {
        get { return mUser.AutoSellCheck; }
        set
        {
            mUser.AutoSellCheck = value;
        }
    }
    public bool CheckAllSell
    {
        get { return mUser.AllSellCheck; }
        set
        {
            mUser.AllSellCheck = value;
        }
    }
    public bool CheckScrollPin
    {
        get { return mUser.ScrollPinCheck; }
        set
        {
            mUser.ScrollPinCheck = value;
        }
    }
    public double ManPower
    {
        get { return mManPower; }
        set
        {
            if(value>=0)
            {
                mManPower = value;
            }
            else
            {
                Debug.LogError("Error on manpower update " + value);
            }
        }
    }
    public double MaxManPower
    {
        get { return mMaxManPower; }
        set
        {
            if(value>=0)
            {
                mMaxManPower = value;
            }
            else
            {
                Debug.LogError("Error on mMaxManPower update " + value);
            }
        }
    }
    public double Buff_Achieve
    {
        get { return mBuffAchieve; }
        set
        {
            if(value>=0)
            {
                mBuffAchieve = value;
            }
            else
            {
                Debug.LogError("Error on mBuffAchieve update " + value);
            }
        }
    }
    public double Buff_Coworker
    {
        get { return mBuffCoworker; }
        set
        {
            if(value>=0)
            {
                mBuffCoworker = value;
            }
            else
            {
                Debug.LogError("Error on mBuffCoworker update " + value);
            }
        }
    }
    public double BuffCoworker_Double
    {
        get { return mBuffCoworker_Double; }
        set
        {
            if(value>=0)
            {
                mBuffCoworker_Double = value;
            }
            else
            {
                Debug.LogError("Error on mBuffCoworker_Double update " + value);
            }
        }
    }
    public double TimeLag
    {
        get { return mTimeLag; }
        set
        {
            if (value >= 0)
            {
                mTimeLag = value;
            }
            else
            {
                Debug.LogError("Error on manpower update " + value);
            }
        }
    }
    public int[] HaveMine
    {
        get { return mUser.MineArr; }
        set
        {
            mUser.MineArr = value;
        }

    }
    public int[] HaveItem
    {
        get { return mUser.HaveItmeArr; }
        set
        {
            mUser.HaveItmeArr = value;
        }
        

    }
    public float[] GetItemCooltimeArr()
    {
        return mUser.ItemCooltimeArr;
    }

    public float[] GetItemMaxCooltimeArr()
    {
        return mUser.ItemMaxCooltimeArr;
    }
    public double[] AddFromMine
    {
        get { return mUser.GetFromMine; }
        set
        {
            mUser.GetFromMine = value;
        }

    }

    public int PlayMoleCount
    {
        get { return mUser.PlayMoleCount; }
        set
        {
            mUser.PlayMoleCount = value;
        }
    }
    public int WhackCount
    {
        get { return mUser.WhackCount; }
        set
        {
            mUser.WhackCount = value;
        }
    }
    public int MineCount
    {
        get { return mUser.MineCount; }
        set
        {
            mUser.MineCount = value;
        }
    }
    public int GetPlayerLevel
    {
        get{ return mUser.PlayerLevel; }
        set{
            mUser.PlayerLevel = value;
        }
    }
    #region Quest
    public int Quest_PlayerLevel
    {
        get { return mUser.Quest_PlayerLevel; }
        set
        {
            mUser.Quest_PlayerLevel = value;
        }

    }
    public int Quest_CowerkerLevelSum
    {
        get { return mUser.Quest_CoworkerLevelSum; }
        set
        {
            mUser.Quest_CoworkerLevelSum = value;
        }

    }
    public int Quest_DigCount
    {
        get { return mUser.Quest_DigCount; }
        set
        {
            mUser.Quest_DigCount = value;
        }

    }
    public int Quest_CraftGem
    {
        get { return mUser.Quest_CraftGem; }
        set
        {
            mUser.Quest_CraftGem = value;
        }

    }
    public int Quest_MoleCount
    {
        get { return mUser.Quest_MoleCount; }
        set
        {
            mUser.Quest_MoleCount = value;
        }

    }
    public int Quest_MineCount
    {
        get { return mUser.Quest_MineCount; }
        set
        {
            mUser.Quest_MineCount = value;
        }

    }
    public int Quest_SilverDazi
    {
        get { return mUser.Quest_SilverDazi; }
        set
        {
            mUser.Quest_SilverDazi = value;
        }

    }
    public int Quest_GoldDazi
    {
        get { return mUser.Quest_GoldDazi; }
        set
        {
            mUser.Quest_GoldDazi = value;
        }

    }
    #endregion 
    #region Achieve
    public int Achieve_Click
    {
        get { return mUser.Achieve_Click; }
        set
        {
            mUser.Achieve_Click = value;
        }

    }
    public int Achive_Mole
    {
        get { return mUser.Achive_Mole; }
        set
        {
            mUser.Achive_Mole = value;
        }

    }
    public int Achive_AutoClick
    {
        get { return mUser.Achive_AutoClick; }
        set
        {
            mUser.Achive_AutoClick = value;
        }

    }
    public int Achive_Norini
    {
        get { return mUser.Achive_Norini; }
        set
        {
            mUser.Achive_Norini = value;
        }

    }
    public int Achive_Coal
    {
        get { return mUser.Achive_Coal; }
        set
        {
            mUser.Achive_Coal = value;
        }

    }
    public int Achive_Ame
    {
        get { return mUser.Achive_Ame; }
        set
        {
            mUser.Achive_Ame = value;
        }

    }
    public int Achive_Gold
    {
        get { return mUser.Achive_Gold; }
        set
        {
            mUser.Achive_Gold = value;
        }

    }
    public int Achive_Dia
    {
        get { return mUser.Achive_Dia; }
        set
        {
            mUser.Achive_Dia = value;
        }

    }
    public int Achive_Vib
    {
        get { return mUser.Achive_Vib; }
        set
        {
            mUser.Achive_Vib = value;
        }

    }
    public int Achive_Dosirak
    {
        get { return mUser.Achive_Dosirak; }
        set
        {
            mUser.Achive_Dosirak = value;
        }

    }
    public int EatAmount
    {
        get { return mUser.EatAmount; }
        set
        {
            mUser.EatAmount = value;
        }

    }
    public int Achive_Silver
    {
        get { return mUser.Achive_Silver; }
        set
        {
            mUser.Achive_Silver = value;
        }

    }
    public int UseSilverAmount
    {
        get { return mUser.UseSilverAmount; }
        set
        {
            mUser.UseSilverAmount = value;
        }

    }
    public int Achive_Earth
    {
        get { return mUser.Achive_Earth; }
        set
        {
            mUser.Achive_Earth = value;
        }

    }
    #endregion 
    public double EarthCurrentProgress
    {
        get { return mUser.EarthCurrentProgress; }
        set
        {
            mUser.EarthCurrentProgress = value;
        }

    }
    public int Attend_Reward
    {
        get { return mUser.Attend_Reward; }
        set
        {
            mUser.Attend_Reward = value;
        }

    }
    public int Check_Attend_Reward
    {
        get { return mUser.Check_Attend_Reward; }
        set
        {
            mUser.Check_Attend_Reward = value;
        }

    }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
            LoadGame();
            if (Application.systemLanguage==SystemLanguage.Korean)
            {
                //Debug.Log("Kor " + (int)Application.systemLanguage);
                LanguageType = 0;
            }
            else
            {
                //Debug.Log("Non Kor" + (int)Application.systemLanguage);
                LanguageType = 1;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mUser.Stage < mUser.PlayerPos)
        {
            mUser.PlayerPos = mUser.Stage;
        }
        //CalManPower();
        mFloorProgress = new double[Constants.MAX_fLOOR];
        mFloorProgressCal = new double[Constants.MAX_fLOOR];
        mFloorGemCost = new double[Constants.MAX_fLOOR];
        mFloorGemCostCal = new double[Constants.MAX_fLOOR];
        UIController.Instance.ShowMoney();
        for (int i = 0; i < Constants.MAX_fLOOR; i++)
        {
            if (i == 0)
            {
                mFloorProgress[i] = 10;
                mFloorProgressCal[i] = 10;
                mFloorGemCost[i] = 1;
                mFloorGemCostCal[i] = 1;
                //Debug.Log((i) + "번째 층 노동력" + mFloorProgress[i]);
                //Debug.Log((i) + "번째 층 원석가격" + mFloorGemCost[i]);
            }
            else
            {
                mFloorProgressCal[i] = mFloorProgressCal[i - 1] * WorkIncrese;
                mFloorProgress[i] = Math.Round(mFloorProgressCal[i],2);
                mFloorGemCostCal[i] = mFloorGemCost[i - 1] * GemCostIncrese;
                mFloorGemCost[i] = Math.Round(mFloorGemCostCal[i], 1);
                //Debug.Log((i) + "번째 층 노동력" + mFloorProgress[i]);
                //Debug.Log((i) + "번째 층 원석가격" + mFloorGemCost[i]);
            }
        }

        

        mScrollToggle.onValueChanged.AddListener((bOn) =>
        {
            CheckScrollPin = bOn;
        });

        GetStartTime();

        InvokeRepeating("GetEndTime", 2f, 60f); 

    }
    public void ReStart()
    {
        mUser.Gold = 0;
        mUser.AmoutGem_O = new double[Constants.MAX_fLOOR];
        mUser.AmoutGem_B = new double[Constants.MAX_fLOOR];
        mUser.AmoutGem_A = new double[Constants.MAX_fLOOR];
        mUser.AmoutGem_S = new double[Constants.MAX_fLOOR];

        Stage = 0;
        mUser.PlayerPos = 0;
        mUser.Progress = 0;
        mUser.ScrollPinCheck = false;

        mUser.PlayerLevel = Constants.PLAYER_STAT_COUNT;
        mUser.ItemCooltimeArr = new float[Constants.USEITEM_AMOUT];
        mUser.ItemMaxCooltimeArr = new float[Constants.USEITEM_AMOUT];
        mUser.HaveItmeArr = new int[Constants.ITEM_COUNT];
        mUser.HaveItmeArr[0] = 5;
        mUser.HaveItmeArr[1] = 1;

        mUser.CoworkerLevelArr = new int[Constants.MAX_fLOOR];
        for (int i = 0; i < mUser.CoworkerLevelArr.Length; i++)
        {
            mUser.CoworkerLevelArr[i] = -1;
        }
        mUser.CoworkerLevelArr[0] = 0;

        mUser.MineArr = new int[Constants.MINE_COUNT];
        mUser.GetFromMine = new double[Constants.MINE_COUNT];

        mUser.Quest_PlayerLevel = 0;
        mUser.Quest_CoworkerLevelSum = 0;
        mUser.Quest_DigCount = 0;
        mUser.Quest_CraftGem = 0;
        mUser.Quest_MoleCount = 0;
        mUser.Quest_MineCount = 0;
        mUser.Quest_SilverDazi = 0;
        mUser.Quest_GoldDazi = 0;

        mUser.ClickAmount = 0;
        mUser.Achieve_Click = 0;

        mUser.Achive_Mole = 0;
        //mUser.Achive_AutoClick = 0;
        //mUser.Achive_Norini = 0;
        mUser.Achive_Coal = 0;
        mUser.Achive_Ame = 0;
        mUser.Achive_Gold = 0;
        mUser.Achive_Dia = 0;
        mUser.Achive_Vib = 0;

        mUser.EatAmount = 0;
        mUser.Achive_Dosirak = 0;

        mUser.UseSilverAmount = 0;
        mUser.Achive_Silver = 0;
        mUser.Achive_Earth = 0;
        mUser.EarthCurrentProgress = 0;

        mUser.Attend_Reward = -1;
        mUser.Check_Attend_Reward = 0;
        mUser.CheckDay = 0;

        mUser.WhackCount = 0;
        mUser.MineCount = 0;

        GemSellController.Instance.ReStart();
        PlayerUpgradeController.Instance.ReStart();
        CoworkerController.Instance.ReStart();
        StageController.Instance.ReStart();
        UIController.Instance.ShowMoney();
        ItemUseController.Instance.ReStart();
        QuestController.Instance.ReStart();
        IAPController.Instance.ReStart();
        MineShopController.Instance.ReStart();
        CraftController.Instance.ReStart();

        mUser.PlayMoleCount = 3;
        MoleController.Instance.CheckPlayButton();

        Save();
        LoadGame();
    }
    private void OnApplicationQuit()
    {
        GetEndTime();
        Save();
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        Save();
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        Save();
    }
    public float[] GetSkillCooltimeArr()
    {
        return mUser.ItemCooltimeArr;
    }
    public float[] GetSkillMaxCooltimeArr()
    {
        return mUser.ItemMaxCooltimeArr;
    }
    public int[] GetCoworkerLevelArr()
    {
        return mUser.CoworkerLevelArr;
    }
    public int GetCoworkerLevelSum()
    {
        int LevelSum = 0;
        for (int i = 0; i < mUser.CoworkerLevelArr.Length; i++)
        {
            if(mUser.CoworkerLevelArr[i] >= 0)
            {
                LevelSum += mUser.CoworkerLevelArr[i];
            }
        }
        return LevelSum;
    }
    //public void AddAmoutGem_A(int id,double amount)
    //{
    //    mUser.AmoutGem_A[id] += amount; ;
    //}
    public void Touch()
    {
        int gain = 0;
        mUser.Progress += Math.Round((mManPower * ItemUseController.Instance.GetGemMulti[0]),2);
        //TODO Sound FX
        //TODO VFX +Text Effect

        while (mUser.Progress >= mFloorProgress[mUser.PlayerPos])
        {
            mUser.Progress = Math.Round((mUser.Progress - mFloorProgress[mUser.PlayerPos]),2);
            gain++;
        }
        AddAmoutGem_O[mUser.PlayerPos] += gain ;
        GemSellController.Instance.RefreshGemData();
        if (Achieve_Click == 0)
        {
            mUser.ClickAmount++;
            if (mUser.ClickAmount >= 100000000)
            {
                QuestController.Instance.Achive_Click();
            }
        }
        //TextEffect effect = TextEffectPool.Instance.GetFromPool();
        //effect.SetText(gain.ToString());
        ////TODO Icon 변경 effect.SetIcon();
        //effect.transform.position = mTextEffectPos.position;

    }
    public double CalBuffManPower()
    {
        return MaxManPower * (1 + Buff_Achieve + Buff_Coworker*(1+ BuffCoworker_Double));
    }
}
