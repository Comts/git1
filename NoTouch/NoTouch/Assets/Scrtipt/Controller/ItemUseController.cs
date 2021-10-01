using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseController : MonoBehaviour
{
    public static ItemUseController Instance;
    [SerializeField]
    private float[] mItemCooltimeArr, mItemMaxCooltimeArr;

#pragma warning disable 0649
    [SerializeField]
    private ItemButton[] mItemButtonArr;
    [SerializeField]
    private Text Item1, Item2, Item1_IAP, Item2_IAP, Item1_S, Item2_S;
#pragma warning restore 0649
    public double[] GetGemMulti { get; set; }
    public double SellGemMulti { get; set; }
    // Start is called before the first frame update
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
    void Start()
    {
        GetGemMulti = new double[2];
        SellGemMulti = 1;
        for (int i = 0; i < GetGemMulti.Length; i++)
        {
            GetGemMulti[i] = 1;
        }
        mItemCooltimeArr = GameController.Instance.GetItemCooltimeArr();
        mItemMaxCooltimeArr = GameController.Instance.GetItemMaxCooltimeArr();
        ShowHaveItem_start();
    }

    public void ReStart()
    {
        SellGemMulti = 1;
        for (int i = 0; i < GetGemMulti.Length; i++)
        {
            GetGemMulti[i] = 1;
        }
        mItemCooltimeArr = GameController.Instance.GetItemCooltimeArr();
        mItemMaxCooltimeArr = GameController.Instance.GetItemMaxCooltimeArr();

        if (mSellGemMulti != null)
        {
            StopCoroutine(mSellGemMulti);
        }
        mSellGemMulti = null;

        if (mGetGemMultiPlayer != null)
        {
            StopCoroutine(mGetGemMultiPlayer);
        }
        mGetGemMultiPlayer = null;

        if (mGetGemMultiCoworker != null)
        {
            StopCoroutine(mGetGemMultiCoworker);
        }
        mGetGemMultiCoworker = null;

        ShowHaveItem();
    }
    public void ShowHaveItem()
    {
        Item1.text = GameController.Instance.HaveItem[1].ToString();
        Item1_IAP.text = Item1.text;
        Item1_S.text = Item1.text;
        Item2.text = GameController.Instance.HaveItem[0].ToString();
        Item2_IAP.text = Item2.text;
        Item2_S.text = Item2.text;
        Quest_SilverDazi.Instance.CheckQuest();
        Quest_GoldDazi.Instance.CheckQuest();
        CheckItemButton();
    }
    public void ShowHaveItem_start()
    {
        Item1.text = GameController.Instance.HaveItem[1].ToString();
        Item1_IAP.text = Item1.text;
        Item1_S.text = Item1.text;
        Item2.text = GameController.Instance.HaveItem[0].ToString();
        Item2_IAP.text = Item2.text;
        Item2_S.text = Item2.text;
        CheckItemButton();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UseItem(int buttonID)//TODO 아이템 사용상태 표시
    {
        switch(buttonID)
        {
            case 0://광물가격상승
                mSellGemMulti = StartCoroutine(SellGemMultiRoutine(10, 2));
                break;
            case 1://광부의 도시락
                mGetGemMultiPlayer = StartCoroutine(GetGemMultiRoutine(0, 10, 10));
                break;
            case 2://알바들의 도시락
                mGetGemMultiCoworker = StartCoroutine(GetGemMultiRoutine(1, 10, 10));
                break;
            case 3:
                mSellGemMulti = StartCoroutine(SellGemMultiRoutine(10, 20));
                break;
            case 4:
                mGetGemMultiPlayer = StartCoroutine(GetGemMultiRoutine(0, 10, 100));
                break;
            case 5:
                mGetGemMultiCoworker = StartCoroutine(GetGemMultiRoutine(1, 10, 100));
                break;
            default:
                Debug.LogError("wrong buttonID "+ buttonID);
                break;
        }
        if (buttonID >= 0)
        {
            if (buttonID < 3)
            {
                GameController.Instance.HaveItem[0]--;
                if (GameController.Instance.Achive_Silver == 0)
                {
                    GameController.Instance.UseSilverAmount++;
                    if (GameController.Instance.UseSilverAmount >= 100)
                    {
                        QuestController.Instance.Achive_Silver();
                    }
                }
            }
            else if (buttonID <6)
            {
                GameController.Instance.HaveItem[1]--;
            }
        }
        mItemCooltimeArr[buttonID] = 10;
        StartCoroutine(CooltimeRoutine(buttonID, 10));
        ShowHaveItem();

    }
    private Coroutine mSellGemMulti;
    private Coroutine mGetGemMultiPlayer;
    private Coroutine mGetGemMultiCoworker;
    private IEnumerator SellGemMultiRoutine(float duration, double value)
    {
        SellGemMulti = value;
        yield return new WaitForSeconds(duration);
        SellGemMulti = 1;
        mSellGemMulti = null;
        CheckItemButton();
    }
    private IEnumerator GetGemMultiRoutine(int id,float duration, double value) //0 player 1 coworker
    {
        GetGemMulti[id] = value;
        yield return new WaitForSeconds(duration);
        GetGemMulti[id] = 1;
        if (id == 0)
        {
            if (GameController.Instance.Achive_Dosirak == 0)
            {
                GameController.Instance.EatAmount++;
                if (GameController.Instance.EatAmount >= 100)
                {
                    QuestController.Instance.Achive_Dosirak();
                }
            }
            mGetGemMultiPlayer = null;
        }
        else if( id == 1)
        {
            mGetGemMultiCoworker = null;
        }
        CheckItemButton();
    }
    private IEnumerator CooltimeRoutine(int buttonID, float cooltime)
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();

        while (mItemCooltimeArr[buttonID] >= 0)
        {
            yield return frame;
            mItemCooltimeArr[buttonID] -= Time.fixedDeltaTime;
            mItemButtonArr[buttonID].ShowCooltime(mItemCooltimeArr[buttonID],
                                                   cooltime);
            mItemButtonArr[buttonID+6].ShowCooltime(mItemCooltimeArr[buttonID],
                                                   cooltime);
            if (buttonID == 1)
            {
                mItemButtonArr[12].ShowCooltime(mItemCooltimeArr[buttonID],
                                                       cooltime);
                mItemButtonArr[14].ShowCooltime(mItemCooltimeArr[buttonID],
                                                       cooltime);
                mItemButtonArr[16].ShowCooltime(mItemCooltimeArr[buttonID],
                                                       cooltime);
                mItemButtonArr[18].ShowCooltime(mItemCooltimeArr[buttonID],
                                                       cooltime);
                mItemButtonArr[20].ShowCooltime(mItemCooltimeArr[buttonID],
                                                       cooltime);
            }
            if (buttonID == 4)
            {
                mItemButtonArr[13].ShowCooltime(mItemCooltimeArr[buttonID],
                                                       cooltime);
                mItemButtonArr[15].ShowCooltime(mItemCooltimeArr[buttonID],
                                                       cooltime);
                mItemButtonArr[17].ShowCooltime(mItemCooltimeArr[buttonID],
                                                       cooltime);
                mItemButtonArr[19].ShowCooltime(mItemCooltimeArr[buttonID],
                                                       cooltime);
                mItemButtonArr[21].ShowCooltime(mItemCooltimeArr[buttonID],
                                                       cooltime);
            }
        }
    }
    public void CheckItemButton()
    {
        for(int i=0;i<mItemButtonArr.Length;i++)
        {
            mItemButtonArr[i].SetButtonActive(true);
        }
        if (GameController.Instance.HaveItem[0] > 0)
        {
            if (mSellGemMulti != null)
            {
                mItemButtonArr[0].SetButtonActive(false);
                mItemButtonArr[6].SetButtonActive(false);
            }
            if(mGetGemMultiPlayer != null)
            {
                mItemButtonArr[1].SetButtonActive(false);
                mItemButtonArr[7].SetButtonActive(false);
                mItemButtonArr[12].SetButtonActive(false);
                mItemButtonArr[14].SetButtonActive(false);
                mItemButtonArr[16].SetButtonActive(false);
                mItemButtonArr[18].SetButtonActive(false);
                mItemButtonArr[20].SetButtonActive(false);
            }
            if(mGetGemMultiCoworker != null)
            {
                mItemButtonArr[2].SetButtonActive(false);
                mItemButtonArr[8].SetButtonActive(false);
            }
        }
        else
        {
            mItemButtonArr[0].SetButtonActive(false);
            mItemButtonArr[1].SetButtonActive(false);
            mItemButtonArr[2].SetButtonActive(false);
            mItemButtonArr[6].SetButtonActive(false);
            mItemButtonArr[7].SetButtonActive(false);
            mItemButtonArr[8].SetButtonActive(false);
            mItemButtonArr[12].SetButtonActive(false);
            mItemButtonArr[14].SetButtonActive(false);
            mItemButtonArr[16].SetButtonActive(false);
            mItemButtonArr[18].SetButtonActive(false);
            mItemButtonArr[20].SetButtonActive(false);
        }

        if (GameController.Instance.HaveItem[1] > 0)
        {
            if (mSellGemMulti != null)
            {
                mItemButtonArr[3].SetButtonActive(false);
                mItemButtonArr[9].SetButtonActive(false);
            }
            if (mGetGemMultiPlayer != null)
            {
                mItemButtonArr[4].SetButtonActive(false);
                mItemButtonArr[10].SetButtonActive(false);
                mItemButtonArr[13].SetButtonActive(false);
                mItemButtonArr[15].SetButtonActive(false);
                mItemButtonArr[17].SetButtonActive(false);
                mItemButtonArr[19].SetButtonActive(false);
                mItemButtonArr[21].SetButtonActive(false);
            }
            if (mGetGemMultiCoworker != null)
            {
                mItemButtonArr[5].SetButtonActive(false);
                mItemButtonArr[11].SetButtonActive(false);
            }
        }
        else
        {
            mItemButtonArr[3].SetButtonActive(false);
            mItemButtonArr[4].SetButtonActive(false);
            mItemButtonArr[5].SetButtonActive(false);
            mItemButtonArr[9].SetButtonActive(false);
            mItemButtonArr[10].SetButtonActive(false);
            mItemButtonArr[11].SetButtonActive(false);
            mItemButtonArr[13].SetButtonActive(false);
            mItemButtonArr[15].SetButtonActive(false);
            mItemButtonArr[17].SetButtonActive(false);
            mItemButtonArr[19].SetButtonActive(false);
            mItemButtonArr[21].SetButtonActive(false);
        }
    }
}
