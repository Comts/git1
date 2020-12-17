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
    public void Init(int id)
    {
        mTitleText.text = string.Format("B{0}",id+1); 

    }
    public void setting (GemSellUIElement GemSellUIElement1, GemSellUIElement GemSellUIElement2, GemSellUIElement GemSellUIElement3, GemSellUIElement GemSellUIElement4, GemSellUIElement GemSellUIElement5)
    {
        mToggle.onValueChanged.AddListener((bOn) =>
        {
            GemSellUIElement1.gameObject.SetActive(bOn);
            GemSellUIElement2.gameObject.SetActive(bOn);
            GemSellUIElement3.gameObject.SetActive(bOn);
            GemSellUIElement4.gameObject.SetActive(bOn);
            GemSellUIElement5.gameObject.SetActive(bOn);
        });
    }
    public void bToggleIsOn(bool f)
    {
        mToggle.SetIsOnWithoutNotify(f);
    }
}
