using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_SilverDazi : InformationLoader
{
    public static Quest_SilverDazi Instance;
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
            Paths.Q_SilverDazi_INFO_TABLE +
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
        ShowQuest(GameController.Instance.Quest_SilverDazi);
    }
    public void CheckQuest()
    {
        if (GameController.Instance.Quest_SilverDazi < mElementList.Count)
        {
            mElementList[GameController.Instance.Quest_SilverDazi].ShowGaugeBar(GameController.Instance.HaveItem[0], mElementList[GameController.Instance.Quest_SilverDazi].GetRequire());

            if (GameController.Instance.HaveItem[0] >= mElementList[GameController.Instance.Quest_SilverDazi].GetRequire())
            {
                mElementList[GameController.Instance.Quest_SilverDazi].ClearQuest();
            }
        }
    }
    public void RequireAward()
    {
        GameController.Instance.Quest_SilverDazi = mElementList[GameController.Instance.Quest_SilverDazi].GetAward();
        ItemUseController.Instance.ShowHaveItem();
        ShowQuest(GameController.Instance.Quest_SilverDazi);
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
