using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyState
{
    Idel,
    Walk,
    Attack,
    Die
}

public class Enemy : MonoBehaviour
{
    private Animator mAnim;
    private Rigidbody2D mRB2D;

    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private int mAtk, mMaxHP;
    private int mCurrentHP;
    private eEnemyState mState;
    private int mDelayCount;

    private void Awake()
    {
        mAnim = GetComponent<Animator>();
        // mAnim.SetBool(AnimHash.Attack, true);
        mRB2D = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        mAnim.SetBool(AnimHash.Dead, false);
        mCurrentHP = mMaxHP;
        mState = eEnemyState.Idel;
        StartCoroutine(Statemachine());
    }

    private IEnumerator Statemachine()
    {
        WaitForSeconds PointOne = new WaitForSeconds(.1f);
        while(true)
        {
            yield return PointOne;

            switch(mState)
            {
                case eEnemyState.Idel:
                    if(mDelayCount>20)
                    {
                        mState = eEnemyState.Walk;
                        mAnim.SetBool(AnimHash.Walk, true);
                        //방향
                        //속도
                        mRB2D.velocity = transform.right * mSpeed;
                        mDelayCount = 0;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eEnemyState.Walk:
                    if (mDelayCount > 10)
                    {
                        mState = eEnemyState.Idel;
                        mAnim.SetBool(AnimHash.Walk, false);
                        mRB2D.velocity = Vector2.zero;
                        mDelayCount = 0;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eEnemyState.Attack:
                    break;
                case eEnemyState.Die:
                    break;
                default:
                    Debug.LogError("wrong state: " + mState);
                    break;
            }
        }
    }
}
