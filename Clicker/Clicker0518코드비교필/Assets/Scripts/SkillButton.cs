using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField]
    private Button mSkillButton;
    [SerializeField]
    private TextMeshProUGUI mSkillTitleText;
    [SerializeField]
    private Image mCooldoenImage;
    [SerializeField]
    private TextMeshProUGUI mCooldownText;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    mSkillButton.onClick.AddListener(() => { }); //인스펙터에서 설정
    //}

      
    public void SetButtonText(string title)
    {
        mSkillTitleText.text = title;
    }
    public void SetButtonActive(bool isActive)
    {
        mSkillButton.interactable = isActive;
    }
    public void ShowCooltime(float currentTime, float maxTime)
    {
        if(currentTime>0)
        {
            mCooldoenImage.gameObject.SetActive(true);
            mCooldoenImage.fillAmount = currentTime / maxTime;
            int min = (int)(currentTime / 60f);
            int sec = (int)(currentTime % 60f);

            mCooldownText.text = string.Format("{0} : {1}", min.ToString("d2"), sec.ToString("d2"));

        }
        else
        {
            mCooldoenImage.gameObject.SetActive(false);
        }
    }
}
