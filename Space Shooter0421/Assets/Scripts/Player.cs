using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int mMaxLife,mCurrentLife;
    private EffectPool mEffectPool;
    private GameController mGameController;
    private SoundController mSoundController;
    private UIController mUIController;
    
    [Header("Fire bolt")]
    [SerializeField]
    private BoltPool mBoltPool;
    [SerializeField]
    private Transform mBoltPos;
    [SerializeField]
    private float mFireLate;
    private float mCurrentFireLate;
    private int mBoltIndex;
    [SerializeField]
    private int mMaxBoltCount;
    [SerializeField]
    private float mBoltXGap;
    [SerializeField]
    private int mCurrentBoltCount=1;
    private Coroutine mBoltChangeRoutine;

    private Rigidbody mRB;
    [Header("Movement")]
    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private float mXMax, mXMin, mZMax, mZMin;
    [SerializeField]
    private float mTilted = 30;
    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody>();
        GameObject effectPool = GameObject.FindGameObjectWithTag("EffectPool");  //가져와서 넣어줌.
        mEffectPool = effectPool.GetComponent<EffectPool>();
        //mGameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        mSoundController = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundController>();
    }
    public void Init(GameController gameController,UIController uiController) //게임컨트롤러가 플레이어를 알고있고 시작할떄 초기화
    {
        mGameController = gameController;
        mUIController = uiController;
        mUIController.ShowLife(mCurrentLife); //같은 start일떄 ui가 안될수도있다.
        mCurrentFireLate = mFireLate;

    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = direction.normalized;

        mRB.velocity = direction * mSpeed;
        //transform.Translate(direction * Time.deltaTime);

        mRB.rotation = Quaternion.Euler(0, 0, horizontal * - mTilted);

        mRB.position = new Vector3(Mathf.Clamp(mRB.position.x, mXMin, mXMax), 
                                   0,
                                   Mathf.Clamp(mRB.position.z, mZMin, mZMax));

        if(Input.GetButton("Fire1") && mCurrentFireLate >= mFireLate)
        {
            float currentXStart = - mBoltXGap * ((mCurrentBoltCount-1)/2); //총알배치
            Vector3 XPos = new Vector3(currentXStart, 0, 0);
 
            for (int i = 0; i < mCurrentBoltCount; i++)
            {
                Bolt bolt = mBoltPool.GetFromPool(mBoltIndex);
                bolt.gameObject.transform.position = mBoltPos.position+XPos;
                bolt.gameObject.transform.rotation = mBoltPos.rotation;
                bolt.ReSetDir();
                XPos.x += mBoltXGap;
            }
            mCurrentFireLate = 0;
            mSoundController.PlayEffectSound((int)eSFXType.FirePlayer);
        }
        else
        {
            mCurrentFireLate += Time.deltaTime;
        }
    }
    //private IEnumerator UpdataLike()//10초 동안만 동작하는코루틴
    //{
    //    float time = 10;
    //    while(time>0)
    //    {
    //        yield return new WaitForFixedUpdate();
    //        time -= Time.fixedDeltaTime;
    //    }
    //}

    public void StartHoming(float time)
    {
        mBoltChangeRoutine=StartCoroutine(ChangeBoltID(1, time));
    }

    private IEnumerator ChangeBoltID(int id,float gap)
    {
        if (mBoltChangeRoutine != null)
        {
            StopCoroutine(mBoltChangeRoutine);
        }
        mBoltIndex = id;
        yield return new WaitForSeconds(gap);
        mBoltIndex = 0;
        mBoltChangeRoutine = null;
    }

    public void AddBoltCount()
    {
        if(mCurrentBoltCount<mMaxBoltCount)
        {
            mCurrentBoltCount++;
        }
    }
    public void AddLife()
    {
        if(mCurrentLife<mMaxLife)
        {
            mCurrentLife++;
            //UI 체력
            mUIController.ShowLife(mCurrentLife);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBolt"))
        {
            mCurrentLife--;
            //UI 체력
            mUIController.ShowLife(mCurrentLife);
            if (mCurrentLife <= 0)
            {
                gameObject.SetActive(false);
                mGameController.PlayerDie();
                //Add score
                //Add effect
                Timer effect = mEffectPool.GetFromPool((int)eEffectType.ExpPlayer);
                effect.transform.position = transform.position;
                //Add sound
                mSoundController.PlayEffectSound((int)eSFXType.ExpPlayer);
            }
        }
    }
}