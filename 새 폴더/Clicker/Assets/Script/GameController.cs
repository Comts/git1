using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData //데이터 클래스의 변수는 public
{
    public double Gold;

    public int Stage;
    public int LastGemID;
    public double Progress;

    public int[] PlayerItemLevelArr; //게임 처음 시작할 때 이 부분을 어떻게 처리할 것인가
}

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private SaveData mUser;

    public Delegates.VoidCallback GoldCallback;

    public double Gold
    {
        get { return mUser.Gold; } //get을 사용 안 하면 Gold = Gold(에러) +100;에서 에러가 뜸.
        set
        {
            if (value >= 0)
            {
                mUser.Gold = value;
                GoldCallback?.Invoke();
                //if(GoldCallback!=null) 위의 한 줄과 같은 코드
                //{
                //    GoldCallback();
                //}
            }
            else
            { Debug.Log("Not enough gold"); }
            GoldCallback = null;
        }
    }

    [SerializeField]
    private double mIncomeWeight = 1.04;
    private double mIncome;

    [SerializeField]
    private double mProgressWeight = 1.08d; //클리커게임에서는 double을 많이 사용
    private double mMaxProgress;

    private double mTouchPower;
    public double TouchPower
    {
        get { return mTouchPower; }
        set
        {
            if (value >= 0)
            {
                mTouchPower = value;
            }
            else
            {
                Debug.LogError("Error on touch power update" + value);
            }
        }
    }

    public double CriticalRate { get; set; }

    public double CriticalValue { get; set; }

    [SerializeField] //임시
    private GemPool mGemPool;

    [SerializeField]
    private Gem mCurrentGem;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //TODO Load Save data
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //mUser.Stage = 0; 데이터 로드함에 따라 이 부분 제거
        //mTouchPower = 1;

        //시작할 때 세이브 데이터 로드
        CalcStage(mUser.LastGemID); 
        mCurrentGem.SetProgress((float)(mUser.Progress / mMaxProgress));
        UIController.Instance.ShowGaugeBar(mUser.Progress, mMaxProgress);
    }

    private void LoadGame()
    {
        string location = Application.streamingAssetsPath + "/SaveData";
        if(File.Exists(location)) 
        {
            StreamReader reader = new StreamReader(location);
            string data = reader.ReadToEnd();
            if(string.IsNullOrEmpty(data))
            {
                //새로운 세이브 데이터 생성
            }
            else
            {
                mUser = JsonConvert.DeserializeObject<SaveData>(data);
            }
        }
        else //파일이 경로에 없을 때 (아예 게임이 새로 시작될 때)
        {
            //새로운 세이브 데이터 생성
            mUser = new SaveData();
        }       
    }

    public int[] GetPlayerItemLevelArr() //세이브 데이터 불러오기 위한 어레이
    {
        return mUser.PlayerItemLevelArr;
    }

    private void CalcStage(int id=-1) //세이브 데이터 로드를 위해 Touch메서드와 분할
    {        
        mMaxProgress = 10 * Math.Pow(mProgressWeight, mUser.Stage); //그냥 Mathf를 쓰면 float값이 리턴되어 시스템 Math 사용
        if(mCurrentGem!=null)
        {
            mCurrentGem.gameObject.SetActive(false);
        }
        if(mUser.LastGemID <0) //파라미터추가
        {
            mUser.LastGemID = UnityEngine.Random.Range(0, Constants.TOTAL_GEM_COUNT); //세공중인 보석 종류 저장
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
            //mCurrentProgress = Math.Min(mCurrentProgress + mTouchPower, mMaxProgress); //아래의 5줄 코드와 동일 동작 
            mUser.Progress += mTouchPower;
            if(mUser.Progress >mMaxProgress)
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
        //mCurrentStage++;
        //mMaxProgress = 10 * Math.Pow(mProgressWeight, mCurrentStage);
        //UIController.Instance.ShowGaugeBar(mCurrentProgress, mMaxProgress);
        }
    }
}
