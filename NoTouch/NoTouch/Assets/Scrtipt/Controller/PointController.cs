using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{
    public static PointController Instance;


    [SerializeField]
    private Image[] PlayerPoint, MinePoint, CraftGemPoint,GemSellPoint, QuestPoint;
    [SerializeField]
    private Image DigPoint, CoworkerPoint;

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
    public void ShowPlayerPoint()
    {
        SoundController.Instance.FXSound(12);
        ShowPoint(PlayerPoint[0],true);
    }
    public void ShowGemSellPoint()
    {
        SoundController.Instance.FXSound(12);
        ShowPoint(GemSellPoint[0],true);
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

    // Update is called once per frame
    void ShowPoint(Image img,bool b)
    {
        img.gameObject.SetActive(b);        
    }
}
