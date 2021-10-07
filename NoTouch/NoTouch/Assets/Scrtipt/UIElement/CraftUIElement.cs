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
    private Text mTitleText, mContentsText, mAmonutText;
    [SerializeField]
    private Button mCraftButton;
#pragma warning restore 0649
    private int mID;
    public void Init(int id,
                     Sprite icon,
                     string title,
                     string contents,
                     Delegates.TwoIntInVoidCallback callback)
    {
        mID = id;
        mIconImage.sprite = icon;
        mTitleText.text = title;
        mContentsText.text = contents;

        mCraftButton.onClick.AddListener(() =>
        {
            callback(mID, 10000);
            UIController.Instance.Popwindow(4);
        });

    }
    private void Update()
    {

        mAmonutText.text = string.Format("{0} / 1만",UnitSetter.GetUnitStr(GameController.Instance.AddAmoutGem_O[mID]).ToString());
    }

    public void SetCraftButtonActive(bool isActive)
    {
        mCraftButton.interactable = isActive;
    }
}
