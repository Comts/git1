using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public static StageController Instance;
    private Animator[] mAnimArr;
    private List<StageUIElement> mElementList;
#pragma warning disable 0649
    [SerializeField]
    private StageUIElement mElementPrefab;
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
        mAnimArr = Resources.LoadAll<Animator>("Coworker");
        mElementList = new List<StageUIElement>();
        Load();
    }

    private void Load()
    {
        for (int i = 0; i <= GameController.Instance.Stage; i++)
        {
            StageUIElement element = Instantiate(mElementPrefab, mElementArea);
            element.Init(i, mAnimArr[i]);


            mElementList.Add(element);
        }
        mElementList[GameController.Instance.PlayerPos].PlayerActive(true);
    }
}
