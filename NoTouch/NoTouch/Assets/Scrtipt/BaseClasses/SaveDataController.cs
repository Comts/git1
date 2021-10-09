using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class SaveDataController : MonoBehaviour
{
    [SerializeField]
    protected SaveData mUser;
    [SerializeField]
    private string url = "";
    [SerializeField]
    private Transform SleepWindow;

    private double mTenMinCount;

    IEnumerator StartTimeChk()
    {
        double starttime=0;
        double CheckDay = 0;
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

                if (CheckDay - mUser.CheckDay >= 1)
                {
                    mUser.CheckDay = CheckDay;
                    if(mUser.Check_Attend_Reward == 0)
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
        if (GameController.Instance.PlayMoleCount < 3)
        {
            if((int)(Math.Abs(mUser.EndTime - mUser.StartTime) / 600)- mTenMinCount > 0)
            {
                GameController.Instance.PlayMoleCount++;
                mTenMinCount++;
                MoleController.Instance.CheckPlayButton();
            }
        }
        //Debug.Log("종료시간 저장 : " + mUser.EndTime);
    }
    public void GetStartTime()
    {
        StartCoroutine(StartTimeChk());
    }
    public void GetEndTime()
    {
        Save();
        StartCoroutine(EndTimeChk());
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
        mUser.Achive_Silver = 0;
        mUser.Achive_Earth = 0;
        mUser.EarthCurrentProgress = 0;

        mUser.Attend_Reward = -1;
        mUser.Check_Attend_Reward = 0;
        mUser.CheckDay = 0;

        mUser.PlayerProfile = 0;
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