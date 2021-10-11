using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomController : MonoBehaviour
{
    public static CustomController Instance;
    private string mInputName;
    private Sprite[] mSpriteArr;
#pragma warning disable 0649
    [SerializeField]
    private Image mProfile,BuyCustomApply;
#pragma warning restore 0649
    private int mCurrentProfile, mPreviousProfile, mNextProfile;
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
        LoadProfile();
        mCurrentProfile = GameController.Instance.PlayerProfile;
    }
    public void LoadProfile()
    {
        if (mSpriteArr == null)
        {
            mSpriteArr = Resources.LoadAll<Sprite>(Paths.PROFILE);
        }
        else
        {
            mSpriteArr = null;
            mSpriteArr = Resources.LoadAll<Sprite>(Paths.PROFILE);
        }
        ShowSettingImage();
    }
    public void CheckName()
    {
        mInputName = PlayerPrefs.GetString("Name");
        if (mInputName != null)
        {
            PlayerUpgradeController.Instance.ChageName(mInputName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadStringName(string name)
    {
        PlayerPrefs.SetString("Name", name);
        PlayerUpgradeController.Instance.ChageName(name);
    }
    public void ShowNextImage()
    {
        mNextProfile = mCurrentProfile+1;
        if(mNextProfile >= mSpriteArr.Length)
        {
            mNextProfile = 0;
        }
        ShowImage(mNextProfile);
    }
    public void ShowPreviousImage()
    {
        mPreviousProfile = mCurrentProfile-1;
        if(mPreviousProfile < 0)
        {
            mPreviousProfile = mSpriteArr.Length-1;
        }
        ShowImage(mPreviousProfile);
    }
    public void ShowImage(int i)
    {
        mProfile.sprite = mSpriteArr[i];
        mCurrentProfile = i;
    }
    public void ShowSettingImage()
    {
        mProfile.sprite = mSpriteArr[GameController.Instance.PlayerProfile];
    }
    public void SelectImage()
    {
        if (IAPController.Instance.CustomApply)
        {
            GameController.Instance.PlayerProfile = mCurrentProfile;
            PlayerUpgradeController.Instance.ChangeProfile();
            StageController.Instance.ChangePlayerHeadImage();
        }
        else
        {
            BuyCustomApply.gameObject.SetActive(true);
        }
    }
}
