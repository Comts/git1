using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_MineCount : InformationLoader
{
    public static Quest_MineCount Instance;
    [SerializeField]
    private QuestTextInfo[] mTextInfoArr;

    private List<QuestUIElement> mElementList;

#pragma warning disable 0649
    [SerializeField]
    private int ItemNum;
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private QuestUIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;

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
        LoadJson(out mTextInfoArr,
            Paths.Q_MineCount_INFO_TABLE +
            Paths.LANGUAGE_TYPE_ARR[GameController.Instance.LanguageType]);

        mElementList = new List<QuestUIElement>();
        Load();
    }
    private void Load()
    {
        for (int i = 0; i < mTextInfoArr.Length; i++)
        {
            QuestUIElement element = Instantiate(mElementPrefab, mElementArea);
            element.Init(i, image, ItemNum, 2, 4 * (i + 1),
                                    string.Format(mTextInfoArr[i].Title, 4 * (i + 1)),
                                    string.Format(mTextInfoArr[i].ContentsFormat, 2),
                                    RequireAward);

            mElementList.Add(element);

            QuestController.Instance.QuestMax++;
        }
        ShowQuest(GameController.Instance.Quest_MineCount);
    }
    public void CheckQuest()
    {
        if (GameController.Instance.Quest_MineCount < mElementList.Count)
        {
            mElementList[GameController.Instance.Quest_MineCount].ShowGaugeBar(GameController.Instance.MineCount, mElementList[GameController.Instance.Quest_MineCount].GetRequire());

            if (GameController.Instance.MineCount >= mElementList[GameController.Instance.Quest_MineCount].GetRequire())
            {
                mElementList[GameController.Instance.Quest_MineCount].ClearQuest();
            }
        }
    }
    public void RequireAward()
    {
        GameController.Instance.Quest_MineCount = mElementList[GameController.Instance.Quest_MineCount].GetAward();

        string DaziName = mElementList[GameController.Instance.Quest_MineCount].GetDaziName();
        int DaziAmount = mElementList[GameController.Instance.Quest_MineCount].GetDaizAmount();
        QuestController.Instance.ShowGetDazi(DaziName, DaziAmount);

        ItemUseController.Instance.ShowHaveItem();
        QuestController.Instance.QuestProgress++;
        ShowQuest(GameController.Instance.Quest_MineCount);
    }
    public void ShowQuest(int id)
    {
        for (int i = 0; i < mElementList.Count; i++)
        {
            mElementList[i].gameObject.SetActive(false);
        }

        if (mElementList.Count > id)
        {
            mElementList[id].gameObject.SetActive(true);
            mElementList[id].Reset_Button();
        }
        else
        {
            mElementList[mElementList.Count - 1].AllClearQuest();

        }
        QuestController.Instance.ShowQuestProgress();
        CheckQuest();

    }

}
