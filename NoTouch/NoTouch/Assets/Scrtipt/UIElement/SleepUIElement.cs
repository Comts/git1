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
    private Text mAmountText;
#pragma warning restore 0649

    public void Init(Sprite icon, string Text)
    {
        mIconImage.sprite = icon;
        mAmountText.text = Text;
       
    }
    
}
