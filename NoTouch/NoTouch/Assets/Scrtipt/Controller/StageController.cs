using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    public static StageController Instance;
    private Animator[] mAnimArr;
    private List<StageUIElement> mElementList;
#pragma warning disable 0649
    [SerializeField]
    private StageUIElement[] mElementPrefab;
    [SerializeField]
    private ScrollRect mScrollArea;
    [SerializeField]
    private Toggle mPinToggle;
    [SerializeField]
    private Transform mElementArea;
    [SerializeField]
    private AddStageUIElement mLastSibling;
    [SerializeField]
    private Toggle[] mPlayerPos;
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
    public void ReStart()
    {
        for(int i =0;i<mElementList.Count;i++)
        {
            PlayerButtoninteractableFalse(i);
            Destroy(mElementList[i].gameObject);
        }
        mElementList.Clear();
        ReLoad();
    }
    private void ReLoad()
    {
        for (int i = 0; i <= GameController.Instance.Stage; i++)
        {
            StageUIElement element = Instantiate(mElementPrefab[i], mElementArea);
            element.Init(i, mAnimArr[i]);
            mElementList.Add(element);
            PlayerButtoninteractable(i);

            if (GameController.Instance.GetCoworkerLevelArr()[i] > 0)
            {
                mElementList[i].CoworkerActive(true);
            }
        }
        mLastSibling.Refresh(mElementList.Count, UnitSetter.GetUnitStr(100000 * math.pow(2, mElementList.Count - 1)));

        mElementList[GameController.Instance.PlayerPos].PlayerActive(true);

        mLastSibling.transform.SetAsLastSibling();

        if (!GameController.Instance.CheckScrollPin)
        {
            mScrollArea.vertical = true;
            mPinToggle.SetIsOnWithoutNotify(false);
        }
        



    }
    private void Load()
    {
        for (int i = 0; i <= GameController.Instance.Stage; i++)
        {
            StageUIElement element = Instantiate(mElementPrefab[i], mElementArea);
            element.Init(i, mAnimArr[i]);
            mElementList.Add(element);
            PlayerButtoninteractable(i);

            if (GameController.Instance.GetCoworkerLevelArr()[i]>0)
            {
                mElementList[i].CoworkerActive(true);
            }
        }
        mLastSibling.Init(mElementList.Count, UnitSetter.GetUnitStr(100000 * math.pow(2, mElementList.Count-1)), AddStage);

        mElementList[GameController.Instance.PlayerPos].PlayerActive(true);

        mPinToggle.onValueChanged.AddListener((bool bOn) =>
        {
            mScrollArea.vertical = !bOn;
        });

        if (GameController.Instance.CheckScrollPin)
        {
            mScrollArea.vertical = false;
            mPinToggle.SetIsOnWithoutNotify(true);
        }

        if (GameController.Instance.Stage < Constants.MAX_fLOOR - 1)
        {
            mLastSibling.transform.SetAsLastSibling();
        }
        else
        {
            mLastSibling.gameObject.SetActive(false);
        }


    }
    public void mPlayerPosSet()
    {
        mPlayerPos[GameController.Instance.PlayerPos].SetIsOnWithoutNotify(true);
    }
    public void CoworkerActive(int f)
    {
        mElementList[f].CoworkerActive(true);
    }
    public void PlayerButtoninteractable(int f)
    {
        mPlayerPos[f].interactable = true;
    }
    public void PlayerButtoninteractableFalse(int f)
    {
        mPlayerPos[f].interactable = false;
    }
    public void PlayerActive(int f)
    {
        for(int i =0;i<= GameController.Instance.Stage;i++)
        {
            mElementList[i].PlayerActive(false);
            if(i==f)
            {
                mElementList[f].PlayerActive(true);
                GameController.Instance.PlayerPos = f;
            }
        }
    }
    public void AddStage(int id, int amount)
    {
        Delegates.VoidCallback callback = () => { AddStageCallback(id, amount); };

        GameController.Instance.GoldCallback = callback;
        double cost = 100000*math.pow(2,id-1);
        GameController.Instance.Gold -= cost;
    }
    public void AddStageCallback(int id, int amount)
    {
        GameController.Instance.Stage = id;
        int nextID = id + 1;

        if (mElementList.Count <= nextID)
        {

            StageUIElement element = Instantiate(mElementPrefab[id], mElementArea);
            element.Init(id, mAnimArr[id]);


            mElementList.Add(element);
            PlayerButtoninteractable(id);
            mLastSibling.Refresh(mElementList.Count, UnitSetter.GetUnitStr(100000 * math.pow(2, id)));
        }
        if (GameController.Instance.Stage < Constants.MAX_fLOOR-1)
        {
            mLastSibling.transform.SetAsLastSibling();
        }
        else
        {
            mLastSibling.gameObject.SetActive(false);
        }
    }
}
