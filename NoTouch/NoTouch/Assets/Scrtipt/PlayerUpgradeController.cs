using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgradeController : InformationLoader
{
    public static PlayerUpgradeController Instance;

    [SerializeField]
    private Image mIconImage;
    [SerializeField]
    private Text mTitleText, mLevelText, mContentsText, mCostText;
    [SerializeField]
    private Button mButton;

    private int mPlayerLevel;
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
        mPlayerLevel = GameController.Instance.GetPlayerLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
