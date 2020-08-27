﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleController : MonoBehaviour
{
    public static MoleController Instance;
    [SerializeField]
    private Transform[] MoleHillArr;
    [SerializeField]
    private Text mTime,mScore;
    [SerializeField]
    private MolePool mMolePool;
    [SerializeField]
    private Transform mMoleWindow;
    [SerializeField]
    private float mPlayTime;
    [SerializeField]
    private int mMoleCount;
    private int Score;
    private float currentTime;
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
    private void Update()
    {
        if (mMoleWindow.gameObject.activeInHierarchy)
        {
            Showtime();
            if (mMolePool.CountCheck() <= mMoleCount)
            {
                SpwanMole();
            }
        }
    }
    public void Showtime()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            int sec = (int)(currentTime);
            int msec = (int)((currentTime % 1)*100);

            mTime.text = string.Format("남은시간 {0}:{1}",
                                      sec.ToString("D2"),
                                      msec.ToString("D2"));
        }
        else
        {
            mMoleWindow.gameObject.SetActive(false);
            Score = 0;
        }
    }
    public void ShowScore()
    {
        mScore.text = string.Format("점수 : {0}",
                                      Score.ToString("D2"));
    }
     
    public void WhackaMole()
    {
        currentTime = mPlayTime; 
        ShowScore();
    }
    public void AddScore()
    {
        Score++;
        ShowScore();
    }
    public void SpwanMole()
    {
        int Pos;
        Mole mole = mMolePool.GetFromPool();
        do
        {
            Pos = Random.Range(0, MoleHillArr.Length);
        } while (!(MoleHillArr[Pos].gameObject.GetComponentInChildren(typeof(Mole), true)));
        mole.transform.position = MoleHillArr[Pos].transform.position;
    }   
}
