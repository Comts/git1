using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    [SerializeField]
    private Text mCoinText;
    [SerializeField]
    private Button mButton;
    //[SerializeField]
    //private Image mImage;

    //void Start()
    //{
    //    //mImage.sprite =                   //이미지변경
    //}
    private void Awake()
    {
        mButton.onClick.AddListener(ShowLog); //델리게이트,액션/ 장.수정하기쉽다./단.파라미터넣을수없다.
        
        //mButton.onClick.AddListener( //람다함수
        //    () =>
        //    {
        //        Debug.Log("haha");

        //    });
    }
    public void ShowLog()
    {
        Debug.Log("Button clicked");
    }
    public void ShowLogString(string value)
    {
        Debug.Log(value);
    }
    public void ShowCoin(float vlaue)
    {
        mCoinText.text = vlaue.ToString();
    }
    //public string,float Coin //ShowCoin과 동일
    //{
    //    set
    //    {
    //        mCoinText.text = value.ToString();
    //    }
    //}
}
