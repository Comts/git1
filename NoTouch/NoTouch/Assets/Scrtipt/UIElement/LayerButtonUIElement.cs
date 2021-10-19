using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerButtonUIElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Text mTitleText;
    [SerializeField]
    private Toggle mToggle;
#pragma warning restore 0649
    private int mID;
    private GemSellUIElement UI1, UI2, UI3, UI4;
    public void Init(int id)
    {
        mTitleText.text = string.Format("B{0}",id+1);
        mID = id;
    }
    public void setting (GemSellUIElement GemSellUIElement1, GemSellUIElement GemSellUIElement2, GemSellUIElement GemSellUIElement3, GemSellUIElement GemSellUIElement4, Image LayerPoint, Image SellPoint)
    {
        UI1 = GemSellUIElement1;
        UI2 = GemSellUIElement2;
        UI3 = GemSellUIElement3;
        UI4 = GemSellUIElement4;

        mToggle.onValueChanged.AddListener((bOn) =>
        {
            GemSellUIElement1.gameObject.SetActive(bOn);
            GemSellUIElement2.gameObject.SetActive(bOn);
            GemSellUIElement3.gameObject.SetActive(bOn);
            GemSellUIElement4.gameObject.SetActive(bOn);
            LayerPoint.gameObject.SetActive(false);

            if (GameController.Instance.GemSellTutorial ==0)
            {
                SellPoint.gameObject.SetActive(bOn);
            }


        });
    }
    public void bToggleIsOn(bool f)
    {
        mToggle.SetIsOnWithoutNotify(f);
    }
    public void SetGemSellUI()
    {
        mToggle.SetIsOnWithoutNotify(true);
        UI1.gameObject.SetActive(true);
        UI2.gameObject.SetActive(true);
        UI3.gameObject.SetActive(true);
        UI4.gameObject.SetActive(true);
    }
}
