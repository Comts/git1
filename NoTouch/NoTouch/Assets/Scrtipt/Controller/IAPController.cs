using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class IAPController : MonoBehaviour, IStoreListener
{
    public static IAPController Instance;
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.


    private Coroutine mCor_AutoClick1;

#pragma warning disable 0649
    [SerializeField] GameObject StarterPack1;
    [SerializeField] GameObject StarterPack2;
    [SerializeField] GameObject AutoClick1;
    [SerializeField] GameObject AutoClick2;
    [SerializeField] GameObject CustomApplyWindow;
#pragma warning restore 0649
    public bool CustomApply;

    // Product identifiers for all products capable of being purchased: 
    // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
    // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
    // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

    // General product identifiers for the consumable, non-consumable, and subscription products.
    // Use these handles in the code to reference which product to purchase. Also use these values 
    // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
    // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
    // specific mapping to Unity Purchasing's AddProduct, below.
    //public static string kProductIDConsumable = "consumable";
    //public static string kProductIDNonConsumable = "nonconsumable";
    //public static string kProductIDSubscription = "subscription";

    //// Apple App Store-specific product identifier for the subscription product.
    //private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

    //// Google Play Store-specific product identifier subscription product.
    //private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

    private string Nonconsumable_StarterPack = "StarterPack";
    private string Nonconsumable_StarterPack2 = "StarterPack2";
    private string Nonconsumable_AutoClick = "AutoClick";
    private string Nonconsumable_AutoClick2 = "AutoClick2";
    private string Nonconsumable_CustomApply = "CustomApply";

    private string Consumable_Dazi_Gold_10 = "Dazi_Gold_10"; //자유롭게
    private string Consumable_Dazi_Gold_20 = "Dazi_Gold_20";
    private string Consumable_Dazi_Gold_50 = "Dazi_Gold_50";
    private string Consumable_Dazi_Gold_100 = "Dazi_Gold_100";

    private string Consumable_Dazi_Silver_50 = "Dazi_Silver_50";
    private string Consumable_Dazi_Silver_100 = "Dazi_Silver_100";
    private string Consumable_Dazi_Silver_200 = "Dazi_Silver_200";
    private string Consumable_Dazi_Silver_500 = "Dazi_Silver_500";



    private string GooglePlay_StarterPack = "com.comts.notouch.starterpack";
    private string GooglePlay_StarterPack2 = "com.comts.notouch.starterpack2";
    private string GooglePlay_AutoClick = "com.comts.notouch.autoclick";
    private string GooglePlay_AutoClick2 = "com.comts.notouch.autoclick2";
    private string GooglePlay_CustomApply = "com.comts.notouch.customapply";

    private string GooglePlay_Dazi_Gold_10 = "com.comts.notouch.dazi.gold.10"; //구글플레이 대문자 안됨. 스토어에 등록된대로입력
    private string GooglePlay_Dazi_Gold_20 = "com.comts.notouch.dazi.gold.20";
    private string GooglePlay_Dazi_Gold_50 = "com.comts.notouch.dazi.gold.50";
    private string GooglePlay_Dazi_Gold_100 = "com.comts.notouch.dazi.gold.100";

    private string GooglePlay_Dazi_Silver_50 = "com.comts.notouch.dazi.silver.50";
    private string GooglePlay_Dazi_Silver_100 = "com.comts.notouch.dazi.silver.100";
    private string GooglePlay_Dazi_Silver_200 = "com.comts.notouch.dazi.silver.200";
    private string GooglePlay_Dazi_Silver_500 = "com.comts.notouch.dazi.silver.500";

    // private string iOS_Ruby100 = "r100"; //ID규정을 확인후 따라야됨
    // private string iOS_Starterpack = "s00";

    private IEnumerator Cor_AutoClick(float time)
    {
        WaitForSeconds TouchSec = new WaitForSeconds(time);
        while (true)
        {
            yield return TouchSec;
            GameController.Instance.Touch();
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        CustomApply = false;
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }

        if (CheckHistory(Nonconsumable_StarterPack))
        {
            StarterPack1.SetActive(false);
            if (CheckHistory(Nonconsumable_StarterPack))
            {
                StarterPack2.SetActive(true);
                if(CheckHistory(Nonconsumable_StarterPack2))
                {
                    StarterPack2.SetActive(false);
                }
            }
        }

        if (CheckHistory(Nonconsumable_AutoClick))
        {
            AutoClick1.SetActive(false);
            if (CheckHistory(Nonconsumable_AutoClick))
            {
                if (CheckHistory(Nonconsumable_AutoClick2))
                {
                    //오토클릭2 실행
                    if (mCor_AutoClick1 != null)
                    {
                        StopCoroutine(mCor_AutoClick1);
                    }
                    GameController.Instance.Achive_AutoClick = 1;
                    StartCoroutine(Cor_AutoClick(0.1f));
                }
                else
                {
                    //오토클릭1 실행
                    mCor_AutoClick1 = StartCoroutine(Cor_AutoClick(0.2f));
                    AutoClick2.SetActive(true);
                }

            }
        }

        if (CheckHistory(Nonconsumable_CustomApply))
        {
            CustomApplyWindow.SetActive(false);
            CustomApply = true;
        }
    }
    public void ReStart()
    {
        if (CheckHistory(Nonconsumable_StarterPack))
        {
            GameController.Instance.HaveItem[0] += 100;
            GameController.Instance.HaveItem[1] += 10;
        }
        if (CheckHistory(Nonconsumable_StarterPack2))
        {
            GameController.Instance.HaveItem[0] += 150;
            GameController.Instance.HaveItem[1] += 15;
        }
        ItemUseController.Instance.ShowHaveItem();

    }
    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(Consumable_Dazi_Gold_10, ProductType.Consumable, new IDs(){
            { GooglePlay_Dazi_Gold_10, GooglePlay.Name },
        });
        builder.AddProduct(Consumable_Dazi_Gold_20, ProductType.Consumable, new IDs(){
            { GooglePlay_Dazi_Gold_20, GooglePlay.Name },
        });
        builder.AddProduct(Consumable_Dazi_Gold_50, ProductType.Consumable, new IDs(){
            { GooglePlay_Dazi_Gold_50, GooglePlay.Name },
        });
        builder.AddProduct(Consumable_Dazi_Gold_100, ProductType.Consumable, new IDs(){
            { GooglePlay_Dazi_Gold_100, GooglePlay.Name },
        });

        builder.AddProduct(Consumable_Dazi_Silver_50, ProductType.Consumable, new IDs(){
            { GooglePlay_Dazi_Silver_50, GooglePlay.Name },
        });
        builder.AddProduct(Consumable_Dazi_Silver_100, ProductType.Consumable, new IDs(){
            { GooglePlay_Dazi_Silver_100, GooglePlay.Name },
        });
        builder.AddProduct(Consumable_Dazi_Silver_200, ProductType.Consumable, new IDs(){
            { GooglePlay_Dazi_Silver_200, GooglePlay.Name },
        });
        builder.AddProduct(Consumable_Dazi_Silver_500, ProductType.Consumable, new IDs(){
            { GooglePlay_Dazi_Silver_500, GooglePlay.Name },
        });

        builder.AddProduct(Nonconsumable_StarterPack, ProductType.NonConsumable, new IDs(){
            { GooglePlay_StarterPack, GooglePlay.Name },
        });
        builder.AddProduct(Nonconsumable_StarterPack2, ProductType.NonConsumable, new IDs(){
            { GooglePlay_StarterPack2, GooglePlay.Name },
        });

        builder.AddProduct(Nonconsumable_AutoClick, ProductType.NonConsumable, new IDs(){
            { GooglePlay_AutoClick, GooglePlay.Name },
        });
        builder.AddProduct(Nonconsumable_AutoClick2, ProductType.NonConsumable, new IDs(){
            { GooglePlay_AutoClick2, GooglePlay.Name },
        });

        builder.AddProduct(Nonconsumable_CustomApply, ProductType.NonConsumable, new IDs(){
            { GooglePlay_CustomApply, GooglePlay.Name },
        });

        //// Add a product to sell / restore by way of its identifier, associating the general identifier
        //// with its store-specific identifiers.
        //builder.AddProduct(kProductIDConsumable, ProductType.Consumable);
        //// Continue adding the non-consumable product.
        //builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);
        //// And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
        //// if the Product ID was configured differently between Apple and Google stores. Also note that
        //// one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
        //// must only be referenced here. 
        //builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
        //    { kProductNameAppleSubscription, AppleAppStore.Name },
        //    { kProductNameGooglePlaySubscription, GooglePlay.Name },
        //});

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
        RestorePurchases();
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public bool CheckHistory(string productID)
    {
        Product product = m_StoreController.products.WithID(productID);
        return product.hasReceipt;
    }

    public void BuyDaziGold10()
    {
        BuyProductID(Consumable_Dazi_Gold_10);
    }
    public void BuyDaziGold20()
    {
        BuyProductID(Consumable_Dazi_Gold_20);
    }
    public void BuyDaziGold50()
    {
        BuyProductID(Consumable_Dazi_Gold_50);
    }
    public void BuyDaziGold100()
    {
        BuyProductID(Consumable_Dazi_Gold_100);
    }
    public void BuyDaziSilver50()
    {
        BuyProductID(Consumable_Dazi_Silver_50);
    }
    public void BuyDaziSilver100()
    {
        BuyProductID(Consumable_Dazi_Silver_100);
    }
    public void BuyDaziSilver200()
    {
        BuyProductID(Consumable_Dazi_Silver_200);
    }
    public void BuyDaziSilver500()
    {
        BuyProductID(Consumable_Dazi_Silver_500);
    }
    public void BuyStarterPack()
    {
        if (CheckHistory(Nonconsumable_StarterPack))
        {
            Debug.Log("이미 구매한 상품");
            return;
        }
        BuyProductID(Nonconsumable_StarterPack);
        QuestController.Instance.Achive_Norini();
    }
    public void BuyStarterPack2()
    {
        if (CheckHistory(Nonconsumable_StarterPack))
        {
            if (CheckHistory(Nonconsumable_StarterPack2))
            {
                Debug.Log("이미 구매한 상품");
                return;
            }
            BuyProductID(Nonconsumable_StarterPack2);
        }
    }
    public void BuyAutoClick()
    {
        if (CheckHistory(Nonconsumable_AutoClick))
        {
            Debug.Log("이미 구매한 상품");
            return;
        }
        BuyProductID(Nonconsumable_AutoClick);
    }
    public void BuyAutoClick2()
    {

        if (CheckHistory(Nonconsumable_AutoClick))
        {
            if (CheckHistory(Nonconsumable_AutoClick2))
            {
                Debug.Log("이미 구매한 상품");
                return;
            }
            BuyProductID(Nonconsumable_AutoClick2);
            QuestController.Instance.Achive_AutoClick();
        }
    }
    public void BuyCustomApply()
    {
        if (CheckHistory(Nonconsumable_CustomApply))
        {
            Debug.Log("이미 구매한 상품");
            return;
        }
        BuyProductID(Nonconsumable_CustomApply);
    }

    //public void BuyConsumable()
    //{
    //    // Buy the consumable product using its general identifier. Expect a response either 
    //    // through ProcessPurchase or OnPurchaseFailed asynchronously.
    //    BuyProductID(kProductIDConsumable);
    //}


    //public void BuyNonConsumable()
    //{
    //    // Buy the non-consumable product using its general identifier. Expect a response either 
    //    // through ProcessPurchase or OnPurchaseFailed asynchronously.
    //    BuyProductID(kProductIDNonConsumable);
    //}


    //public void BuySubscription()
    //{
    //    // Buy the subscription product using its the general identifier. Expect a response either 
    //    // through ProcessPurchase or OnPurchaseFailed asynchronously.
    //    // Notice how we use the general product identifier in spite of this ID being mapped to
    //    // custom store-specific identifiers above.
    //    BuyProductID(kProductIDSubscription);
    //}


    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) //args 는 유니티 id로들어옴
    {
        bool validPurchase = true; // Presume valid for platforms with no R.V.

        // Unity IAP's validation logic is only included on these platforms.
#if UNITY_EDITOR
        validPurchase = true;
#elif UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
    // Prepare the validator with the secrets we prepared in the Editor
    // obfuscation window.
    var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
        AppleTangle.Data(), Application.identifier);

    try {
        // On Google Play, result will have a single product Id.
        // On Apple stores receipts contain multiple products.
        var result = validator.Validate(args.purchasedProduct.receipt);
        // For informational purposes, we list the receipt(s)
        Debug.Log("Receipt is valid. Contents:");
        foreach (IPurchaseReceipt productReceipt in result) {
            Debug.Log(productReceipt.productID);
            Debug.Log(productReceipt.purchaseDate);
            Debug.Log(productReceipt.transactionID);
        }
    } catch (IAPSecurityException) {
        Debug.Log("Invalid receipt, not unlocking content");
        validPurchase = false;
    }
#endif

        //// A consumable product has been purchased by this user.
        //if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable, StringComparison.Ordinal))
        //{
        //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        //    // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.

        //}
        //// Or ... a non-consumable product has been purchased by this user.
        //else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
        //{
        //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        //    // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
        //}
        //// Or ... a subscription product has been purchased by this user.
        //else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
        //{
        //    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        //    // TODO: The subscription item has been successfully purchased, grant this to the player.
        //}
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        if (validPurchase)
        {
            if (String.Equals(args.purchasedProduct.definition.id, Consumable_Dazi_Gold_10, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
                //해당아이템 지급
                GameController.Instance.HaveItem[1]+=10;
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Dazi_Gold_20, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                GameController.Instance.HaveItem[1] += 25;
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Dazi_Gold_50, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                GameController.Instance.HaveItem[1] += 75;
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Dazi_Gold_100, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                GameController.Instance.HaveItem[1] += 200;
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Dazi_Silver_50, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                GameController.Instance.HaveItem[0] += 50;
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Dazi_Silver_100, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                GameController.Instance.HaveItem[0] += 120;
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Dazi_Silver_200, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                GameController.Instance.HaveItem[0] += 250;
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Consumable_Dazi_Silver_500, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                GameController.Instance.HaveItem[0] += 700;
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Nonconsumable_StarterPack, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                GameController.Instance.HaveItem[0] += 100;
                GameController.Instance.HaveItem[1] += 10;
                StarterPack1.SetActive(false);
                StarterPack2.SetActive(true);
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Nonconsumable_StarterPack2, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                GameController.Instance.HaveItem[0] += 150;
                GameController.Instance.HaveItem[1] += 15;
                StarterPack2.SetActive(false);
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Nonconsumable_AutoClick, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                //Autoclick1
                AutoClick1.SetActive(false);
                AutoClick2.SetActive(true);
                mCor_AutoClick1 = StartCoroutine(Cor_AutoClick(0.2f));
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Nonconsumable_AutoClick2, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                //Autoclick2
                AutoClick2.SetActive(false);
                if (mCor_AutoClick1 != null)
                {
                    StopCoroutine(mCor_AutoClick1);
                }
                StartCoroutine(Cor_AutoClick(0.1f));
            }
            else if (String.Equals(args.purchasedProduct.definition.id, Nonconsumable_CustomApply, StringComparison.Ordinal))
            {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                CustomApplyWindow.SetActive(false);
                CustomApply = true;

            }
            else
            {
                Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            }
            ItemUseController.Instance.ShowHaveItem();
            // Return a flag indicating whether this product has completely been received, or if the application needs 
            // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
            // saving purchased products to the cloud, and when that save is delayed. 
            return PurchaseProcessingResult.Complete;

        }
        return PurchaseProcessingResult.Pending;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}