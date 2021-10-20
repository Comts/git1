using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetGemEffect : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image mIcon;
#pragma warning restore 0649


    public void SetIcon(Sprite sprite)
    {
        mIcon.sprite = sprite;
    }
}
