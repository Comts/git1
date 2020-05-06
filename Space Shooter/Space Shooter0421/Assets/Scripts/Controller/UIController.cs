using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text mScoreText,mMessageText,mRestartText;//원래있었던 변수의 순서를 바꾸는건괜찮다 ,이름을 바꾸면 인스턴스에서 끊어짐 다시 연결필요.

    [SerializeField]
    private GameObject[] mLifeObjArr;

    public void ShowLife(int life) //라이프 슬롯기능 베이직
    {
        for(int i =0; i<mLifeObjArr.Length;i++)
        {
            if(i<life)
            {
                mLifeObjArr[i].SetActive(true);
            }
            else
            {
                mLifeObjArr[i].SetActive(false);
            }
            
        }
    }

    public void ShowScore(int amount)
    {
        mScoreText.text = "Score: " + amount.ToString();
    }
    public void ShowMessagetext(string data)
    {
        mMessageText.text = data;
    }
    public void ShowRestart(bool isActive)
    {
        mRestartText.gameObject.SetActive(isActive);
    }
}
