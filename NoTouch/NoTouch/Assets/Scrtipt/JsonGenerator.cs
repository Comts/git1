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


    public void GeneratePlayerTextInfo()
    {
        PlayerStatText[] infoArr = new PlayerStatText[Constants.PLAYER_STAT_COUNT];// PlayerUpgradeController.Instance.GetTextInfoArr();
        infoArr[0] = new PlayerStatText();
        infoArr[0].ID = 0;
        infoArr[0].Title = "장인의손길";
        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);
        WriteFile(data, "PlayerText.json");
    }
    public void GeneratePlayerInfo()
    {
        PlayerStat[] infoArr = new PlayerStat[Constants.PLAYER_STAT_COUNT];//PlayerUpgradeController.Instance.GetInfoArr();
        infoArr[0] = new PlayerStat();
        infoArr[0].ID = 0;
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
