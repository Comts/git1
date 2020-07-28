using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddStageUIElement : MonoBehaviour
{
    [SerializeField]
    private Image mGemImage;
    [SerializeField]
    private Text mTitleText, mContentsText, mCostText;
    [SerializeField]
    private Button mAddButton;

    private int mID;
    public void Init(int id,
                     Sprite icon,
                     string title,
                     string contents,
                     string cost,
                     Delegates.TwoIntInVoidCallback callback)
    {
        mID = id;
        mGemImage.sprite = icon;
        mTitleText.text = title;
        Refresh(contents, cost);

        mAddButton.onClick.AddListener(() =>
        {
            callback(mID, 1);
        });
    }
    public void Refresh(string contents, string cost)
    {
        mContentsText.text = contents;
        mCostText.text = cost;
    }
}
