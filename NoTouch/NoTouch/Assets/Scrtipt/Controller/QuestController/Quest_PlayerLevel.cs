using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_PlayerLevel : MonoBehaviour
{
    public static Quest_PlayerLevel Instance;
    [SerializeField]
    private List<QuestUIElement> mElementList;

    [SerializeField]
    private int ItemNum;

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

        for (int i = 0; i < mElementList.Count; i++)
        {
            mElementList[i].Init(i, ItemNum);
        }
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
            for (int i = 0; i < mElementList.Count; i++)
            {
                mElementList[i].gameObject.SetActive(false);
            }
        }

    }
}
