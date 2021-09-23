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
    [SerializeField]
    private Button mMolebutton;
    [SerializeField]
    private Text mPlayCountText;
#pragma warning restore 0649
    private int Score;
    private int Molecount;
    private double mGold;
    private double mRandom;
    private float currentTime;
    public int MoleCount
    {
        get { return Molecount; }
        set
        {
            Molecount = value;
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
        Molecount = 0;
        Score = 0;
        CheckPlayButton();
    }

    public void CheckPlayButton()
    {
        mPlayCountText.text = GameController.Instance.PlayMoleCount.ToString();
        if (GameController.Instance.PlayMoleCount > 0)
        {
            mMolebutton.interactable = true;
        }
        else
        {
            mMolebutton.interactable = false;
        }
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
                //Debug.Log("GetGemCost" + GameController.Instance.GetGemCost[GameController.Instance.Stage]);
                //Debug.Log("ManPower" + GameController.Instance.ManPower);
                //Debug.Log("Progerss" + GameController.Instance.GetRequireProgress[GameController.Instance.Stage]);
                //Debug.Log("ManPower/Progerss" + (GameController.Instance.ManPower / GameController.Instance.GetRequireProgress[GameController.Instance.Stage]));
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
        GameController.Instance.PlayMoleCount--;
        CheckPlayButton(); ;
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
        GameController.Instance.WhackCount += Score;
        GameController.Instance.Gold+=(mGold* mutiply);
        Score = 0;
        Quest_MoleCount.Instance.CheckQuest();
    }
    private IEnumerator SpwanMole()
    {
        if (Molecount < mSpwanMoleCount)
        {
            int pos = UnityEngine.Random.Range(0, MoleArr.Length);
            while (MoleArr[pos].gameObject.activeInHierarchy)
            {
                pos = UnityEngine.Random.Range(0, MoleArr.Length);
            }
            MoleArr[pos].gameObject.SetActive(true);
            Molecount++;
        }
        yield return new WaitForSeconds(2);
    }

}
