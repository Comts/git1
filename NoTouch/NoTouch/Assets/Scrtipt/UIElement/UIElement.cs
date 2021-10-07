using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mIconImage;
    [SerializeField]
    private Text mTitleText, mLevelText, mContentsText, mCostText;
    [SerializeField]
    private Button mButton;
#pragma warning restore 0649

    private int mID;
    public void Init(int id,
                     Sprite icon,
                     string title,
                     string level,
                     string contents,
                     string cost,
                     Delegates.TwoIntInVoidCallback callback)
    {
        mID = id;
        mIconImage.sprite = icon;
        mTitleText.text = title;
        Refresh(level, contents, cost);

        mButton.onClick.AddListener(() =>
        {
            callback(mID, 1);
        });
       
    }
    public void ChangeTitle(string title)
    {
        mTitleText.text = title;
    }
    public void Refresh(string level, string contents, string cost)
    {
        mLevelText.text = level;
        mContentsText.text = contents;
        mCostText.text = cost;
    }

    public void SetButtonActive(bool isActive)
    {
        mButton.interactable = isActive;
    }
    
}
