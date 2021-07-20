using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance;
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
        Quest_PlayerLevel.Instance.ShowQuest(GameController.Instance.Quest_PlayerLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReStart()
    {
        Quest_PlayerLevel.Instance.ShowQuest(GameController.Instance.Quest_PlayerLevel);
    }
}
