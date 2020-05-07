using System; // system에도 random이있음  UnityEngine.random사용
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private int mCurrentStage;

    [SerializeField]
    private double mProgressWeight = 1.08d;
    private double mCurrentProgress;
    private double mMaxProgress;

    private double mTouchPower;

    [SerializeField]
    private Gem mCurrentGem;

    [SerializeField]//임시
    private GemPool mGemPool;

    private void Awake()
    {
        
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //TODO Load Save Data
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mCurrentStage = 0;
        mMaxProgress = 10;
        mTouchPower = 1;
        mCurrentGem = mGemPool.GetFromPool(UnityEngine.Random.Range(0, Constants.TOTAL_GEM_COUNT));
    }
    
    private void CalcNextStage()
    {
        mCurrentStage++;
        //mMaxProgress = 10 * Mathf.Pow(mProgressWeight, mCurrentStage); //float
        mMaxProgress = 10 * Math.Pow(mProgressWeight, mCurrentStage); //double  10* 1.08^n

        mCurrentGem.gameObject.SetActive(false);
        mCurrentGem = mGemPool.GetFromPool(UnityEngine.Random.Range(0, Constants.TOTAL_GEM_COUNT));
        //Debug.Log("New Gem");
        //Debug.Log(mMaxProgress);
        mCurrentProgress = 0;
    }

    public void Touch()
    {
        if (mCurrentProgress >= mMaxProgress)
        {
            CalcNextStage();
        }
        else
        {
            mCurrentProgress += mTouchPower;
            // 터치효과 적용 <- 원석의 작업 진행도 증가
            float progress = (float)(mCurrentProgress / mMaxProgress); //이미지 fillamount값이 float
            mCurrentGem.SetProgress(progress);
            //Debug.Log(progress);
            // 작업진행도에 따라 원석의 이미지 업데이트 -Gem
        }
        // 게이지바에 진행도 표시
        UIController.Instance.ShowGaugeBar(mCurrentProgress, mMaxProgress);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
