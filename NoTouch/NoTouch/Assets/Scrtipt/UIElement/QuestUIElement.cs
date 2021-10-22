using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Button mButton;
    [SerializeField]
    private GaugeBar mGaugeBar;
    [SerializeField]
    private Text mTitleText, mContentsText,mButtonText;
    [SerializeField]
    private Sprite GoldDazi, SilverDazi;
    [SerializeField]
    private Image mButtonImage, mQuestImage;
#pragma warning restore 0649
    // Start is called before the first frame update

    // Update is called once per frame
    private int mID, mItemNum,NextID, mRequire, mAmount;
    string DaziName;
    int DaziAmount;
    public void Init(   int id , 
                        Sprite Q_image,
                        int ItemNum,
                        int Itemamount,
                        int Require,
                        string title,
                        string contents,
                        Delegates.VoidCallback callback)
    {
        mID = id;
        mQuestImage.sprite = Q_image;
        mItemNum = ItemNum;
        mAmount = Itemamount;
        NextID = mID + 1;
        mRequire = Require;
        mTitleText.text = title;
        mContentsText.text = contents;
        mButtonText.text = string.Format("{0} 개 받기", mAmount);
        DaziAmount = mAmount;
        if (mItemNum == 0)
        {
            mButtonImage.sprite = SilverDazi;
            DaziName = "실버다지";
        }
        else
        {
            mButtonImage.sprite = GoldDazi;
            DaziName = "골드다지";
        }

        mButton.onClick.AddListener(() =>
        {
            callback();
        });
    }
    public int GetRequire()
    {
        return mRequire;
    }
    public int GetDaizAmount()
    {
        return DaziAmount;
    }
    public string GetDaziName()
    {
        return DaziName;
    }
    public int GetAward()
    {
        GameController.Instance.HaveItem[mItemNum] += mAmount;
        ItemUseController.Instance.ShowHaveItem();
        return NextID;
    }
    public void Reset_Button()
    {
        mGaugeBar.gameObject.SetActive(true);
        mButton.gameObject.SetActive(false);
    }
    public void ClearQuest()
    {
        mGaugeBar.gameObject.SetActive(false);
        mButton.gameObject.SetActive(true);
        SoundController.Instance.FXSound(12);
        PointController.Instance.ShowQuestPoint();
    }
    public void AllClearQuest()
    {
        gameObject.SetActive(true);
        mGaugeBar.gameObject.SetActive(false);
        mButton.gameObject.SetActive(false);
    }
    public void ShowGaugeBar(double current, double max)
    {
        mGaugeBar.gameObject.SetActive(true);
        string progressStr = string.Format("{0} / {1}",
                                            current,
                                            max);
        float progress = (float)(current / max);
        mGaugeBar.ShowGaugeBar(progress, progressStr);
    }
}
