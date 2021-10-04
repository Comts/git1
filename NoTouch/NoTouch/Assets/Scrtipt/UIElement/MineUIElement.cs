using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineUIElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mIconImage;
    [SerializeField]
    private Text mTitleText, mContentsText, mCostText, mAmountText;
    [SerializeField]
    private Button mBuyButton, mSellButton;
#pragma warning restore 0649

    private int mID;
    public void Init(int id,
                     Sprite icon,
                     string title,
                     string contents,
                     string cost,
                     Delegates.TwoIntInVoidCallback callback, 
                     Delegates.TwoIntInVoidCallback callback2)
    {
        mID = id;
        mIconImage.sprite = icon;
        mTitleText.text = title;
        Refresh(contents, cost);

        mBuyButton.onClick.AddListener(() =>
        {
            callback(mID, 1);
        });
        mSellButton.onClick.AddListener(() =>
        {
            callback2(mID, 1);
        });

    }

    public void Refresh( string contents, string cost)
    {
        mContentsText.text = contents;
        mCostText.text = cost;
    }
    public void ShowAmount(double amount)
    {
        mAmountText.text = UnitSetter.GetUnitStr(amount).ToString();
    }

    public void SetBuyButtonActive(bool isActive)
    {
        mBuyButton.gameObject.SetActive(isActive);
        mSellButton.gameObject.SetActive(!isActive);
    }

}
