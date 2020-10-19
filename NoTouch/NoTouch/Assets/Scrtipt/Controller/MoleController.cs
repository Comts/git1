using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleController : MonoBehaviour
{
    public static MoleController Instance;
#pragma warning disable 0649
    [SerializeField]
    private Mole[] MoleArr;
    [SerializeField]
    private Text mTime,mScore;
    [SerializeField]
    private Transform mMoleWindow;
    [SerializeField]
    private Transform mFinishWindow;
    [SerializeField]
    private float mPlayTime;
    [SerializeField]
    private int mSpwanMoleCount;
    [SerializeField]
    private Text mMoneyText;
    [SerializeField]
    private int mMinMulti,mMaxMulti;
#pragma warning restore 0649
    private int Score;
    private int count;
    private double mGold;
    private double mRandom;
    private float currentTime;
    public int MoleCount
    {
        get { return count; }
        set
        {
            count = value;
        }
    }
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
    private void Start()
    {
        count = 0;
        Score = 0;
    }
    private void FixedUpdate()
    {
        if (mMoleWindow.gameObject.activeInHierarchy)
        {
            Showtime();
        }

    }
    public void Showtime()
    {
        if (currentTime > 0)
        {
            StartCoroutine(SpwanMole());
            currentTime -= Time.deltaTime;
            int sec = (int)(currentTime);
            int msec = (int)((currentTime % 1)*100);

            mTime.text = string.Format("남은시간 {0}:{1}",
                                      sec.ToString("D2"),
                                      msec.ToString("D2"));
            if(Score==0)
            {
                mRandom = UnityEngine.Random.Range(1, 1.5f) * GameController.Instance.GetGemCost[GameController.Instance.Stage] * UnityEngine.Random.Range(mMinMulti, mMaxMulti) *(GameController.Instance.ManPower/ GameController.Instance.GetRequireProgress[GameController.Instance.Stage]);
                Debug.Log("GetGemCost" + GameController.Instance.GetGemCost[GameController.Instance.Stage]);
                Debug.Log("ManPower" + GameController.Instance.ManPower);
                Debug.Log("Progerss" + GameController.Instance.GetRequireProgress[GameController.Instance.Stage]);
                Debug.Log("ManPower/Progerss" + (GameController.Instance.ManPower / GameController.Instance.GetRequireProgress[GameController.Instance.Stage]));
            }
        }
        else
        {
            calMoney();
            mFinishWindow.gameObject.SetActive(true);
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
    public void calMoney()
    {
        mGold = Math.Round(mRandom * Score);
        mMoneyText.text = UnitSetter.GetUnitStr(mGold);
    }   
    public void AddMoney(int mutiply)
    {
        GameController.Instance.Gold+=(mGold* mutiply);
        Score = 0;
    }
    private IEnumerator SpwanMole()
    {
        if (count < mSpwanMoleCount)
        {
            int pos = UnityEngine.Random.Range(0, MoleArr.Length);
            while (MoleArr[pos].gameObject.activeInHierarchy)
            {
                pos = UnityEngine.Random.Range(0, MoleArr.Length);
            }
            MoleArr[pos].gameObject.SetActive(true);
            count++;
        }
        yield return new WaitForSeconds(2);
    }

}
