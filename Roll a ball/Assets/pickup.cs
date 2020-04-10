using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    [SerializeField]
    private Vector3 mTumble;
    private Vector3 mRealTumble;
    private Player mPlayer;
    //// Start is called before the first frame update
    void Start()
    {
        mRealTumble = mTumble * Time.fixedDeltaTime;
        GameObject playerobj = GameObject.FindGameObjectWithTag("Player");//오브젝트 찾을때.
        mPlayer = playerobj.GetComponent<Player>();//오브젝트 찾을때.
        //mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); 동일
    }
    private void FixedUpdate()
    {
        transform.Rotate(mRealTumble);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    transform.Rotate(mTumble*Time.deltaTime); //프레임타임을 곱해줘 맞춰줌.
    //}

    private void OnTriggerEnter(Collider other) // 겹칩 둘중하나  Rigidbody필요  , 둘중 하나만 트리거면 된다. (or case)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false); //활성화 비활성화
            mPlayer.AddScore();
        }//Destroy(gameObject); 가비지 쌓임 안쓰는게 좋다.
    }
    //private void OnCollisionEnter(Collision collision) // 충돌 둘중하나 Rigidbody필요 , 둘다 콜리젼이여야된다.(and case)
    //{
        
    //}
}
