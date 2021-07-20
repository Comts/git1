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
        //PlayerStatText info = new PlayerStatText[Constants.PLAYER_STAT_COUNT];// PlayerUpgradeController.Instance.GetTextInfoArr();
        PlayerStatText info = new PlayerStatText();
        info.ID = 0;
        info.Title = "장인의손길";
        string data = JsonConvert.SerializeObject(info, Formatting.Indented);
        WriteFile(data, "PlayerText.json");
    }
    public void GeneratePlayerInfo()
    {
        //PlayerStat info = new PlayerStat[Constants.PLAYER_STAT_COUNT];//PlayerUpgradeController.Instance.GetInfoArr();
        PlayerStat info = new PlayerStat();
        info.ID = 0;
        string data = JsonConvert.SerializeObject(info, Formatting.Indented);
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
