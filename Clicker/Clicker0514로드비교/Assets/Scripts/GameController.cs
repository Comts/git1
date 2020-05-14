using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField]
    private SaveData mUser;
    //private double mGold;
    //private int mCurrentStage;
    //private double mCurrentProgress;
    //private int mGemID;
    //private int[] mPlayerItemLevelArr;

    public Delegates.VoidCallback GoldCallback;
    public double Gold
    {
        get { return mUser.Gold; }
        set
        {
            if(value >= 0)
            {
                mUser.Gold = value;
                if(GoldCallback != null)
                {
                    GoldCallback();
                }
            }
            else
            {
                Debug.Log("not enough gold");
            }
            GoldCallback = null;
        }
    }

    [SerializeField]
    private double mIncomeWeight = 1.04d;
    private double mIncome;


    [SerializeField]
    private double mProgressWeight = 1.08d;
    private double mMaxProgress;

    private double mTouchPower;
    public double TouchPower
    {
        get { return mTouchPower; }
        set
        {
            if(value >= 0)
            {
                mTouchPower = value;
            }
            else
            {
                Debug.LogError("Error on touch power update " + value);
            }
        }
    }

    public double CriticalRate { get; set; }

    public double CriticalValue { get; set; }

    [SerializeField]//temp
    private GemPool mGemPool;

    [SerializeField]
    private Gem mCurrentGem;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //mUser.Stage = 0;
        //mTouchPower = 1;
        //게임 로드
        CalcStage(mUser.LastGemID); //mGemID 보석 불러오기
        mCurrentGem.SetProgress((float)(mUser.Progress / mMaxProgress));
        UIController.Instance.ShowGaugeBar(mUser.Progress, mMaxProgress);
    }
    private void LoadGame()
    {
        string location = Application.streamingAssetsPath + "/SaveData";
        if (true)//File.Exists(location))//파일존재 확인
        {
            //StreamReader reader = new StreamReader(location);
            
            string data = PlayerPrefs.GetString("SaveData"); //reader.ReadToEnd();
            if(string.IsNullOrEmpty(data)) //파일이 비어있는지확인
            {

                //새로운 세이브 데이터 생성
                CreateNewSaveData();
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));
                mUser = (SaveData)formatter.Deserialize(stream);// load
            }
            //reader.Close();
        }
        //else
        //{
        //    //새로운 세이브 데이터 생성
        //    CreateNewSaveData();
        //}

    }
    private void CreateNewSaveData()
    {
        mUser = new SaveData();
        mUser.Gold = 0;
        mUser.LastGemID = -1;
        mUser.Progress = 0;
        mUser.Stage = 0;
        mUser.PlayerItemLevelArr = new int[Constants.PLAYER_TIEM_COUNT];
        mUser.PlayerItemLevelArr[0] = 1;//터치하는 아이템레벨
    }

    private void Save()
    {
        string location = Application.streamingAssetsPath + "/SaveData";
        BinaryFormatter formatter = new BinaryFormatter();//세이브
        MemoryStream stream = new MemoryStream(); //세이브
        //StreamWriter writer = new StreamWriter(location);

        formatter.Serialize(stream, mUser);

        string data = Convert.ToBase64String(stream.GetBuffer());
        PlayerPrefs.SetString("SaveData", data);
        //writer.Write(data);
        //writer.Close();
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    public int[] GetPlayerItemLevelArr()
    {
        return mUser.PlayerItemLevelArr;
    }

    private void CalcStage(int id = -1)
    {
        mMaxProgress = 10 * Math.Pow(mProgressWeight, mUser.Stage);
        if(mCurrentGem != null)
        {
            mCurrentGem.gameObject.SetActive(false);
        }
        if(mUser.LastGemID < 0)
        {
            mUser.LastGemID = UnityEngine.Random.Range(0, Constants.TOTAL_GEM_COUNT);
        }
        mCurrentGem = mGemPool.GetFromPool(mUser.LastGemID);
        mIncome = 5 * Math.Pow(mIncomeWeight, mUser.Stage);
    }

    public void Touch()
    {
        if (mUser.Progress >= mMaxProgress)
        {
            mUser.Gold += mIncome;
            mUser.Stage++;
            mUser.Progress = 0;
            CalcStage();
        }
        else
        {
            mUser.Progress += mTouchPower;
            if(mUser.Progress > mMaxProgress)
            {
                mUser.Progress = mMaxProgress;
            }
            float progress = (float)(mUser.Progress / mMaxProgress);
            mCurrentGem.SetProgress(progress);
        }
        UIController.Instance.ShowGaugeBar(mUser.Progress, mMaxProgress);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    mUser.Stage++;
        //    mMaxProgress = 10 * Math.Pow(mProgressWeight, mUser.Stage);
        //    UIController.Instance.ShowGaugeBar(mUser.Progress, mMaxProgress);
        //}
    }
}
