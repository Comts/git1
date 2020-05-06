using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    
    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody>();
        mRB.velocity = transform.forward * mSpeed;
        //mRB.velocity = new Vector3(0, 0, mSpeed);
        //Vector3.forward=vecto(0,0,1) right up one. zero 등등
    } 

    
}
