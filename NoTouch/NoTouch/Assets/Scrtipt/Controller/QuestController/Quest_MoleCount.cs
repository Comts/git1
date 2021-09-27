using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_MoleCount : InformationLoader
{
    public static Quest_MoleCount Instance;
    [SerializeField]
    private QuestTextInfo[] mTextInfoArr;

    private List<QuestUIElement> mElementList;

    [SerializeField]
    private int ItemNum;
    [SerializeField]
    private Sprite image;
#pragma warning disable 0649
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
    private void Start()
    {
        LoadJson(out mTextInfoArr,
            Paths.Q_MoleCount_INFO_TABLE +
            Paths.LANGUAGE_TYPE_ARR[GameController.Instance.LanguageType]);

        mElementList = new List<QuestUIElement>();
        Load();
    }
    private void Load()
    {
        for (int i = 0; i < mTextInfoArr.Length; i++)
        {
            QuestUIElement element = Instantiate(mElementPrefab, mElementArea);
            element.Init(i, image, ItemNum, 1, 500 * (i + 1),
                                    string.Format(mTextInfoArr[i].Title, 500 * (i + 1)),
                                    string.Format(mTextInfoArr[i].ContentsFormat, 1),
                                    RequireAward);

            mElementList.Add(element);
        }
        ShowQuest(GameController.Instance.Quest_MoleCount);
    }
    public void CheckQuest()
    {
        if (GameController.Instance.Quest_MoleCount < mElementList.Count)
        {
            mElementList[GameController.Instance.Quest_MoleCount].ShowGaugeBar(GameController.Instance.WhackCount, mElementList[GameController.Instance.Quest_MoleCount].GetRequire());

            if (GameController.Instance.WhackCount >= mElementList[GameController.Instance.Quest_MoleCount].GetRequire())
            {
                mElementList[GameController.Instance.Quest_MoleCount].ClearQuest();
            }
        }
    }
    public void RequireAward()
    {
        GameController.Instance.Quest_MoleCount = mElementList[GameController.Instance.Quest_MoleCount].GetAward();
        ItemUseController.Instance.ShowHaveItem();
        ShowQuest(GameController.Instance.Quest_MoleCount);
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
        CheckQuest();

    }
}
