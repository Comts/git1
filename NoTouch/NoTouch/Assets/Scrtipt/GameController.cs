﻿using System;
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
    private float WorkIncrese = 1.2f;
    [SerializeField]
    private double GemCostIncrese = 1.3;
#pragma warning restore 0649
    private float[] mFloorProgress, mFloorProgressCal;
    private double[] mFloorGemCost, mFloorGemCostCal;
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
    public double[] AddAmoutGem_A
    {
        get { return mUser.AmoutGem_A; }
        set
        {
                mUser.AmoutGem_A = value;
        }
    }
    public int Stage
    {
        get { return mUser.Stage; }
        set
        {
            mUser.Stage = value;
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
    public int[] HaveMine
    {
        get { return mUser.MineArr; }
        set
        {
            mUser.MineArr = value;
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
        mFloorProgress = new float[Constants.MAX_fLOOR];
        mFloorProgressCal = new float[Constants.MAX_fLOOR];
        mFloorGemCost = new double[Constants.MAX_fLOOR];
        mFloorGemCostCal = new double[Constants.MAX_fLOOR];
        for (int i =0;i<Constants.MAX_fLOOR;i++)
        {
            if(i==0)
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
                mFloorProgress[i] = Mathf.Round(mFloorProgressCal[i]);
                mFloorGemCostCal[i] = mFloorGemCost[i - 1] * GemCostIncrese;
                mFloorGemCost[i] = Math.Round(mFloorGemCostCal[i],1);
                //Debug.Log((i) + "번째 층 노동력" + mFloorProgress[i]);
                //Debug.Log((i) + "번째 층 원석가격" + mFloorGemCost[i]);
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
    //public void AddAmoutGem_A(int id,double amount)
    //{
    //    mUser.AmoutGem_A[id] += amount; ;
    //}
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
