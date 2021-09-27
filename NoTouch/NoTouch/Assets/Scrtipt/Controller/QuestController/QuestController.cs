using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance;


    [SerializeField]
    private Transform[] QuestList;

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
    }
}
