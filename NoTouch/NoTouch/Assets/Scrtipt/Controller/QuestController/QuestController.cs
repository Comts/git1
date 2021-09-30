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
    private Image Achive_Mole_Window, Achive_Mole_Image;
    [SerializeField]
    private Image Achive_AutoClick_Window, Achive_AutoClick_Image;
    [SerializeField]
    private Image Achive_Norini_Window, Achive_Norini_Image;
    [SerializeField]
    private Image Achive_Coal_Window, Achive_Coal_Image;
    [SerializeField]
    private Image Achive_Ame_Window, Achive_Ame_Image;
    [SerializeField]
    private Image Achive_Gold_Window, Achive_Gold_Image;
    [SerializeField]
    private Image Achive_Dia_Window, Achive_Dia_Image;
    [SerializeField]
    private Image Achive_Vib_Window, Achive_Vib_Image;
    [SerializeField]
    private Image Achive_Dosirak_Window, Achive_Dosirak_Image;
    [SerializeField]
    private Image Achive_Silver_Window, Achive_Silver_Image;
#pragma warning restore 0649

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
        for(int i = 0; i < QuestList.Length; i++)
        {
            QuestList[i].gameObject.SetActive(true);
        }

        if (GameController.Instance.Achieve_Click == 1)
        {
            Achieve_Click_Image.gameObject.SetActive(true);
        }
        if (GameController.Instance.Achive_Mole == 1)
        {
            Achive_Mole_Image.gameObject.SetActive(true);
        }
        if (GameController.Instance.Achive_AutoClick == 1)
        {
            Achive_AutoClick_Image.gameObject.SetActive(true);
        }
        if (GameController.Instance.Achive_Norini == 1)
        {
            Achive_Norini_Image.gameObject.SetActive(true);
        }
        if (GameController.Instance.Achive_Coal == 1)
        {
            Achive_Coal_Image.gameObject.SetActive(true);
        }
        if (GameController.Instance.Achive_Ame == 1)
        {
            Achive_Ame_Image.gameObject.SetActive(true);
        }
        if (GameController.Instance.Achive_Gold == 1)
        {
            Achive_Gold_Image.gameObject.SetActive(true);
        }
        if (GameController.Instance.Achive_Dia == 1)
        {
            Achive_Dia_Image.gameObject.SetActive(true);
        }
        if (GameController.Instance.Achive_Vib == 1)
        {
            Achive_Vib_Image.gameObject.SetActive(true);
        }
        if (GameController.Instance.Achive_Dosirak == 1)
        {
            Achive_Dosirak_Image.gameObject.SetActive(true);
        }
        if (GameController.Instance.Achive_Silver == 1)
        {
            Achive_Silver_Image.gameObject.SetActive(true);
        }
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
        Achive_Mole_Image.gameObject.SetActive(false);
        Achive_Coal_Image.gameObject.SetActive(false);
        Achive_Ame_Image.gameObject.SetActive(false);
        Achive_Gold_Image.gameObject.SetActive(false);
        Achive_Dia_Image.gameObject.SetActive(false);
        Achive_Vib_Image.gameObject.SetActive(false);
        Achive_Dosirak_Image.gameObject.SetActive(false);
        Achive_Silver_Image.gameObject.SetActive(false);
    }
    public void Achive_Click()
    {
        GameController.Instance.Achieve_Click = 1;
        Achieve_Click_Window.gameObject.SetActive(true);
        Achieve_Click_Image.gameObject.SetActive(true);
    }
    public void Achive_Mole()
    {
        GameController.Instance.Achive_Mole = 1;
        Achive_Mole_Window.gameObject.SetActive(true);
        Achive_Mole_Image.gameObject.SetActive(true);
    }
    public void Achive_AutoClick()
    {
        GameController.Instance.Achive_AutoClick = 1;
        Achive_AutoClick_Window.gameObject.SetActive(true);
        Achive_AutoClick_Image.gameObject.SetActive(true);
    }
    public void Achive_Norini()
    {
        GameController.Instance.Achive_Norini = 1;
        Achive_Norini_Window.gameObject.SetActive(true);
        Achive_Norini_Image.gameObject.SetActive(true);
    }
    public void Achive_Coal()
    {
        if (GameController.Instance.AddAmoutGem_S[0]>=1000)
        {
            GameController.Instance.Achive_Coal = 1;
            Achive_Coal_Window.gameObject.SetActive(true);
            Achive_Coal_Image.gameObject.SetActive(true);
        }
    }
    public void Achive_Ame()
    {
        if (GameController.Instance.AddAmoutGem_B[5]>=1000)
        {
            GameController.Instance.Achive_Ame = 1;
            Achive_Ame_Window.gameObject.SetActive(true);
            Achive_Ame_Image.gameObject.SetActive(true);
        }
    }
    public void Achive_Gold()
    {
        if (GameController.Instance.AddAmoutGem_S[14]>=1000)
        {
            GameController.Instance.Achive_Gold = 1;
            Achive_Gold_Window.gameObject.SetActive(true);
            Achive_Gold_Image.gameObject.SetActive(true);
        }
    }
    public void Achive_Dia()
    {
        if (GameController.Instance.AddAmoutGem_S[15]>=1000)
        {
            GameController.Instance.Achive_Dia = 1;
            Achive_Dia_Window.gameObject.SetActive(true);
            Achive_Dia_Image.gameObject.SetActive(true);
        }
    }
    public void Achive_Vib()
    {
        if (GameController.Instance.AddAmoutGem_S[16]>=1000)
        {
            GameController.Instance.Achive_Vib = 1;
            Achive_Vib_Window.gameObject.SetActive(true);
            Achive_Vib_Image.gameObject.SetActive(true);
        }
    }
    public void Achive_Dosirak()
    {
        GameController.Instance.Achive_Dosirak = 1;
        Achive_Dosirak_Window.gameObject.SetActive(true);
        Achive_Dosirak_Image.gameObject.SetActive(true);
    }
    public void Achive_Silver()
    {
        GameController.Instance.Achive_Silver = 1;
        Achive_Silver_Window.gameObject.SetActive(true);
        Achive_Silver_Image.gameObject.SetActive(true);
    }
}
