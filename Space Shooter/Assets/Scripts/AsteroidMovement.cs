using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private float mTorque, mSpeed;
    // Start is called before the first frame update
    private void Awake()
    {
        mRB = GetComponent<Rigidbody>();
        //mRB.AddTorque 회전력관련 코드 각속도

    }
    private void OnEnable()
    {

        mRB.angularVelocity = Random.onUnitSphere*mTorque;//단위구 반지름1자리 랜덤
        //mRB.angularVelocity = Random.insideUnitSphere;//단위구 반지름0~1 랜덤
        mRB.velocity = Vector3.back * mSpeed;
    }

    void Start()
    {


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag ("Bolt")||
           other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            //add score
            //add effect
            //add sound
            other.gameObject.SetActive(false);
        }
    }
}
