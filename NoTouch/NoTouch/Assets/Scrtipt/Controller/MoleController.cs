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
    private float mPlayTime;
    [SerializeField]
    private int mSpwanMoleCount;
#pragma warning restore 0649
    private int Score;
    private int count;
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
    }
    private void FixedUpdate()
    {
        if (mMoleWindow.gameObject.activeInHierarchy)
        {
            Showtime();
            StartCoroutine (SpwanMole());
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
    private IEnumerator SpwanMole()
    {
        if (count < mSpwanMoleCount)
        {
            int pos = Random.Range(0, MoleArr.Length);
            while (MoleArr[pos].gameObject.activeInHierarchy)
            {
                pos = Random.Range(0, MoleArr.Length);
            }
            MoleArr[pos].gameObject.SetActive(true);
            count++;
        }
        yield return new WaitForSeconds(2);
    }

}
