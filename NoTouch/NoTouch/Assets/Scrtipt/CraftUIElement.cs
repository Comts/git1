using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftUIElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mIconImage;
    [SerializeField]
    private Text mTitleText, mContentsText, mAmonutText, mConsumeText;
    [SerializeField]
    private Button mCraftButton;
#pragma warning restore 0649
    private int mID;
    public void Init(int id,
                     Sprite icon,
                     string title,
                     string contents,
                     string cost,
                     Delegates.TwoIntInVoidCallback callback)
    {
        mID = id;
        mIconImage.sprite = icon;
        mTitleText.text = title;
        mContentsText.text = contents;
        mConsumeText.text = cost;

        mCraftButton.onClick.AddListener(() =>
        {
            callback(mID, 1000);
            UIController.Instance.Popwindow(4);
        });

    }


    public void SetCraftButtonActive(bool isActive)
    {
        mCraftButton.interactable = isActive;
    }
}
