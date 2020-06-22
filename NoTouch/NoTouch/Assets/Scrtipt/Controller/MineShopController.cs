using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineShopController : InformationLoader
{
    public static MineShopController Instance;
    [SerializeField]
    private MineInfo[] mInfoArr;
    [SerializeField]
    private MineTextInfo[] mTextInfoArr;
    private Sprite[] mIconArr;

    private List<MineUIElement> mElementList;
#pragma warning disable 0649
    [SerializeField]
    private MineUIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;
    [SerializeField]
    private float mCurrentTime;

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
    void Start()
    {

        LoadJson(out mInfoArr, Paths.MINE_INFO_TABLE);
        LoadJson(out mTextInfoArr,
            Paths.MINE_TEXT_INFO_TABLE +
            Paths.LANGUAGE_TYPE_ARR[GameController.Instance.LanguageType]);

        mIconArr = Resources.LoadAll<Sprite>(Paths.MINE_ICON);

        mElementList = new List<MineUIElement>();
        Load();
    }
    private void Update()
    {
        mCurrentTime += Time.deltaTime;
        if (mCurrentTime >= 5)
        {
            for (int i = 0; i < Constants.MINE_COUNT; i++)
            {
                if(GameController.Instance.HaveMine[i] > 0)
                {
                    GameController.Instance.AddFromMine[i]++;
                }
            }
            mCurrentTime = 0;
        }
    }

    private void Load()
    {
        for (int i = 0; i < mInfoArr.Length; i++)
        {



            MineUIElement element = Instantiate(mElementPrefab, mElementArea);
            element.Init(i, mIconArr[i],
                        mTextInfoArr[i].Title,
                        mTextInfoArr[i].ContentsFormat,
                        UnitSetter.GetUnitStr(mInfoArr[i].Cost),
                        BuyMine, SellMine);


            mElementList.Add(element);
            if (GameController.Instance.HaveMine[i] != 0)
            {
                mElementList[i].SetBuyButtonActive(false);
            }
        }
    }
    public void BuyMine(int id, int amount)
    {
        Delegates.VoidCallback callback = () => { BuyCallback(id, amount); };
        GameController.Instance.GoldCallback = callback;
        double cost = mInfoArr[id].Cost;
        GameController.Instance.Gold -= cost;

    }
    public void BuyCallback(int id, int add)
    {

        GameController.Instance.HaveMine[id] = add;
        if (GameController.Instance.HaveMine[id]!=0)
        {
            mElementList[id].SetBuyButtonActive(false);
        }

    }
    public void SellMine(int id, int amount)
    {
        double cost = mInfoArr[id].Cost;
        GameController.Instance.Gold += cost;
        GameController.Instance.AddAmoutGem_O[id] += GameController.Instance.AddFromMine[id];
        GameController.Instance.AddFromMine[id] = 0;
        GameController.Instance.HaveMine[id] = 0;
        mElementList[id].SetBuyButtonActive(true);
        mElementList[id].Refresh(mTextInfoArr[id].ContentsFormat, UnitSetter.GetUnitStr(mInfoArr[id].Cost));

    }
}
