using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
#pragma warning restore 0649
    private float[] mFloorProgress, mFloorProgressCal;
    private double mManPower;
    public Delegates.VoidCallback GoldCallback;
    public double Gold
    {
        get { return mUser.TotalGold; }
        set
        {
            if(value >=0)
            {
                mUser.TotalGold = value;
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
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
            //TODO 게임로드
            if(Application.systemLanguage==SystemLanguage.Korean)
            {
                Debug.Log("Kor " + (int)Application.systemLanguage);
                LanguageType = 0;
            }
            else
            {
                Debug.Log("Non Kor" + (int)Application.systemLanguage);
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
        mFloorProgress = new float[Constants.Max_floor];
        mFloorProgressCal = new float[Constants.Max_floor];
        for (int i =0;i<Constants.Max_floor;i++)
        {
            if(i==0)
            {
                mFloorProgress[i] = 10;
                mFloorProgressCal[i] = 10;
                Debug.Log((i+1)+"번째 층 " + mFloorProgress[i]);
            }
            else
            {
                mFloorProgressCal[i] = mFloorProgressCal[i - 1] * Constants.GemCostIncrese;
                mFloorProgress[i] = Mathf.Round(mFloorProgressCal[i]); 
                Debug.Log((i + 1) + "번째 층 " + mFloorProgress[i]);
            }
        }
    }
    
    public void Touch()
    {
        int gain = 0;
        mUser.Progress += ManPower;
        //TODO Sound FX
        //TODO VFX +Text Effect


        if (mUser.Progress >= mFloorProgress[mUser.Stage])
        {
            while (mUser.Progress >= mFloorProgress[mUser.Stage])
            {
                mUser.Progress -= mFloorProgress[mUser.Stage];
                mUser.AmoutGem_A[mUser.Stage]++;
                gain++;
            }
        }

        //TextEffect effect = TextEffectPool.Instance.GetFromPool();
        //effect.SetText(gain.ToString());
        ////TODO Icon 변경 effect.SetIcon();
        //effect.transform.position = mTextEffectPos.position;

    }
}
