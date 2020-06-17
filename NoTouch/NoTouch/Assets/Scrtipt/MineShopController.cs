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

    private List<UIElement> mElementList;
#pragma warning disable 0649
    [SerializeField]
    private UIElement mElementPrefab;
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
    void Start()
    {

        LoadJson(out mInfoArr, Paths.MINE_INFO_TABLE);
        LoadJson(out mTextInfoArr,
            Paths.MINE_TEXT_INFO_TABLE +
            Paths.LANGUAGE_TYPE_ARR[GameController.Instance.LanguageType]);

        mIconArr = Resources.LoadAll<Sprite>(Paths.MINE_ICON);

        mElementList = new List<UIElement>();
        Load();
    }

    private void Load()
    {
        for (int i = 0; i < mInfoArr.Length; i++)
        {



            UIElement element = Instantiate(mElementPrefab, mElementArea);
            element.Init(i, mIconArr[i],
                        mTextInfoArr[i].Title,
                        null,
                        mTextInfoArr[i].ContentsFormat,
                        UnitSetter.GetUnitStr(mInfoArr[i].Cost),
                        BuyMine);
            Debug.Log(mInfoArr[i].Cost);


            mElementList.Add(element);
            if (GameController.Instance.HaveMine[i] != 0)
            {
                mElementList[i].SetButtonActive(false);
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
            mElementList[id].SetButtonActive(false);
        }

    }
}
