using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class ItemUseController : MonoBehaviour
{
    public static ItemUseController Instance;
    [SerializeField]
    private float[] mItemCooltimeArr, mItemMaxCooltimeArr;

    [SerializeField]
    private ItemButton[] mItemButtonArr;
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
        for (int i=0;i< GetGemMulti.Length;i++)
        {
            GetGemMulti[i] = 1;
        }
        mItemCooltimeArr = GameController.Instance.GetItemCooltimeArr();
        mItemMaxCooltimeArr = GameController.Instance.GetItemMaxCooltimeArr();
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
            }
            else if (buttonID <6)
            {
                GameController.Instance.HaveItem[1]--;
            }

        }
        mItemCooltimeArr[buttonID] = 10;
        StartCoroutine(CooltimeRoutine(buttonID, 10));
        CheckItemButton();

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
            }
            if(mGetGemMultiPlayer != null)
            {
                mItemButtonArr[1].SetButtonActive(false);
            }
            if(mGetGemMultiCoworker != null)
            {
                mItemButtonArr[2].SetButtonActive(false);
            }
        }
        else
        {
            mItemButtonArr[0].SetButtonActive(false);
            mItemButtonArr[1].SetButtonActive(false);
            mItemButtonArr[2].SetButtonActive(false);
        }

        if (GameController.Instance.HaveItem[1] > 0)
        {
            if (mSellGemMulti != null)
            {
                mItemButtonArr[3].SetButtonActive(false);
            }
            if (mGetGemMultiPlayer != null)
            {
                mItemButtonArr[4].SetButtonActive(false);
            }
            if (mGetGemMultiCoworker != null)
            {
                mItemButtonArr[5].SetButtonActive(false);
            }
        }
        else
        {
            mItemButtonArr[3].SetButtonActive(false);
            mItemButtonArr[4].SetButtonActive(false);
            mItemButtonArr[5].SetButtonActive(false);
        }
    }
}
