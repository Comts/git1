using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemSellController : InformationLoader
{
    public static GemSellController Instance;
    [SerializeField]
    private GemSellInfo[] mInfoArr;
    [SerializeField]
    private GemSellTextInfo[] mTextInfoArr;
    [SerializeField]
    protected Gem[] mOriginArr;
    private Sprite[] mIconArr;
#pragma warning disable 0649
    [SerializeField]
    private GemSellUIElement mElementPrefab;
    [SerializeField]
    private LayerButtonUIElement mLayerButtonPrefab;
    [SerializeField]
    private Transform mElementArea;
    [SerializeField]
    private Toggle mAllSellToggle;
#pragma warning restore 0649
    private List<GemSellUIElement> mElementList;
    private List<LayerButtonUIElement> mButtonElementList;


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

        LoadJson(out mInfoArr, Paths.SELLGEM_INFO_TABLE);
        LoadJson(out mTextInfoArr,
            Paths.SELLGEM_TEXT_INFO_TABLE +
            Paths.LANGUAGE_TYPE_ARR[GameController.Instance.LanguageType]);

        mOriginArr = Resources.LoadAll<Gem>(Paths.GEM_PREFAB);
        mIconArr = new Sprite[Constants.MAX_fLOOR * 4];
        mElementList = new List<GemSellUIElement>();
        mButtonElementList = new List<LayerButtonUIElement>();
        Load();
    }
    public void ReStart()
    {

        for (int i = 0; i < mElementList.Count; i++)
        {
            GameController.Instance.CheckAutoSell[i] = false;
            mElementList[i].bToggleIsOn(false);
            mElementList[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < mButtonElementList.Count;i++)
        {
            mButtonElementList[i].bToggleIsOn(false);
            mButtonElementList[i].gameObject.SetActive(false);
        }
        mButtonElementList[0].gameObject.SetActive(true);

        mAllSellToggle.SetIsOnWithoutNotify(false);
        GameController.Instance.CheckAllSell = false;
    }
    private void Update()
    {
        for(int i =0;i< mElementList.Count;i++)
        {
            if(GameController.Instance.CheckAutoSell[i])
            {
                SellGem(i, mElementList[i].GetMaxSellAmount());
                mElementList[i].bToggleIsOn(true);
            }
        }
    }
    public void SetAllSellToggle(bool bOn)
    {
        mAllSellToggle.SetIsOnWithoutNotify(bOn);
        GameController.Instance.CheckAllSell = bOn;
    }
    private void Load()
    {
        for (int i = 0; i < mInfoArr.Length; i++)
        {
            if (i % 4 == 0)
            {
                LayerButtonUIElement Buttonelement = Instantiate(mLayerButtonPrefab, mElementArea);
                Buttonelement.Init(i/4);
                mButtonElementList.Add(Buttonelement);
            }

            mIconArr[i] = mOriginArr[i/4].GetSprite(i%4);

            GemSellUIElement element = Instantiate(mElementPrefab, mElementArea);
            element.Init(i, mIconArr[i],
                        mTextInfoArr[i].Title,
                        string.Format("가격 : {0} 원", UnitSetter.GetUnitStr(mInfoArr[i].Cost)),
                        SellGem);


            mElementList.Add(element);

            mElementList[i].gameObject.SetActive(false);
            if (i % 4 == 3)
            {
                mButtonElementList[mButtonElementList.Count - 1].bToggleIsOn(false);
                mButtonElementList[mButtonElementList.Count-1].setting(mElementList[i-3], mElementList[i-2], mElementList[i-1], mElementList[i]);
            }
            if (i / 4 <= GameController.Instance.Stage)
            {
                mButtonElementList[i / 4].gameObject.SetActive(true);
            }
        }

        mAllSellToggle.onValueChanged.AddListener((bOn) =>
        {
            GameController.Instance.CheckAllSell = bOn;
            for (int i = 0; i < mElementList.Count; i++)
            {
                if (i%4==0)
                {
                    if (bOn)
                    {
                        GameController.Instance.CheckAutoSell[i] = bOn;
                        SellGem(i, mElementList[i].GetMaxSellAmount());
                        mElementList[i].bToggleIsOn(true);
                    }
                    else
                    {
                        GameController.Instance.CheckAutoSell[i] = false;
                        mElementList[i].bToggleIsOn(false);
                    }
                }
            }
        });

        SetAllSellToggle(GameController.Instance.CheckAllSell);

    }
    public void SetSellUI(int num)
    {
        mButtonElementList[num].gameObject.SetActive(true);
    }
    public void RefreshGemData()
    {
        for(int i =0; i<mInfoArr.Length;i++)
        {
            mElementList[i].CalSellAmount();
        }
        CraftController.Instance.CheckCraftButton();
    }
    public void ReSetSlider()
    {
        for (int i = 0; i < mInfoArr.Length; i++)
        {
            mElementList[i].ReSetSlider();
        }
    }

        
    public void SellGem(int id, double amount)
    {
        double cost = mInfoArr[id].Cost * amount;

        int div;
        div = id % 4;
        switch (div)
        {
            case 0:
                GameController.Instance.AddAmoutGem_O[id / 4]-=amount;
                break;
            case 1:
                GameController.Instance.AddAmoutGem_B[id / 4] -= amount;
                break;
            case 2:
                GameController.Instance.AddAmoutGem_A[id / 4] -= amount;
                break;
            case 3:
                GameController.Instance.AddAmoutGem_S[id / 4] -= amount;
                break;
        }
        GameController.Instance.Gold += cost;
        mElementList[id].CalSellAmount();
        mElementList[id].ReSetSlider();
    }

}
