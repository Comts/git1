using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Button mItemUseButton;
    [SerializeField]
    private Text mItemTitleText;
    [SerializeField]
    private Image mCooldownImage;
    [SerializeField]
    private Text mCooldownText;
#pragma warning restore 0649
    public void SetButtonText(string title)
    {
        mItemTitleText.text = title;
    }

    public void SetButtonActive(bool isActive)
    {
        mItemUseButton.interactable = isActive;
    }
    public void ShowCooltime(float currentTime, float maxTime)
    {
        if (currentTime > 0)
        {
            mCooldownImage.gameObject.SetActive(true);
            mCooldownImage.fillAmount = currentTime / maxTime;
            int min = (int)(currentTime / 60f);
            int sec = (int)(currentTime % 60f);

            mCooldownText.text = string.Format("{0} : {1}",
                                            min.ToString("D2"),
                                            sec.ToString("D2"));
        }
        else
        {
            mCooldownImage.gameObject.SetActive(false);
        }
    }
}
