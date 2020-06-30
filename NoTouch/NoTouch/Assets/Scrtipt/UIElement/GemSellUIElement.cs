using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemSellUIElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mIconImage;
    [SerializeField]
    private Text mTitleText, mAmountText, mContentsText, mSellAmountText;
    [SerializeField]
    private Button mButton;
    [SerializeField]
    private Slider mSlider;
    private double mSellAmount;
#pragma warning restore 0649

    private int mID;
    public void Init(int id,
                     Sprite icon,
                     string title,
                     string amount,
                     string contents,
                     Delegates.IntDoubleInVoidCallback callback)
    {
        mID = id;
        mIconImage.sprite = icon;
        mTitleText.text = title;
        CalSellAmount();

        mButton.onClick.AddListener(() =>
        {
            callback(mID, mSellAmount);
        });

    }


    public void SetButtonActive(bool isActive)
    {
        mButton.interactable = isActive;
    }

    public void CalSellAmount()
    {
        int div;
        div = mID % 5;
        switch(div)
        {
            case 0:
                mSellAmount = GameController.Instance.AddAmoutGem_O[mID / 5];
                break;
            case 1:
                mSellAmount = GameController.Instance.AddAmoutGem_A[mID / 5];
                break;
            case 2:
                mSellAmount = GameController.Instance.AddAmoutGem_S[mID / 5];
                break;
            case 3:
                mSellAmount = GameController.Instance.AddAmoutGem_SS[mID / 5];
                break;
            case 4:
                mSellAmount = GameController.Instance.AddAmoutGem_SSS[mID / 5];
                break;
        }
        mAmountText.text = mSellAmount.ToString();
        mSellAmountText.text = (mSlider.value * mSellAmount).ToString();
    }

}
