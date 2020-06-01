using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonGenerator : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }
    
    
    public void GeneratePlayerItemInfo()
    {
        PlayerStat infoArr = new PlayerStat();//PlayerUpgradeController.Instance.GetInfoArr();
        
        infoArr.ID = 0;
        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);
        WriteFile(data, "Player.json");
    }

    private void WriteFile(string data, string fileName)
    {
        string fileLocation = string.Concat(Application.dataPath, "/", fileName);

        StreamWriter writer = new StreamWriter(fileLocation);
        writer.Write(data);
        writer.Close();
    }

}
