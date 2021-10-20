using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class InAppUpdateController : MonoBehaviour
{
    [SerializeField]
    public Image UpdateWindow;
    [SerializeField]
    public Text MarketVersion,AppVersion;

    string url = "https://play.google.com/store/apps/details?id=com.Comts.NoTouch";
    // Start is called before the first frame update
    private void Start()
    {
        CheckUpdate();
    }
    public void CheckUpdate()
    {
        UnsafeSecurityPolicy.Instate();
        string marketVersion = "";

        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(url);

        foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//span[@class='htlgb']"))
        {
            marketVersion = node.InnerText.Trim();
            
            if(marketVersion != null)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(marketVersion, @"^\d{1}\.\d{1}\.\d{1}$"))
                //if (System.Text.RegularExpressions.Regex.IsMatch(marketVersion, @"^\d{1}\.\d{1}$"))
                {
                    string a = marketVersion.ToString();
                    MarketVersion.text = string.Format("최신 버전 : {0}", marketVersion.ToString());
                    string b = Application.version.ToString();
                    AppVersion.text = string.Format("현재 버전 : {0}", Application.version.ToString());

                    if (a!= b)
                    {
                        UpdateWindow.gameObject.SetActive(true);
                    }
                }
            }
        }


    }

    public void OpenUpdate()
    {
        Application.OpenURL(url);
    }
}
