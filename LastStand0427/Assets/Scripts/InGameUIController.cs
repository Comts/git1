using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    [SerializeField]
    private Text mCoinText;
    //[SerializeField]
    //private Image mImage;
    
    //void Start()
    //{
    //    //mImage.sprite =                   //이미지변경
    //}
    
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
