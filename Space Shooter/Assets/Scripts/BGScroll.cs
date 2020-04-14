﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    private Rigidbody mRB;

    [SerializeField]
    private float mSpeed;
    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody>();
        mRB.velocity = Vector3.back * mSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BGTrigger"))
        {
            transform.position += new Vector3(0, 0, 40.96f);
        }
    }
    //// Update is called once per frame
    //void Update()
    //{
    //    if(transform.position.z <=-15.48)
    //    {
    //        //위치 확인 후 위치 이동
    //    }
    //}
}
