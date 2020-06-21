using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftController : InformationLoader
{
    public static CraftController Instance;
    [SerializeField]
    private CraftInfo[] mInfoArr;
    [SerializeField]
    private CraftTextInfo[] mTextInfoArr;
    private Sprite[] mIconArr;
#pragma warning disable 0649
    [SerializeField]
    private CraftUIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;

    private List<CraftUIElement> mElementList;

    [SerializeField]
    private GaugeBar mGaugeBar;
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

        LoadJson(out mInfoArr, Paths.CRAFT_INFO_TABLE);
        LoadJson(out mTextInfoArr,
            Paths.CRAFT_TEXT_INFO_TABLE +
            Paths.LANGUAGE_TYPE_ARR[GameController.Instance.LanguageType]);

        mIconArr = Resources.LoadAll<Sprite>(Paths.CRAFT_ICON);

        mElementList = new List<CraftUIElement>();
        Load();
    }
    private void Update()
    {
        for (int i = 0; i < mInfoArr.Length; i++) 
        { 
            if (GameController.Instance.AddAmoutGem_A[i] < 1000)
            {
                mElementList[i].SetCraftButtonActive(false);
            }
            else
            {
                mElementList[i].SetCraftButtonActive(true);
            }
        }
    }
    private void Load()
    {
        for (int i = 0; i < mInfoArr.Length; i++)
        {



            CraftUIElement element = Instantiate(mElementPrefab, mElementArea);
            element.Init(i, mIconArr[i],
                        mTextInfoArr[i].Title,
                        mTextInfoArr[i].ContentsFormat,
                        //string.Format("{0}EA", GameController.Instance.AddAmoutGem_A[i]),
                        "1000EA",
                        StartCraft);


            mElementList.Add(element);
            if (GameController.Instance.AddAmoutGem_A[i] < 1000)
            {
                mElementList[i].SetCraftButtonActive(false);
            }
        }
    }
    public void StartCraft(int id, int amount)
    {
        GameController.Instance.AddAmoutGem_A[id] -= amount;
    }
    public void ShowGaugeBar(double current, double max)
    {
        string progressStr = string.Format("{0} / {1}",
                                            UnitSetter.GetUnitStr(current),
                                            UnitSetter.GetUnitStr(max));
        float progress = (float)(current / max);
        mGaugeBar.ShowGaugeBar(progress, progressStr);
    }
}
