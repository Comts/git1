using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
#pragma warning disable 0649
    [SerializeField]
    private Image[] mWindowArr;
#pragma warning restore 0649
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
    public void Popwindow(int id)
    {

        if (!mWindowArr[id].gameObject.activeInHierarchy)
        {
            mWindowArr[id].gameObject.SetActive(true);
            for (int i = 0; i < mWindowArr.Length; i++)
            {
                if (i == id)
                {
                    continue;
                }
                mWindowArr[i].gameObject.SetActive(false);
            }
        }
        else
        {
            mWindowArr[id].gameObject.SetActive(false);
        }
    }

}
