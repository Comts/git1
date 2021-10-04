using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffUIElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mIconImage;
    [SerializeField]
    private Text mTitleText,mContentsText;
#pragma warning restore 0649
    void Start()
    {
    }
    public void Init(Sprite icon, string title,string contents)
    {
        Color color = new Color(1, 1, 1, 0.5f);
        Color color_B = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        mIconImage.sprite = icon;
        mTitleText.text = title;
        mContentsText.text = contents;
        mIconImage.color = color;
        mTitleText.color = color_B;
        mContentsText.color = color_B;
    }

    public void ShowBuff(Sprite icon)
    {
        mIconImage.sprite = icon;
        Color color = new Color(0, 0, 0, 1);
        Color color_R = new Color(1, 0.3f, 0.3f, 1);

        mIconImage.color += color;
        mTitleText.color = color_R;
        mContentsText.color = color_R;
    }

    
}
