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
    [SerializeField]
    private Transform mHPBarPos;
    private Animator mAnim;
    private Rigidbody2D mRB2D;

    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private int mAtk, mMaxHP;
    private float mCurrentHP;
    private eEnemyState mState;
    private int mDelayCount;
    private InGameController mController;

    [SerializeField]
    private float mReward;

    private Player mTarget;
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
        mDelayCount = 0;
    }
    public void SetIngameController(InGameController controller)
    {
        mController = controller;
    }
    public void StartMoving()
    {
        StartCoroutine(Statemachine());

    }
    public void Attack()
    {
        mTarget.Hit(mAtk);
    }
    public void Hit(float amount)
    {
        mCurrentHP -= amount;
        //Show HPBar
        if(mCurrentHP<=0)
        {
            mState = eEnemyState.Die;
            mDelayCount = 0;
            mController.AddCoin(mReward);
            TextEffect textEffect = mController.GetTextEffect();
            textEffect.ShowText(mReward);
            textEffect.transform.position = mHPBarPos.position;// 월드좌표 카메라
            //textEffect.transform.position = Camera.main.WorldToScreenPoint(mHPBarPos.position);// 오버레이일때  tag가 MainCamera돼어야 Camera.가능 
            //text effect
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(mTarget==null)
            {
               mTarget = collision.gameObject.GetComponent<Player>();
            }
            mAnim.SetBool(AnimHash.Walk, false);
            mState = eEnemyState.Attack;
            mDelayCount = 0;
            //Player p = collision.gameObject.GetComponent<Player>();
            //p.Hit(1);
            //collision.gameObject.SendMessage("Hit",1, SendMessageOptions.DontRequireReceiver); //파라메터 1개가능
        }

    }
    public void AttackFinish()
    {
        mAnim.SetBool(AnimHash.Attack, false);
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
                    if (mDelayCount >= 30)
                    {
                        mAnim.SetBool(AnimHash.Attack, true);
                        //mTarget.Hit(1); 애니메이션 싱크 안맞음
                        mDelayCount = 0;
                    }
                    else
                    {
                        mDelayCount++;
                    }
                    break;
                case eEnemyState.Die:
                    if(mDelayCount==0)
                    {
                        mAnim.SetBool(AnimHash.Dead, true);
                        mDelayCount++;
                        //포인트 획득 이펙트
                    }
                    else if (mDelayCount >= 10)
                    {
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        mDelayCount++;
                    }

                    break;
                default:
                    Debug.LogError("wrong state: " + mState);
                    break;
            }
        }
    }
}
