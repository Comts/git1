using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIElement : MonoBehaviour
{
    [SerializeField]
    private Button mButton;
    [SerializeField]
    private GaugeBar mGaugeBar;
    [SerializeField]
    private Text mTitleText, mContentsText;
    // Start is called before the first frame update

    // Update is called once per frame
    private int mID, mItemNum,NextID, mRequire, mAmount;
    public void Init(   int id , 
                        int ItemNum,
                        int Itemamount,
                        int Require,
                        string title,
                        string contents,
                        Delegates.VoidCallback callback)
    {
        mID = id;
        mItemNum = ItemNum;
        mAmount = Itemamount;
        NextID = mID + 1;
        mRequire = Require;
        mTitleText.text = title;
        mContentsText.text = contents;

        mButton.onClick.AddListener(() =>
        {
            callback();
        });
    }
    public int GetRequire()
    {
        return mRequire;
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
