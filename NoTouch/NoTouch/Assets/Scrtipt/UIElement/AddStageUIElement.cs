using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddStageUIElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Text mCostText;
    [SerializeField]
    private Button mAddButton;
#pragma warning restore 0649

    private int mID;
    public void Init(int id,
                     string cost,
                     Delegates.TwoIntInVoidCallback callback)
    {
        mID = id;
        Refresh( cost);

        mAddButton.onClick.AddListener(() =>
        {
            callback(mID, 1);
        });
    }
    public void Refresh(string cost)
    {
        mCostText.text = cost;
    }

}
