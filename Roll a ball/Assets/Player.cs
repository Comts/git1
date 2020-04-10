using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text기능 시 필요

public class Player : MonoBehaviour
{
    [SerializeField]
    private Text mScoreText, mClearText;
    private Rigidbody mRB;

    [SerializeField]
    private float mSpeed = 5;

    [SerializeField]
    private float mScore;
    // Start is called before the first frame update
    void Start()
    {
        mScore = 0;
        mRB = GetComponent<Rigidbody>();
        //gameObject; transform; 스크립트가 붙어있는 물체
        mScoreText.text = "Score: " + mScore.ToString();
        mClearText.text = "";
    }
    public void AddScore()
    {
        mScore++;
        mScoreText.text = "Score: "+ mScore.ToString();
        if(mScore>=4)
        {
            mClearText.text = "Game Clear!";
        }
        //UI출력
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 axis = new Vector3(horizontal,0,vertical);
        axis = axis.normalized*5;
        axis.y = mRB.velocity.y;

        //mRB.AddForce(axis);
        mRB.velocity = axis;
        //개별변경안됨
        //float velY = mRB.velocity.y;
        //mRB.velocity.y = velY;

    }
}
