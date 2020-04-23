﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator mAnim;
    //private Rigidbody2D mRB2D;
    [SerializeField]
    private float mJumpForce;
    //[SerializeField]
    //private int AttackHash;
    // Start is called before the first frame update
    void Start()
    {
        //SpriteRenderer rand = GetComponent<SpriteRenderer>();
        //rand.sortingOrder = 10;    


        //AttackHash = Animator.StringToHash("IsAttack");
        mAnim = GetComponent<Animator>();
        //mAnim.SetBool(AnimHash.Attack, true);
        //mAnim.SetBool("IsAttack", true);
        //mAnim.SetBool(AttackHash, true);
        //mRB2D = GetComponent<Rigidbody2D>(); 리짓바디있으면 밀림.
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.identity;//rotation (0,0,0)
        }



        if (Input.GetButtonDown("Fire1"))
        {
            mAnim.SetBool(AnimHash.Attack, true);
        }
        else if (Input.GetButtonUp("Fire1"))
        {

            mAnim.SetBool(AnimHash.Attack, false);
        }
        //if(Input.GetButtonDown("Jump"))
        //{
        //    mAnim.SetBool("IsJump", true);
        //    mRB2D.velocity = Vector2.up * mJumpForce;
        //}
        //mAnim.SetFloat("JumpVel", mRB2D.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))//&& collision.enabled)
        {
            mAnim.SetBool("IsJump", false);
        }
    }
}
