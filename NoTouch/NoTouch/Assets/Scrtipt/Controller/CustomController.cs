using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomController : MonoBehaviour
{
    public static CustomController Instance;
    private string mInputName;
    public Sprite[] mSpriteArr;
#pragma warning disable 0649
    [SerializeField]
    private Image mProfile,BuyCustomApply,ChangeSucces;
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
        mCurrentProfile = GameController.Instance.PlayerProfile;
        mSpriteArr = Resources.LoadAll<Sprite>(Paths.PROFILE);
    }
    public void LoadProfile()
    {
        Sprite spr = CustomImage.Instance.CheckCustompath();
        if (spr != null)
        {
            mSpriteArr[mSpriteArr.Length - 1] = spr;
        }
        mProfile.sprite = mSpriteArr[GameController.Instance.PlayerProfile];
    }
    public void CheckName()
    {
        mInputName = PlayerPrefs.GetString("Name");
        if (GameController.Instance.ChangeName==1)
        {
            PlayerUpgradeController.Instance.ChageName(mInputName);
        }
    }
    public void ChangeCustomImage(Sprite spr)
    {
        mSpriteArr[mSpriteArr.Length-1] = spr;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadStringName(string name)
    {
        if(name.Length > 0)
        {
            PlayerPrefs.SetString("Name", name);
            PlayerUpgradeController.Instance.ChageName(name);
            GameController.Instance.ChangeName = 1;
        }
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
    public void SelectImage()
    {
        if (IAPController.Instance.CustomApply)
        {
            GameController.Instance.PlayerProfile = mCurrentProfile;
            PlayerUpgradeController.Instance.ChangeProfile();
            StageController.Instance.ChangePlayerHeadImage();
            EarthController.Instance.ChangePlayerHead();
            ChangeSucces.gameObject.SetActive(true);
        }
        else
        {
            BuyCustomApply.gameObject.SetActive(true);
        }
    }
}
