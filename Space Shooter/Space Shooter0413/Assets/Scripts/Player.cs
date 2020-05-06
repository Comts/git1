using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Fire bolt")]
    [SerializeField]
    private BoltPool mBoltPool;
    [SerializeField]
    private Transform mBoltPos;
    [SerializeField]
    private float mFireLate;
    private float mCurrnetFireLate;

    //[Space] //간격벌리기
    [Header("Movement")]
    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private float mXMin, mXMax, mZMax, mZMin;
    [SerializeField]
    private float mTilted = 30;
    private Rigidbody mRB;
    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody>();
        mCurrnetFireLate = mFireLate;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = direction.normalized;
        mRB.velocity = direction * mSpeed;
        //transform.Translate(direction * Time.deltaTime); //잘사용안함 리짓바디안쓸때사용

        mRB.rotation = Quaternion.Euler(0, 0, horizontal * -mTilted);

        mRB.position = new Vector3( Mathf.Clamp(mRB.position.x, mXMin, mXMax), 
                                    0,
                                    Mathf.Clamp(mRB.position.z, mZMin, mZMax));


        if (Input.GetButton("Fire1") && mCurrnetFireLate >= mFireLate) //if (Input.GetKeyDown(KeyCode.Space))
        {
            //GameObject obj  = Instantiate(Bolt);  
            Bolt blot =mBoltPool.GetFromPool();
            blot.gameObject.transform.position = mBoltPos.position; //월드 좌표
            //mBoltPos.localPosition // 오브젝트의 하위
            mCurrnetFireLate = 0;
        }
        else
        {
            mCurrnetFireLate += Time.deltaTime; //deltatime = 프레임간 시간간격
        }
    }
}
