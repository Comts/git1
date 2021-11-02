using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SaveDataController : MonoBehaviour
{
    [SerializeField]
    protected SaveData mUser;
    [SerializeField]
    private string url = "";
#pragma warning disable 0649
    [SerializeField]
    private Transform SleepWindow;
    [SerializeField]
    private int RandomDaziCoolTime,AutoClickAdsCoolTime;
    [SerializeField]
    private Text RandomDaziText;
    [SerializeField]
    private ItemButton RandomDaziButton,AutoClickAdsButton;
    [SerializeField]
    private Image PointRandomDaziButton, PointAutoClickAdsButton;
#pragma warning restore 0649

    private double mTenMinCount;
    private double mPlayMoletime;
    private double mStartMoletime;
    private double mRandomDaziClicktime,mAutoClickAdstime;
    private Coroutine RandomDaziRouting, StartRandomDaziRouting;
    private Coroutine AutoClickAds;
    double CheckDay = 0;

    IEnumerator StartTimeChk()
    {
        double starttime=0;
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string date = request.GetResponseHeader("date");

                DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
                TimeSpan timestamp = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);
                starttime = timestamp.TotalSeconds;
                CheckDay = timestamp.Days;
                if (mUser.CheckDay == 0)
                {
                    mUser.CheckDay = CheckDay;
                }
                if (CheckDay - mUser.CheckDay >= 1)
                {
                    mUser.CheckDay = CheckDay;
                    mUser.RandomDaziCount = 10;
                    ShowRandomDaziText(false);
                    if (mUser.Check_Attend_Reward == 0)
                    {
                        GameController.Instance.Attend_Reward++;
                        mUser.Check_Attend_Reward = 1;
                    }
                    QuestController.Instance.ShowAttendWindow();
                }
            }
        }
        mUser.StartTime = starttime;
        if(mUser.StartTime * mUser.EndTime !=0)
        {
            GameController.Instance.TimeLag = Math.Abs(mUser.StartTime - mUser.EndTime);
        }
        if (MineShopController.Instance.CheckSleepWork() || CoworkerController.Instance.CheckSleepJob())
        {
            SleepWindow.gameObject.SetActive(true);
        }

        mTenMinCount = (int)(GameController.Instance.TimeLag / 600);
        while (GameController.Instance.PlayMoleCount<3 && mTenMinCount>0)
        {
            GameController.Instance.PlayMoleCount++;
            mTenMinCount--;
            MoleController.Instance.CheckPlayButton();
        }
        mTenMinCount = 0;
        //Debug.Log("시작시간 : " + mUser.StartTime);
        //Debug.Log("종료시간 : " + mUser.EndTime);
        //Debug.Log("시간차이 : " + GameController.Instance.TimeLag);

    }
    IEnumerator EndTimeChk()
    {
        double Endtime=0;
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string date = request.GetResponseHeader("date");

                DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
                TimeSpan timestamp = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);
                Endtime = timestamp.TotalSeconds;
            }
        }
        mUser.EndTime = Endtime;
    }
    private IEnumerator RandomDaziClickTimeCheck()
    {
        double Gettime = 0;
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string date = request.GetResponseHeader("date");

                DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
                TimeSpan timestamp = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);
                Gettime = timestamp.TotalSeconds;
            }
        }
        mRandomDaziClicktime = Gettime;
        if (mUser.RandomDaziFinishTime == 0)
        {
            mUser.RandomDaziFinishTime = Gettime;
        }
    }
    private IEnumerator RandomDaziRoutine()
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        mRandomDaziClicktime = 0;

        while (mRandomDaziClicktime == 0)
        {
            StartCoroutine(RandomDaziClickTimeCheck());
            yield return frame;
        }

        if (mRandomDaziClicktime >= mUser.RandomDaziFinishTime)
        {
            mUser.RandomDaziFinishTime = mRandomDaziClicktime + RandomDaziCoolTime;
            mUser.RandomDaziCount--;

        }
        ShowRandomDaziText(false);
        double cooltime = mUser.RandomDaziFinishTime - mRandomDaziClicktime;

        PointRandomDaziButton.gameObject.SetActive(false);
        while (cooltime >= 0)
        {
            yield return frame;
            cooltime -= Time.deltaTime;
            RandomDaziButton.ShowCooltime((float)cooltime, RandomDaziCoolTime);
        }
        ShowRandomDaziText(true);
    }
    private IEnumerator StartRandomDazi()
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        mRandomDaziClicktime = 0;

        while (mRandomDaziClicktime == 0)
        {
            StartCoroutine(RandomDaziClickTimeCheck());
            yield return frame;
        }

        ShowRandomDaziText(false);
        if (mRandomDaziClicktime < mUser.RandomDaziFinishTime)
        {
            double cooltime = mUser.RandomDaziFinishTime - mRandomDaziClicktime;

            PointRandomDaziButton.gameObject.SetActive(false);
            while (cooltime >= 0)
            {
                yield return frame;
                cooltime -= Time.deltaTime;
                RandomDaziButton.ShowCooltime((float)cooltime, RandomDaziCoolTime);
            }
            ShowRandomDaziText(true);
        }
    }
    private IEnumerator AutoClickAdsRoutine()
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        mAutoClickAdstime = 0;

        while (mAutoClickAdstime == 0)
        {
            StartCoroutine(AutoClickAdsTimeCheck());
            yield return frame;
        }

        if (mAutoClickAdstime >= mUser.AutoClilckAdsFinishTime)
        {
            mUser.AutoClilckAdsFinishTime = mAutoClickAdstime + AutoClickAdsCoolTime;

        }
        double cooltime = mUser.AutoClilckAdsFinishTime - mAutoClickAdstime;

        AutoClickAds = StartCoroutine(AutoClick());
        PointAutoClickAdsButton.gameObject.SetActive(false);
        while (cooltime >= 0)
        {
            yield return frame;
            cooltime -= Time.deltaTime;
            AutoClickAdsButton.ShowCooltime((float)cooltime, AutoClickAdsCoolTime);
        }
        if (AutoClickAds != null)
        {
            StopCoroutine(AutoClickAds);
        }
        SoundController.Instance.FXSound(12);
        PointAutoClickAdsButton.gameObject.SetActive(true);
    }
    private IEnumerator AutoClickAdsTimeCheck()
    {
        double Gettime = 0;
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string date = request.GetResponseHeader("date");

                DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
                TimeSpan timestamp = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);
                Gettime = timestamp.TotalSeconds;
            }
        }
        mAutoClickAdstime = Gettime;
        if (mUser.AutoClilckAdsFinishTime == 0)
        {
            mUser.AutoClilckAdsFinishTime = Gettime;
        }
    }
    private IEnumerator StartAutoClick()
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        mAutoClickAdstime = 0;

        PointAutoClickAdsButton.gameObject.SetActive(true);
        while (mAutoClickAdstime == 0)
        {
            StartCoroutine(AutoClickAdsTimeCheck());
            yield return frame;
        }

        if (mAutoClickAdstime < mUser.AutoClilckAdsFinishTime)
        {
            double cooltime = mUser.AutoClilckAdsFinishTime - mAutoClickAdstime;

            AutoClickAds = StartCoroutine(AutoClick());
            PointAutoClickAdsButton.gameObject.SetActive(false);
            while (cooltime >= 0)
            {
                yield return frame;
                cooltime -= Time.deltaTime;
                AutoClickAdsButton.ShowCooltime((float)cooltime, AutoClickAdsCoolTime);
            }
            if (AutoClickAds != null)
            {
                StopCoroutine(AutoClickAds);
            }
            SoundController.Instance.FXSound(12);
            PointAutoClickAdsButton.gameObject.SetActive(true);
        }
    }
    private IEnumerator AutoClick()
    {
        WaitForSeconds TouchSec = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return TouchSec;
            GameController.Instance.Touch();
        }
    }
    public void AutoClick_Ads()
    {
        StartCoroutine(AutoClickAdsRoutine());
    }
    public void RandomDazi()
    {
        RandomDaziRouting=StartCoroutine(RandomDaziRoutine());
    }
    public void ShowRandomDaziText(bool b)
    {
        RandomDaziText.text = String.Format("하루 광고 제한 10회  남은 광고 : {0}회", mUser.RandomDaziCount);
        if (mUser.RandomDaziCount > 0)
        {
            if (b)
            {
                SoundController.Instance.FXSound(12);
            }
            RandomDaziButton.SetButtonActive(true);
            PointRandomDaziButton.gameObject.SetActive(true);
        }
        else
        {
            RandomDaziButton.SetButtonActive(false);
            PointRandomDaziButton.gameObject.SetActive(false);
        }
    }
    public void ResetRandomDazi()
    {
        if (mUser.CheckDay == 0)
        {
            mUser.CheckDay = CheckDay;
        }
        ShowRandomDaziText(false);
        if (RandomDaziRouting != null)
        {
            StopCoroutine(RandomDaziRouting);
        }
        if (StartRandomDaziRouting != null)
        {
            StopCoroutine(StartRandomDaziRouting);
        }
        RandomDaziButton.ShowCooltime(0,0);
    }
    IEnumerator MoleTimeChk()
    {
        double MoleChecktime=0;
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string date = request.GetResponseHeader("date");

                DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
                TimeSpan timestamp = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);
                MoleChecktime = timestamp.TotalSeconds;
            }
        }
        if (mStartMoletime == 0)
        {
            mStartMoletime = MoleChecktime;
        }
        else
        {
            mPlayMoletime = MoleChecktime;
        }

        if (mStartMoletime* mPlayMoletime != 0)
        {
            if ((mPlayMoletime - mStartMoletime) >= 600)
            {
                GameController.Instance.PlayMoleCount++;
                mStartMoletime = mPlayMoletime;
                mPlayMoletime = 0;
                MoleController.Instance.CheckPlayButton();
                if (GameController.Instance.PlayMoleCount >= 3)
                {
                    mStartMoletime = 0;
                }
            }

        }
    }
    public void GetStartTime()
    {
        StartCoroutine(StartTimeChk());
        StartCoroutine(StartAutoClick());
        StartRandomDaziRouting=StartCoroutine(StartRandomDazi());

    }
    public void GetEndTime()
    {
        Save();
        StartCoroutine(EndTimeChk());
        GetChargeMoleTime();
    }
    public void GetChargeMoleTime()
    {
        if (GameController.Instance.PlayMoleCount < 3)
        {
            StartCoroutine(MoleTimeChk());
        }
    }
    protected void LoadGame()
    {
        string data = PlayerPrefs.GetString("SaveData");
        if (string.IsNullOrEmpty(data))
        {
            CreateNewSaveData();
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));
            mUser = (SaveData)formatter.Deserialize(stream);
        }
        FixSaveData();

    }

    protected void FixSaveData()
    {

        if (mUser.ItemCooltimeArr == null)
        {
            mUser.ItemCooltimeArr = new float[Constants.USEITEM_AMOUT];
        }
        else if (mUser.ItemCooltimeArr.Length != Constants.USEITEM_AMOUT)
        {
            float[] temp = new float[Constants.USEITEM_AMOUT];
            int count = Mathf.Min(Constants.USEITEM_AMOUT, mUser.ItemCooltimeArr.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.ItemCooltimeArr[i];
            }
            mUser.ItemCooltimeArr = temp;
        }

        if (mUser.ItemMaxCooltimeArr == null)
        {
            mUser.ItemMaxCooltimeArr = new float[Constants.USEITEM_AMOUT];
        }
        else if (mUser.ItemMaxCooltimeArr.Length != Constants.USEITEM_AMOUT)
        {
            float[] temp = new float[Constants.USEITEM_AMOUT];
            int count = Mathf.Min(Constants.USEITEM_AMOUT, mUser.ItemMaxCooltimeArr.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.ItemMaxCooltimeArr[i];
            }
            mUser.ItemMaxCooltimeArr = temp;
        }
        if (mUser.HaveItmeArr == null)
        {
            mUser.HaveItmeArr = new int[Constants.ITEM_COUNT];
        }
        else if (mUser.HaveItmeArr.Length != Constants.ITEM_COUNT)
        {
            int[] temp = new int[Constants.ITEM_COUNT];
            int count = Mathf.Min(Constants.ITEM_COUNT, mUser.HaveItmeArr.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.HaveItmeArr[i];
            }
            mUser.HaveItmeArr = temp;
        }
        if (mUser.CoworkerLevelArr == null)
        {
            mUser.CoworkerLevelArr = new int[Constants.MAX_fLOOR];
            for (int i = 0; i < mUser.CoworkerLevelArr.Length; i++)
            {
                mUser.CoworkerLevelArr[i] = -1;
            }
            mUser.CoworkerLevelArr[0] = 0;
        }
        else if (mUser.CoworkerLevelArr.Length != Constants.MAX_fLOOR)
        {
            int[] temp = new int[Constants.MAX_fLOOR];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = -1;
            }
            temp[0] = 0;
            int count = Mathf.Min(Constants.MAX_fLOOR, mUser.CoworkerLevelArr.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.CoworkerLevelArr[i];
            }
            mUser.CoworkerLevelArr = temp;
        }

        if (mUser.MineArr == null)
        {
            mUser.MineArr = new int[Constants.MINE_COUNT];
        }
        else if (mUser.MineArr.Length != Constants.MINE_COUNT)
        {
            int[] temp = new int[Constants.MINE_COUNT];
            int count = Mathf.Min(Constants.MINE_COUNT, mUser.MineArr.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.MineArr[i];
            }
            mUser.MineArr = temp;
        }

        if (mUser.GetFromMine == null)
        {
            mUser.GetFromMine = new double[Constants.MINE_COUNT];
        }
        else if (mUser.GetFromMine.Length != Constants.MINE_COUNT)
        {
            double[] temp = new double[Constants.MINE_COUNT];
            int count = Mathf.Min(Constants.MINE_COUNT, mUser.GetFromMine.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.GetFromMine[i];
            }
            mUser.GetFromMine = temp;
        }

        if (mUser.AutoSellCheck == null)
        {
            mUser.AutoSellCheck = new bool[Constants.MAX_fLOOR * 5];
        }
        else if (mUser.AutoSellCheck.Length != (Constants.MAX_fLOOR * 5))
        {
            bool[] temp = new bool[Constants.MAX_fLOOR * 5];
            int count = Mathf.Min((Constants.MAX_fLOOR * 5), mUser.AutoSellCheck.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.AutoSellCheck[i];
            }
            mUser.AutoSellCheck = temp;
        }

    }

    protected void CreateNewSaveData()
    {
        mUser = new SaveData();
        mUser.Gold = 0;
        mUser.AmoutGem_O = new double[Constants.MAX_fLOOR];
        mUser.AmoutGem_B = new double[Constants.MAX_fLOOR];
        mUser.AmoutGem_A = new double[Constants.MAX_fLOOR];
        mUser.AmoutGem_S = new double[Constants.MAX_fLOOR];


        mUser.Stage = 0;
        mUser.PlayerPos = 0;
        mUser.Progress = 0;

        mUser.PlayerLevel = Constants.PLAYER_STAT_COUNT;
        mUser.ItemCooltimeArr = new float[Constants.USEITEM_AMOUT];
        mUser.ItemMaxCooltimeArr = new float[Constants.USEITEM_AMOUT];
        mUser.HaveItmeArr = new int[Constants.ITEM_COUNT];
        mUser.HaveItmeArr[0] = 5;
        mUser.HaveItmeArr[1] = 1;

        mUser.CoworkerLevelArr = new int [Constants.MAX_fLOOR];
        for (int i = 0; i < mUser.CoworkerLevelArr.Length; i++)
        {
            mUser.CoworkerLevelArr[i] = -1;
        }
        mUser.CoworkerLevelArr[0] = 0;

        mUser.MineArr = new int[Constants.MINE_COUNT];
        mUser.GetFromMine = new double[Constants.MINE_COUNT];

        mUser.AutoSellCheck = new bool[Constants.MAX_fLOOR*5];
        mUser.ScrollPinCheck = false;

        mUser.PlayMoleCount = 3;
        mUser.WhackCount = 0;
        mUser.MineCount = 0;
        mUser.RandomDaziCount = 10;

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
        mUser.Achive_AutoClick = 0;
        mUser.Achive_Norini = 0;
        mUser.Achive_Coal = 0;
        mUser.Achive_Ame = 0;
        mUser.Achive_Gold = 0;
        mUser.Achive_Dia = 0;
        mUser.Achive_Vib = 0;

        mUser.EatAmount = 0;
        mUser.Achive_Dosirak = 0;

        mUser.UseSilverAmount = 0;
        mUser.UseGoldAmount = 0;
        mUser.Achive_Silver_FLEX = 0;
        mUser.Achive_Gold_FLEX = 0;
        mUser.Achive_Earth = 0;
        mUser.EarthCurrentProgress = 0;

        mUser.Attend_Reward = -1;
        mUser.Check_Attend_Reward = 0;
        mUser.CheckDay = 0;
        mUser.RandomDaziFinishTime = 0;
        mUser.AutoClilckAdsFinishTime = 0;

        mUser.PlayerProfile = 0;
        mUser.FirstTry = 0;
        mUser.ChangeName = 0;
        mUser.GemSellTutorial = 0;
        mUser.PlayerLevelUpTutorial = 0;
        mUser.MoleTutorial = 0;
        mUser.StagePinTutorial = 0;
        mUser.CraftGemTutorial = 0;
        mUser.MineTutorial = 0;
    }

    protected void Save()
    {
        
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream(); 
        
        
        formatter.Serialize(stream, mUser);

        string data = Convert.ToBase64String(stream.GetBuffer());
        PlayerPrefs.SetString("SaveData", data);
       
    }
}