using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomController : MonoBehaviour
{
    public static CustomController Instance;
    private string mInputName;
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
}
