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
    private int mRequire;
    int mPlayerlevel;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ShowGaugeBar(GameController.Instance.GetPlayerLevel, mRequire);
        if(GameController.Instance.GetPlayerLevel >= mRequire)
        {
            ClearQuest();
        }
    }
    private int mID, mItemNum,NextID;
    public void Init(int id , int ItemNum)
    {
        mID = id;
        mItemNum = ItemNum;
        NextID = mID + 1;
    }
    public void GetAward(int amount)
    {
        GameController.Instance.HaveItem[mItemNum] += amount;
        GameController.Instance.Quest_PlayerLevel= NextID;
        ItemUseController.Instance.ShowHaveItem();
    }
    public void ClearQuest()
    {
        mGaugeBar.gameObject.SetActive(false);
        mButton.gameObject.SetActive(true);
    }
    public void ShowGaugeBar(double current, double max)
    {
        string progressStr = string.Format("{0} / {1}",
                                            current,
                                            max);
        float progress = (float)(current / max);
        mGaugeBar.ShowGaugeBar(progress, progressStr);
    }
}
