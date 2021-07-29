﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_DigCount : InformationLoader
{
    public static Quest_DigCount Instance;
    [SerializeField]
    private QuestTextInfo[] mTextInfoArr;

    private List<QuestUIElement> mElementList;

    [SerializeField]
    private int ItemNum;
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
            Paths.Q_DigCount_INFO_TABLE +
            Paths.LANGUAGE_TYPE_ARR[GameController.Instance.LanguageType]);

        mElementList = new List<QuestUIElement>();
        Load();
    }
    private void Load()
    {
        for (int i = 0; i < mTextInfoArr.Length; i++)
        {
            QuestUIElement element = Instantiate(mElementPrefab, mElementArea);
            element.Init(i, ItemNum, 1, 4 * (i + 1),
                                    string.Format(mTextInfoArr[i].Title, 4 * (i + 1)),
                                    string.Format(mTextInfoArr[i].ContentsFormat, 1),
                                    RequireAward);

            mElementList.Add(element);
        }
        ShowQuest(GameController.Instance.Quest_DigCount);
    }
    void Update()
    {
        if (GameController.Instance.Quest_DigCount < mElementList.Count)
        {
            mElementList[GameController.Instance.Quest_DigCount].ShowGaugeBar(GameController.Instance.Stage, mElementList[GameController.Instance.Quest_DigCount].GetRequire());

            if (GameController.Instance.Stage >= mElementList[GameController.Instance.Quest_DigCount].GetRequire())
            {
                mElementList[GameController.Instance.Quest_DigCount].ClearQuest();
            }
        }
    }
    public void RequireAward()
    {
        GameController.Instance.Quest_DigCount = mElementList[GameController.Instance.Quest_DigCount].GetAward();
        ItemUseController.Instance.ShowHaveItem();
        ShowQuest(GameController.Instance.Quest_DigCount);
    }
    public void ShowQuest(int id)
    {
        if (mElementList.Count > id)
        {
            mElementList[id].gameObject.SetActive(true);
            for (int i = 0; i < mElementList.Count; i++)
            {
                if (i == id)
                {
                    continue;
                }
                mElementList[i].gameObject.SetActive(false);
            }
        }
        else
        {
            mElementList[mElementList.Count - 1].AllClearQuest();

        }

    }

}