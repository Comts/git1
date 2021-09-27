using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_GoldDazi : InformationLoader
{
    public static Quest_GoldDazi Instance;
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
    // Start is called before the first frame update
    void Start()
    {
        LoadJson(out mTextInfoArr,
            Paths.Q_GoldDazi_INFO_TABLE +
            Paths.LANGUAGE_TYPE_ARR[GameController.Instance.LanguageType]);

        mElementList = new List<QuestUIElement>();
        Load();
    }
    private void Load()
    {
        for (int i = 0; i < mTextInfoArr.Length; i++)
        {
            QuestUIElement element = Instantiate(mElementPrefab, mElementArea);
            element.Init(i, image, ItemNum, (i+1), 20 * (i + 1),
                                    string.Format(mTextInfoArr[i].Title, 20 * (i + 1)),
                                    string.Format(mTextInfoArr[i].ContentsFormat, (i + 1)),
                                    RequireAward);

            mElementList.Add(element);
        }
        ShowQuest(GameController.Instance.Quest_GoldDazi);
    }
    public void CheckQuest()
    {
        if (GameController.Instance.Quest_GoldDazi < mElementList.Count)
        {
            mElementList[GameController.Instance.Quest_GoldDazi].ShowGaugeBar(GameController.Instance.HaveItem[1], mElementList[GameController.Instance.Quest_GoldDazi].GetRequire());

            if (GameController.Instance.HaveItem[1] >= mElementList[GameController.Instance.Quest_GoldDazi].GetRequire())
            {
                mElementList[GameController.Instance.Quest_GoldDazi].ClearQuest();
            }
        }
    }
    public void RequireAward()
    {
        GameController.Instance.Quest_GoldDazi = mElementList[GameController.Instance.Quest_GoldDazi].GetAward();
        ItemUseController.Instance.ShowHaveItem();
        ShowQuest(GameController.Instance.Quest_GoldDazi);
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
