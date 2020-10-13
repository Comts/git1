using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIElement : MonoBehaviour
{
    private int mID;
    [SerializeField]
    private Image mIconImage;
    [SerializeField]
    private Text mTileText, mContentsText, mLevelText, mCostText;
    [SerializeField]
    private Button mPurchaseButton;
    //public void Init(Item item)//UI쪾은 범용적으로 쓸수 있겠끔 파라메터 여러개로 (표시할게 같으므로)
    public void Init(Sprite icon, int id, string name, string contents, int level, double cost, Delegates.IntInvoidReturn callback) //UnityAction 파라메터 불가능
    {
        mIconImage.sprite=icon;
        mID = id;
        mTileText.text = name;
        mContentsText.text = contents;
        mLevelText.text = level.ToString();
        mCostText.text = cost.ToString();
        mPurchaseButton.onClick.AddListener(()=>{callback(mID); }); 
    }
    //public void SetButtonOnClick(ShopController shop)
    //{
       
    //    mPurchaseButton.onClick.AddListener(() => { shop.LevelUp(mID); });//람다함수
    //    //mPurchaseButton.onClick.AddListener(delegate { shop.LevelUp(0); });//동일하지만 파라메터 불가능
    //    //mPurchaseButton.onClick.AddListener(LevelUp); //AddListener 파라미터 없어야됨.위아동일
    //}

    //ShopController mShop;
    //public void LevelUp()
    //{
    //    mShop.LevelUp(0);
    //}
}
