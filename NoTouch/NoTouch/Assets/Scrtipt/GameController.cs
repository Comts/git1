using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
    [SerializeField]
    private float GemCostIncrese = 1.2f;
#pragma warning restore 0649
    private float[] mFloorProgress, mFloorProgressCal;
    [SerializeField]
    private double mManPower;
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
        if(mUser.Stage<mUser.PlayerPos)
        {
            mUser.PlayerPos = mUser.Stage;
        }
        //CalManPower();
        mFloorProgress = new float[Constants.Max_floor];
        mFloorProgressCal = new float[Constants.Max_floor];
        for (int i =0;i<Constants.Max_floor;i++)
        {
            if(i==0)
            {
                mFloorProgress[i] = 10;
                mFloorProgressCal[i] = 10;
                //Debug.Log((i)+"번째 층 " + mFloorProgress[i]);
            }
            else
            {
                mFloorProgressCal[i] = mFloorProgressCal[i - 1] * GemCostIncrese;
                mFloorProgress[i] = Mathf.Round(mFloorProgressCal[i]); 
                //Debug.Log((i ) + "번째 층 " + mFloorProgress[i]);
            }
        }

    }
    private void OnApplicationQuit()
    {
        Save();
    }

    public int[] GetPlayerLevelArr()
    {
        return mUser.PlayerLevelArr;
    }
    public float[] GetSkillCooltimeArr()
    {
        return mUser.SkillCooltimeArr;
    }
    public float[] GetSkillMaxCooltimeArr()
    {
        return mUser.SkillMaxCooltimeArr;
    }
    public int[] GetCoworkerLevelArr()
    {
        return mUser.CoworkerLevelArr;
    }
    public void Touch()
    {
        int gain = 0;
        mUser.Progress += mManPower;
        //TODO Sound FX
        //TODO VFX +Text Effect

        
        while (mUser.Progress >= mFloorProgress[mUser.PlayerPos])
        {
            mUser.Progress -= mFloorProgress[mUser.PlayerPos];
            gain++;
        }
        mUser.AmoutGem_A[mUser.PlayerPos] +=gain;

        //TextEffect effect = TextEffectPool.Instance.GetFromPool();
        //effect.SetText(gain.ToString());
        ////TODO Icon 변경 effect.SetIcon();
        //effect.transform.position = mTextEffectPos.position;

    }
    private void Update()
    {
    }
}
