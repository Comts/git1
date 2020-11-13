﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //씬연결
public class GameController : MonoBehaviour
{
    private float mGold=100;
    public void UseGold(float amount, Delegates.VoidCallback callback = null)
    {
        if(mGold>=amount)
        {
            mGold -= amount;
            if(callback!=null)
            {
                callback();
            }

        }
        else
        {
            Debug.Log("Not enough gold " + amount);
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    //public Delegates.VoidCallback GoldSpendCallback { get; set; }
    //public float Gold
    //{
    //    get
    //    {
    //        return mGold;
    //    }
    //    set
    //    {
    //        if (value >= 0)
    //        {
    //            mGold = value;
    //            if (GoldSpendCallback != null)
    //            {
    //                GoldSpendCallback();
    //                GoldSpendCallback = null;
    //            }
    //        }
    //        else
    //        {
    //            GoldSpendCallback = null;
    //        }
    //    }
    //}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}