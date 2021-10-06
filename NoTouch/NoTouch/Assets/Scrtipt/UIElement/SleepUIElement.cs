using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepUIElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mIconImage;
    [SerializeField]
    private Text mTitleText, mGemNameText,mAmountText;
#pragma warning restore 0649

    public void Init(Sprite icon, string Title, string GemName, string Amount)
    {
        mIconImage.sprite = icon;
        mTitleText.text = Title;
        mGemNameText.text = GemName;
        mAmountText.text = string.Format("{0} 개", Amount);
       
    }
    
}
