﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private float mTorque, mSpeed;
    private EffectPool mEffectPool;
    private GameController mGameController;

    private void Awake()
    {
        mRB = GetComponent<Rigidbody>();
        GameObject effectPool = GameObject.FindGameObjectWithTag("EffectPool");  //가져와서 넣어줌.
        mEffectPool = effectPool.GetComponent<EffectPool>();
        mGameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnEnable()
    {
        mRB.angularVelocity = Random.insideUnitSphere * mTorque;
        mRB.velocity = Vector3.back * mSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //mRB.angularVelocity = Random.onUnitSphere;
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Bolt") ||
    //        other.gameObject.CompareTag("Player"))
    //    {
    //        gameObject.SetActive(false);
    //        //Add score
    //        //Add effect
    //        //Add sound
    //        other.gameObject.SetActive(false);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        bool isBolt = other.gameObject.CompareTag("Bolt");
        bool isPlayer = other.gameObject.CompareTag("Player");
        if (isBolt || isPlayer)
        {
            gameObject.SetActive(false);
            //Add score
            mGameController.AddScore(1);
            //add effect
            Timer effect = mEffectPool.GetFromPool((int)eEffectType.ExpAst);
            effect.transform.position = transform.position;
            //Add sound
            if (isBolt)
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
