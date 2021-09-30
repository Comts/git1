using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_CraftGem : InformationLoader
{
    public static Quest_CraftGem Instance;
    [SerializeField]
    private QuestTextInfo[] mTextInfoArr;

    private List<QuestUIElement> mElementList;

    private Sprite[] mIconArr;
#pragma warning disable 0649
    [SerializeField]
    private int ItemNum;
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
            Paths.Q_CraftGem_INFO_TABLE +
            Paths.LANGUAGE_TYPE_ARR[GameController.Instance.LanguageType]);

        mElementList = new List<QuestUIElement>();
        mIconArr = Resources.LoadAll<Sprite>(Paths.Q_CRAFT_ICON);
        Load();
    }
    private void Load()
    {
        for (int i = 0; i < mTextInfoArr.Length; i++)
        {
            QuestUIElement element = Instantiate(mElementPrefab, mElementArea);
            element.Init(i, mIconArr[i], ItemNum,1, 10, 
                                    string.Format(mTextInfoArr[i].Title, 10),
                                    string.Format(mTextInfoArr[i].ContentsFormat, 1),
                                    RequireAward);

            mElementList.Add(element);
        }
        ShowQuest(GameController.Instance.Quest_CraftGem); 
    }
    public void CheckQuest()
    {
        if (GameController.Instance.Quest_CraftGem < mElementList.Count)
        {
            int div;
            div = GameController.Instance.Quest_CraftGem % 3;

            switch (div)
            {
                case 0:
                    mElementList[GameController.Instance.Quest_CraftGem].ShowGaugeBar(GameController.Instance.AddAmoutGem_B[GameController.Instance.Quest_CraftGem / 3], mElementList[GameController.Instance.Quest_CraftGem].GetRequire());

                    if (GameController.Instance.AddAmoutGem_B[GameController.Instance.Quest_CraftGem / 3] >= mElementList[GameController.Instance.Quest_CraftGem].GetRequire())
                    {
                        mElementList[GameController.Instance.Quest_CraftGem].ClearQuest();
                    }
                    break;
                case 1:
                    mElementList[GameController.Instance.Quest_CraftGem].ShowGaugeBar(GameController.Instance.AddAmoutGem_A[GameController.Instance.Quest_CraftGem / 3], mElementList[GameController.Instance.Quest_CraftGem].GetRequire());

                    if (GameController.Instance.AddAmoutGem_A[GameController.Instance.Quest_CraftGem / 3] >= mElementList[GameController.Instance.Quest_CraftGem].GetRequire())
                    {
                        mElementList[GameController.Instance.Quest_CraftGem].ClearQuest();
                    }
                    break;
                case 2:
                    mElementList[GameController.Instance.Quest_CraftGem].ShowGaugeBar(GameController.Instance.AddAmoutGem_S[GameController.Instance.Quest_CraftGem / 3], mElementList[GameController.Instance.Quest_CraftGem].GetRequire());

                    if (GameController.Instance.AddAmoutGem_S[GameController.Instance.Quest_CraftGem / 3] >= mElementList[GameController.Instance.Quest_CraftGem].GetRequire())
                    {
                        mElementList[GameController.Instance.Quest_CraftGem].ClearQuest();
                    }
                    break;
                case 3:
                    Debug.Log("CraftGemQuest Error");
                    break;
            }
        }
    }
    public void RequireAward()
    {
        GameController.Instance.Quest_CraftGem = mElementList[GameController.Instance.Quest_CraftGem].GetAward();
        ItemUseController.Instance.ShowHaveItem();
        ShowQuest(GameController.Instance.Quest_CraftGem);
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
