using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{
    public static PointController Instance;


#pragma warning disable 0649
    [SerializeField]
    private Image[] PlayerPoint,GemSellPoint, QuestPoint;
    [SerializeField]
    private Image DigPoint, CoworkerPoint,SettingPoint,MolePoint,StagePinPoint,EarthPoint, CraftGemPoint, MinePoint;
    [SerializeField]
    private Image GemSellWindow, PlayerWindow, DaziExplainWindow, SettingWindow;
    [SerializeField]
    private Text DaziExpalinText;
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
    public void ShowEarthExplain()
    {
        ShowPoint(EarthPoint, true);
        DaziExplainWindow.gameObject.SetActive(true);
        DaziExpalinText.text = string.Format("{0}님\n지구를 뚫어서 탐험해 보세요.", PlayerPrefs.GetString("Name"));
    }
    public void ShowStageChangeExplain()
    {
        ShowPoint(PlayerPoint[0], true);
        DaziExplainWindow.gameObject.SetActive(true);
        DaziExpalinText.text = string.Format("층수를 선택하여\n플레이어를 이동시킬 수 있습니다.");
    }
    public void ShowDigExplain()
    {
        DaziExplainWindow.gameObject.SetActive(true);
        DaziExpalinText.text = string.Format("{0}님\n땅굴파기를 해보세요.\n깊은 곳으로 갈수록.\n더 비싼 원석이 나옵니다.", PlayerPrefs.GetString("Name"));
    }
    public void ShowMineExplain()
    {
        SoundController.Instance.FXSound(12);
        ShowPoint(MinePoint, true);
        DaziExplainWindow.gameObject.SetActive(true);
        DaziExpalinText.text = string.Format("{0}님\n광산을 구매해 보세요.\n구매한 광산에 원석이 모입니다.\n판매 시 모은 원석을 얻습니다.", PlayerPrefs.GetString("Name"));
    }
    public void ShowCoworkerExplain()
    {
        SoundController.Instance.FXSound(12);
        ShowPoint(CoworkerPoint, true);
        DaziExplainWindow.gameObject.SetActive(true);
        DaziExpalinText.text = string.Format("{0}님\n동료와 함께 원석을 채굴해 보세요.", PlayerPrefs.GetString("Name"));
    }
    public void ShowCraftGemExplain()
    {
        SoundController.Instance.FXSound(12);
        ShowPoint(CraftGemPoint, true);
        DaziExplainWindow.gameObject.SetActive(true);
        DaziExpalinText.text = string.Format("원석을 모아 가공해 보세요\n광물의 등급이 높을수록\n판매 시 더 많은 다지코인을 얻습니다.");
    }
    public void ShowStagePinExplain()
    {
        SoundController.Instance.FXSound(12);
        ShowStagePinPoint(true);
        DaziExplainWindow.gameObject.SetActive(true);
        DaziExpalinText.text = string.Format("{0}님\n스테이지 고정을 선택해 보세요.\n화면이 고정되어 터치하기 편합니다.", PlayerPrefs.GetString("Name"));
    }
    public void ShowStagePinPoint(bool b)
    {
        ShowPoint(StagePinPoint, b);
    }
    public void ShowMoleExplain()
    {
        SoundController.Instance.FXSound(12);
        ShowPoint(MolePoint, true);
        DaziExplainWindow.gameObject.SetActive(true);
        DaziExpalinText.text = string.Format("{0}님\n두더지 잡기 게임을 해보세요.\n두더지를 많이 잡을수록\n많은 다지코인을 얻습니다.", PlayerPrefs.GetString("Name"));
    }
    public void ShowEndMoleExplain()
    {
        DaziExplainWindow.gameObject.SetActive(true);
        DaziExpalinText.text = string.Format("두더지 잡기는 횟수 제한이 있어요.\n기다리시면 횟수가 충전됩니다.\n최대 충전 횟수는 3회예요");
        GameController.Instance.MoleTutorial = 1;
    }
    public void ShowNameExplain()
    {
        SoundController.Instance.FXSound(12);
        if (!SettingWindow.gameObject.activeInHierarchy)
        {
            ShowPoint(SettingPoint, true);
            DaziExplainWindow.gameObject.SetActive(true);
            DaziExpalinText.text = "설정탭에서 플레이어의\n이름을 변경해보세요.\n얼굴 변경권 구매 시\n플레이어의 얼굴도 변경 가능합니다.";
        }
    }
    public void ShowTouchExplain()
    {
        DaziExplainWindow.gameObject.SetActive(true);
        DaziExpalinText.text = "안녕하세요.\n게임 설명을 맡은 두다지입니다.\n우선 화면을 터치하여 원석을 채굴해 보세요.";
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
        if (b == true)
        {
            DaziExplainWindow.gameObject.SetActive(true);
            DaziExpalinText.text = "플레이어의 레벨을 올리세요.\n레벨이 높을수록 노동력이 높아\n쉽게 채굴 할 수 있습니다.";
        }
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
        if (GameController.Instance.Gold <= 0)
        {
            DaziExplainWindow.gameObject.SetActive(true);
            DaziExpalinText.text = "채굴한 원석을 팔아\n다지코인을 얻으세요.\n자동 판매 설정 시\n원석을 자동으로 팔 수 있습니다.";
            SoundController.Instance.FXSound(12);
            ShowPoint(GemSellPoint[0], true);
            if (GemSellWindow.gameObject.activeInHierarchy)
            {
                ShowPoint(GemSellPoint[2], true);
                ShowPoint(GemSellPoint[1], true);
            }
        }
        else
        {
            GameController.Instance.GemSellTutorial = 1;
            NoShowGemSellPoint();

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
