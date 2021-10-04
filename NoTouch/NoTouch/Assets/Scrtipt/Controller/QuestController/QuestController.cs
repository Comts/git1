using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance;


#pragma warning disable 0649
    [SerializeField]
    private Transform[] QuestList;
    [SerializeField]
    private Image Achieve_Click_Window,Achieve_Click_Image;
    [SerializeField]
    private Image Achieve_Mole_Window, Achieve_Mole_Image;
    [SerializeField]
    private Image Achieve_AutoClick_Window, Achieve_AutoClick_Image;
    [SerializeField]
    private Image Achieve_Norini_Window, Achieve_Norini_Image;
    [SerializeField]
    private Image Achieve_Coal_Window, Achieve_Coal_Image;
    [SerializeField]
    private Image Achieve_Ame_Window, Achieve_Ame_Image;
    [SerializeField]
    private Image Achieve_Gold_Window, Achieve_Gold_Image;
    [SerializeField]
    private Image Achieve_Dia_Window, Achieve_Dia_Image;
    [SerializeField]
    private Image Achieve_Vib_Window, Achieve_Vib_Image;
    [SerializeField]
    private Image Achieve_Dosirak_Window, Achieve_Dosirak_Image;
    [SerializeField]
    private Image Achieve_Silver_Window, Achieve_Silver_Image;
    [SerializeField]
    private Image[] Achieve_Earth_Window, Achieve_Earth_Image;
    [SerializeField]
    private Text mAchieveText,mQuestText,BuffText;
#pragma warning restore 0649
    private int AchieveProgress;
    public int QuestMax,QuestProgress;
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
    // Start is called before the first frame update
    void Start()
    {
        QuestMax = 0;
        QuestProgress = GameController.Instance.Quest_PlayerLevel +
                        GameController.Instance.Quest_CowerkerLevelSum +
                        GameController.Instance.Quest_DigCount +
                        GameController.Instance.Quest_CraftGem +
                        GameController.Instance.Quest_MoleCount +
                        GameController.Instance.Quest_MineCount +
                        GameController.Instance.Quest_SilverDazi +
                        GameController.Instance.Quest_GoldDazi;

        for (int i = 0; i < QuestList.Length; i++)
        {
            QuestList[i].gameObject.SetActive(true);
        }
        CheckProgress();
        ShowQuestProgress();
    }
    public void CheckProgress()
    {
        AchieveProgress = 0;

        if (GameController.Instance.Achieve_Click == 1)
        {
            Achieve_Click_Image.gameObject.SetActive(true);
            AchieveProgress++;
        }
        if (GameController.Instance.Achive_Mole == 1)
        {
            Achieve_Mole_Image.gameObject.SetActive(true);
            AchieveProgress++;
        }
        if (GameController.Instance.Achive_AutoClick == 1)
        {
            Achieve_AutoClick_Image.gameObject.SetActive(true);
            AchieveProgress++;
        }
        if (GameController.Instance.Achive_Norini == 1)
        {
            Achieve_Norini_Image.gameObject.SetActive(true);
            AchieveProgress++;
        }
        if (GameController.Instance.Achive_Coal == 1)
        {
            Achieve_Coal_Image.gameObject.SetActive(true);
            AchieveProgress++;
        }
        if (GameController.Instance.Achive_Ame == 1)
        {
            Achieve_Ame_Image.gameObject.SetActive(true);
            AchieveProgress++;
        }
        if (GameController.Instance.Achive_Gold == 1)
        {
            Achieve_Gold_Image.gameObject.SetActive(true);
            AchieveProgress++;
        }
        if (GameController.Instance.Achive_Dia == 1)
        {
            Achieve_Dia_Image.gameObject.SetActive(true);
            AchieveProgress++;
        }
        if (GameController.Instance.Achive_Vib == 1)
        {
            Achieve_Vib_Image.gameObject.SetActive(true);
            AchieveProgress++;
        }
        if (GameController.Instance.Achive_Dosirak == 1)
        {
            Achieve_Dosirak_Image.gameObject.SetActive(true);
            AchieveProgress++;
        }
        if (GameController.Instance.Achive_Silver == 1)
        {
            Achieve_Silver_Image.gameObject.SetActive(true);
            AchieveProgress++;
        }
        if (GameController.Instance.Achive_Earth > 0)
        {
            for (int i = 0; i < GameController.Instance.Achive_Earth; i++)
            {
                Achieve_Earth_Image[i].gameObject.SetActive(true);
                AchieveProgress++;
            }
        }
        ShowAchieveProgress();

    }
    public void ShowAchieveProgress()
    {
        GameController.Instance.Buff_Achieve = AchieveProgress; 
        GameController.Instance.ManPower = GameController.Instance.CalBuffManPower(); 
        PlayerUpgradeController.Instance.ReSetSlider();
        string BuffStr = string.Format("+ {0} %", AchieveProgress * 100);

        BuffText.text = BuffStr;

        string progressStr = string.Format("{0} / {1}",
                                            AchieveProgress,
                                            16);
        mAchieveText.text = progressStr;
    }
    public void ShowQuestProgress()
    {
        string progressStr = string.Format("{0} / {1}",
                                            QuestProgress,
                                            QuestMax);
        mQuestText.text = progressStr;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReStart()
    {
        Quest_PlayerLevel.Instance.ShowQuest(GameController.Instance.Quest_PlayerLevel);
        Quest_CowerkorLevelSum.Instance.ShowQuest(GameController.Instance.Quest_CowerkerLevelSum);
        Quest_DigCount.Instance.ShowQuest(GameController.Instance.Quest_DigCount);
        Quest_CraftGem.Instance.ShowQuest(GameController.Instance.Quest_CraftGem);
        Quest_MoleCount.Instance.ShowQuest(GameController.Instance.Quest_MoleCount);
        Quest_MineCount.Instance.ShowQuest(GameController.Instance.Quest_MineCount);
        Quest_SilverDazi.Instance.ShowQuest(GameController.Instance.Quest_SilverDazi);
        Quest_GoldDazi.Instance.ShowQuest(GameController.Instance.Quest_GoldDazi);


        Achieve_Click_Image.gameObject.SetActive(false);
        Achieve_Mole_Image.gameObject.SetActive(false);
        Achieve_Coal_Image.gameObject.SetActive(false);
        Achieve_Ame_Image.gameObject.SetActive(false);
        Achieve_Gold_Image.gameObject.SetActive(false);
        Achieve_Dia_Image.gameObject.SetActive(false);
        Achieve_Vib_Image.gameObject.SetActive(false);
        Achieve_Dosirak_Image.gameObject.SetActive(false);
        Achieve_Silver_Image.gameObject.SetActive(false);

        for (int i = 0; i < Achieve_Earth_Image.Length; i++)
        {
            Achieve_Earth_Image[i].gameObject.SetActive(false);
        }
        CheckProgress();
        QuestProgress = 0;
        ShowQuestProgress();
    }
    public void Achive_Click()
    {
        GameController.Instance.Achieve_Click = 1;
        Achieve_Click_Window.gameObject.SetActive(true);
        Achieve_Click_Image.gameObject.SetActive(true);
        AchieveProgress++;
        ShowAchieveProgress();
    }
    public void Achive_Mole()
    {
        GameController.Instance.Achive_Mole = 1;
        Achieve_Mole_Window.gameObject.SetActive(true);
        Achieve_Mole_Image.gameObject.SetActive(true);
        AchieveProgress++;
        ShowAchieveProgress();
    }
    public void Achive_AutoClick()
    {
        GameController.Instance.Achive_AutoClick = 1;
        Achieve_AutoClick_Window.gameObject.SetActive(true);
        Achieve_AutoClick_Image.gameObject.SetActive(true);
        AchieveProgress++;
        ShowAchieveProgress();
    }
    public void Achive_Norini()
    {
        GameController.Instance.Achive_Norini = 1;
        Achieve_Norini_Window.gameObject.SetActive(true);
        Achieve_Norini_Image.gameObject.SetActive(true);
        AchieveProgress++;
        ShowAchieveProgress();
    }
    public void Achive_Coal()
    {
        if (GameController.Instance.AddAmoutGem_S[0]>=1000)
        {
            GameController.Instance.Achive_Coal = 1;
            Achieve_Coal_Window.gameObject.SetActive(true);
            Achieve_Coal_Image.gameObject.SetActive(true);
            AchieveProgress++;
            ShowAchieveProgress();
        }
    }
    public void Achive_Ame()
    {
        if (GameController.Instance.AddAmoutGem_B[5]>=1000)
        {
            GameController.Instance.Achive_Ame = 1;
            Achieve_Ame_Window.gameObject.SetActive(true);
            Achieve_Ame_Image.gameObject.SetActive(true);
            AchieveProgress++;
            ShowAchieveProgress();
        }
    }
    public void Achive_Gold()
    {
        if (GameController.Instance.AddAmoutGem_S[14]>=1000)
        {
            GameController.Instance.Achive_Gold = 1;
            Achieve_Gold_Window.gameObject.SetActive(true);
            Achieve_Gold_Image.gameObject.SetActive(true);
            AchieveProgress++;
            ShowAchieveProgress();
        }
    }
    public void Achive_Dia()
    {
        if (GameController.Instance.AddAmoutGem_S[15]>=1000)
        {
            GameController.Instance.Achive_Dia = 1;
            Achieve_Dia_Window.gameObject.SetActive(true);
            Achieve_Dia_Image.gameObject.SetActive(true);
            AchieveProgress++;
            ShowAchieveProgress();
        }
    }
    public void Achive_Vib()
    {
        if (GameController.Instance.AddAmoutGem_S[16]>=1000)
        {
            GameController.Instance.Achive_Vib = 1;
            Achieve_Vib_Window.gameObject.SetActive(true);
            Achieve_Vib_Image.gameObject.SetActive(true);
            AchieveProgress++;
            ShowAchieveProgress();
        }
    }
    public void Achive_Dosirak()
    {
        GameController.Instance.Achive_Dosirak = 1;
        Achieve_Dosirak_Window.gameObject.SetActive(true);
        Achieve_Dosirak_Image.gameObject.SetActive(true);
        AchieveProgress++;
        ShowAchieveProgress();
    }
    public void Achive_Silver()
    {
        GameController.Instance.Achive_Silver = 1;
        Achieve_Silver_Window.gameObject.SetActive(true);
        Achieve_Silver_Image.gameObject.SetActive(true);
        AchieveProgress++;
        ShowAchieveProgress();
    }
    public void Achive_Earth()
    {
        Achieve_Earth_Window[GameController.Instance.Achive_Earth].gameObject.SetActive(true);
        Achieve_Earth_Image[GameController.Instance.Achive_Earth].gameObject.SetActive(true);
        GameController.Instance.Achive_Earth++;
        AchieveProgress++;
        ShowAchieveProgress();
    }
}
