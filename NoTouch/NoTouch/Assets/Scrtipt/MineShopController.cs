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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
