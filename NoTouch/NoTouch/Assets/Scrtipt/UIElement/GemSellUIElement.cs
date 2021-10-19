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
    private Text mTitleText, mContentsText, mAmountText, mSellAmountText;
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
                     Delegates.IntDoubleInVoidCallback callback,
                     Image LayerPoint,
                     Image SellPoint)
    {
        mID = id;
        mIconImage.sprite = icon;
        mTitleText.text = title;
        mContentsText.text = contents;
        CalSellAmount();

        mButton.onClick.AddListener(() =>
        {
            callback(mID, mSellAmount);
            if (SellPoint.gameObject.activeInHierarchy)
            {
                GameController.Instance.GemSellTutorial = 1;
                LayerPoint.gameObject.SetActive(false);
                SellPoint.gameObject.SetActive(false);
            }
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
    public bool bToggleState()
    {
        return mToggle.isOn;
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
        div = mID % 4;
        switch(div)
        {
            case 0:
                mMaxSellAmount = GameController.Instance.AddAmoutGem_O[mID / 4];
                break;
            case 1:
                mMaxSellAmount = GameController.Instance.AddAmoutGem_B[mID / 4];
                break;
            case 2:
                mMaxSellAmount = GameController.Instance.AddAmoutGem_A[mID / 4];
                break;
            case 3:
                mMaxSellAmount = GameController.Instance.AddAmoutGem_S[mID / 4];
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
        mAmountText.text = UnitSetter.GetUnitStr(mMaxSellAmount).ToString();
        mSellAmount = Math.Round( mSlider.value * mMaxSellAmount);
        mSellAmountText.text = UnitSetter.GetUnitStr(mSellAmount).ToString();
    }

}
