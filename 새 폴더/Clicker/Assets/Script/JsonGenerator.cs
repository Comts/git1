using System.IO; //파일 입출력 관리 (파일을 읽고 쓸 때) 
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
        PlayerStatText[] infoArr = PlayerUpgradeController.Instance.GetTextInfoArr();
        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented); //파라미터로 준 Formatting.Indented은 자동으로 라인/들여쓰기된 문자열을 리턴
        WriteFile(data, "PlayerItemText.json");
    }

    public void GeneratePlayerItemInfo()
    {
        PlayerStat[] infoArr = PlayerUpgradeController.Instance.GetInfoArr();
        string data = JsonConvert.SerializeObject(infoArr,Formatting.Indented); 
        WriteFile(data, "PlayerItem.json");
    }

    private void WriteFile(string data, string filename) //데이터 저장 (테이블로 쓰기 위해 사용, 세이브 위한 거 아님)
    {
        string fileLocation = string.Concat(Application.dataPath, "/", filename);

        StreamWriter writer = new StreamWriter(fileLocation); //있으면 생성 없으면 덮어쓰기
        writer.Write(data);
        writer.Close();

        //Debug.Log(Application.dataPath); //데이터 클래스를 에디터에서만 쓸 수 있음.(에디터에서만 사용할 때만 사용)
        //Debug.Log(Application.persistentDataPath); //안드로이드의 정해진 패스로 들어감. (별로 안 좋음)
        //Debug.Log(Application.streamingAssetsPath); //위와 같은 기능 
    }
}
