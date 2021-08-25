using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coworker : MonoBehaviour
{
    [SerializeField]
    private int mID;
    [SerializeField]
    private float mWorkPeriod;
    [SerializeField]
    private float mCurrentTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartWork(int id, float period)
    {
        mID = id;
        mWorkPeriod = period;
        mCurrentTime = 0;
    }
    public void StopWork()
    {
        mWorkPeriod = 0;
        mCurrentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (mWorkPeriod > 0)
        {
            mCurrentTime += Time.deltaTime;;
            if (mCurrentTime >= mWorkPeriod)
            {
                CoworkerController.Instance.JobFinish(mID);//TODO FX, mTextEffectPos.position);
                mCurrentTime = mCurrentTime - mWorkPeriod;
            }
        }

    }
}
