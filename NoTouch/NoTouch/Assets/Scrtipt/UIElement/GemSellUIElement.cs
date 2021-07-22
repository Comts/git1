using System;
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
    private double mMaxSellAmount;
    [SerializeField]
    private Toggle mToggle;
#pragma warning restore 0649

    private int mID;
    public void Init(int id,
                     Sprite icon,
                     string title,
                     string contents,
                     Delegates.IntDoubleInVoidCallback callback)
    {
        mID = id;
        mIconImage.sprite = icon;
        mTitleText.text = title;
        mContentsText.text = contents;
        CalSellAmount();

        mButton.onClick.AddListener(() =>
        {
            callback(mID, mSellAmount);
        });
        mToggle.onValueChanged.AddListener((bOn) =>
        {
            GameController.Instance.CheckAutoSell[mID] = bOn;
            if (!bOn)
            {
                GemSellController.Instance.SetAllSellToggle(bOn);
            }
        });

    }
    public void bToggleIsOn(bool f)
    {
        mToggle.SetIsOnWithoutNotify(f);
    }


    public void SetButtonActive(bool isActive)
    {
        mButton.interactable = isActive;
    }
    public void SetSliderActive(bool isActive)
    {

        mSlider.interactable = isActive;
    }
    public void ReSetSlider()
    {

        mSlider.value = 1;
    }
    public double GetMaxSellAmount()
    {
        return mMaxSellAmount;
    }
    public void CalSellAmount()
    {
        int div;
        div = mID % 5;
        switch(div)
        {
            case 0:
                mMaxSellAmount = GameController.Instance.AddAmoutGem_O[mID / 5];
                break;
            case 1:
                mMaxSellAmount = GameController.Instance.AddAmoutGem_A[mID / 5];
                break;
            case 2:
                mMaxSellAmount = GameController.Instance.AddAmoutGem_S[mID / 5];
                break;
            case 3:
                mMaxSellAmount = GameController.Instance.AddAmoutGem_SS[mID / 5];
                break;
            case 4:
                mMaxSellAmount = GameController.Instance.AddAmoutGem_SSS[mID / 5];
                break;
        }
        if (mMaxSellAmount > 0)
        {
            SetButtonActive(true);
            SetSliderActive(true);
        }
        else
        {
            SetButtonActive(false);
            SetSliderActive(false);
        }
        mAmountText.text = mMaxSellAmount.ToString();
        mSellAmount = Math.Round( mSlider.value * mMaxSellAmount);
        mSellAmountText.text = string.Format("{0}", mSellAmount);
    }

}
