using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField]
    private Image[] mWindowArr;
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
