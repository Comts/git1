using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{
    public static PointController Instance;


#pragma warning disable 0649
    [SerializeField]
    private Image[] PlayerPoint, MinePoint, CraftGemPoint,GemSellPoint, QuestPoint;
    [SerializeField]
    private Image DigPoint, CoworkerPoint;
    [SerializeField]
    private Image GemSellWindow, PlayerWindow;
#pragma warning restore 0649

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowCoworkerPoint(bool b)
    {
        SoundController.Instance.FXSound(12);
        ShowPoint(CoworkerPoint, b);
    }
    public void ShowDigPoint(bool b)
    {
        ShowPoint(DigPoint, b);
    }
    public void ShowQuestPoint()
    {
        ShowPoint(QuestPoint[0],true);
    }
    public void ShowAchievePoint()
    {
        ShowPoint(QuestPoint[0],true);
        ShowPoint(QuestPoint[1],true);
    }
    public void ShowPlayerPoint(bool b)
    {
        ShowPoint(PlayerPoint[0],b);
    }
    public void ShowPlayerLevelUpPoint(bool b)
    {
            ShowPoint(PlayerPoint[0], b);
        if (PlayerWindow.gameObject.activeInHierarchy)
        {
            ShowPoint(PlayerPoint[0], false);
        }
        ShowPoint(PlayerPoint[1],b);
    }
    public bool CheckPlyerLevelUpPoint()
    {
        if (PlayerPoint[0].gameObject.activeInHierarchy|| PlayerPoint[1].gameObject.activeInHierarchy)
        {
            return true;
        }
        return false;
    }
    public void ShowGemSellPoint()
    {
        SoundController.Instance.FXSound(12);
        ShowPoint(GemSellPoint[0],true);
        if (GemSellWindow.gameObject.activeInHierarchy)
        {
            ShowPoint(GemSellPoint[2], true);
            ShowPoint(GemSellPoint[1], true);
        }
    }
    public void ShowGemSellLayoutPoint()
    {
        if (GemSellPoint[0].gameObject.activeInHierarchy)
        {
            ShowPoint(GemSellPoint[2],true);
            ShowPoint(GemSellPoint[1],true);
            ShowPoint(GemSellPoint[0],false);
            
        }
    }
    public void NoShowGemSellPoint()
    {
            ShowPoint(GemSellPoint[2], false);
            ShowPoint(GemSellPoint[1], false);
            ShowPoint(GemSellPoint[0], false);
    }

    // Update is called once per frame
    void ShowPoint(Image img,bool b)
    {
        img.gameObject.SetActive(b);        
    }
}
