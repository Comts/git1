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
    [SerializeField]
    private Text[] mStageGem;
    [SerializeField]
    private Button mDigButton;
    private double mDigCost;
    [SerializeField]
    private GetGemEffectPool mGetGemEffectPool;
    private Sprite[] mGemIconArr;


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
        mGemIconArr = Resources.LoadAll<Sprite>(Paths.CRAFT_ICON);
        Load();
    }
    public void CheckDigButton()
    {
        if (!GameController.Instance.CheckScrollPin)
        {
            if (GameController.Instance.GetPlayerLevel >= 10 && GameController.Instance.StagePinTutorial == 0)
            {
                PointController.Instance.ShowStagePinExplain();
            }
        }
        else
        {
            GameController.Instance.StagePinTutorial = 1;
        }
           
        if (GameController.Instance.Gold >= mDigCost)
        {
            if (mDigButton.interactable == false)
            {
                SoundController.Instance.FXSound(12);
                PointController.Instance.ShowDigPoint(true);
                if(GameController.Instance.Stage == 0)
                {
                    PointController.Instance.ShowDigExplain();
                }
            }
            mDigButton.interactable = true;
        }
        else
        {
            if (mDigButton.interactable == true)
            {
                PointController.Instance.ShowDigPoint(false);
            }
            mDigButton.interactable = false;
        }
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
        mLastSibling.gameObject.SetActive(true);
    }
    private void ReLoad()
    {
        for (int i = 0; i <= GameController.Instance.Stage; i++)
        {
            StageUIElement element = Instantiate(mElementPrefab[i], mElementArea);
            element.Init(i, mAnimArr[i]);
            mElementList.Add(element);
            mElementList[i].HaveStage();
            PlayerButtoninteractable(i);

            if (GameController.Instance.CoworkerLevelArr[i] > 0)
            {
                mElementList[i].CoworkerActive(true);
            }
        }
        mLastSibling.Refresh(mElementList.Count, UnitSetter.GetUnitStr(100000 * math.pow(2.1, mElementList.Count - 1)));
        mDigCost = 100000 * math.pow(2.1, mElementList.Count - 1);
        CheckDigButton();

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
            mElementList[i].HaveStage();
            PlayerButtoninteractable(i);

            if (GameController.Instance.CoworkerLevelArr[i]>0)
            {
                mElementList[i].CoworkerActive(true);
            }
        }
        mLastSibling.Init(mElementList.Count, UnitSetter.GetUnitStr(100000 * math.pow(2.1, mElementList.Count-1)), AddStage);
        mDigCost = 100000 * math.pow(2.1, mElementList.Count - 1);
        CheckDigButton();

        mElementList[GameController.Instance.PlayerPos].PlayerActive(true);

        mPinToggle.onValueChanged.AddListener((bool bOn) =>
        {
            mScrollArea.vertical = !bOn;
            if(GameController.Instance.StagePinTutorial == 0&& bOn)
            {
                GameController.Instance.StagePinTutorial = 1;
                PointController.Instance.ShowStagePinPoint(false);
            }
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
    public void PlayerGemEffect()
    {
        GetGemEffect effect = mGetGemEffectPool.GetFromPool();
        effect.SetIcon(mGemIconArr[GameController.Instance.PlayerPos]);
        effect.transform.position = mElementList[GameController.Instance.PlayerPos].CheckPlayePos().position;
    }
    public Transform GetCoworkerPos(int i)
    {
        return mElementList[i].GetCoworkerPos();
    }
    public void mPlayerPosSet()
    {
        mPlayerPos[GameController.Instance.PlayerPos].SetIsOnWithoutNotify(true);
    }
    public void ChangePlayerImage(int i)
    {
        mElementList[GameController.Instance.PlayerPos].ChangePlayerImage(i);
    }
    public void ChangePlayerHeadImage()
    {
        for(int i = 0; i <= GameController.Instance.Stage; i++)
        {
            mElementList[i].ChangePlayerHead();
        }
    }
    public void ChangeCustomImage(Sprite spr)
    {
        for(int i = 0; i <= GameController.Instance.Stage; i++)
        {
            mElementList[i].ChangeCustomImage(spr);

        }
    }
    public void CoworkerActive(int f)
    {
        mElementList[f].CoworkerActive(true);
    }
    public void PlayerButtoninteractable(int f)
    {
        mPlayerPos[f].interactable = true;
        mStageGem[f].gameObject.SetActive(true);
    }
    public void PlayerButtoninteractableFalse(int f)
    {
        mPlayerPos[f].interactable = false;
        mStageGem[f].gameObject.SetActive(false);
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
                mElementList[GameController.Instance.PlayerPos].ChangePlayerImage(TouchManager.Instance.TouchImage);
            }
        }
    }
    public void AddStage(int id, int amount)
    {
        Delegates.VoidCallback callback = () => { AddStageCallback(id, amount); };

        GameController.Instance.GoldCallback = callback;
        double cost = 100000*math.pow(2.1,id-1);
        GameController.Instance.Gold -= cost;
    }
    public void AddStageCallback(int id, int amount)
    {
        SoundController.Instance.FXSound(8);
        GameController.Instance.Stage = id;
        int nextID = id + 1;

        CoworkerController.Instance.AddCowerker(id - 1);
        MineShopController.Instance.SetMine(id);
        CraftController.Instance.SetCraft(id);
        GemSellController.Instance.SetSellUI(id);
        if (GameController.Instance.Stage == 1)
        {
            PointController.Instance.ShowStageChangeExplain();
        }

        if (mElementList.Count <= nextID)
        {

            StageUIElement element = Instantiate(mElementPrefab[id], mElementArea);
            element.Init(id, mAnimArr[id]);
            mElementList.Add(element);
            mElementList[id].ShowStage();
            PlayerButtoninteractable(id);
            mLastSibling.Refresh(mElementList.Count, UnitSetter.GetUnitStr(100000 * math.pow(2.1, id)));
            mDigCost = 100000 * math.pow(2.1, mElementList.Count - 1);
            CheckDigButton();
        }

        if (GameController.Instance.Stage < Constants.MAX_fLOOR-1)
        {
            mLastSibling.transform.SetAsLastSibling();
        }
        else
        {
            mLastSibling.gameObject.SetActive(false);
        }
        Quest_DigCount.Instance.CheckQuest();
        PlayerActive(GameController.Instance.Stage);
    }
}
