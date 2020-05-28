using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{
    [SerializeField]
    protected SaveData mUser;

    protected void LoadGame()
    {
        string data = PlayerPrefs.GetString("SaveData");
        if (string.IsNullOrEmpty(data))
        {
            CreateNewSaveData();
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));
            mUser = (SaveData)formatter.Deserialize(stream);
        }
        FixSaveData();
    }

    protected void FixSaveData()
    {

    }

    protected void CreateNewSaveData()
    {
        mUser = new SaveData();
        mUser.TotalGold = 0;
        mUser.AmoutGem_A = new double[Constants.Max_floor];
        mUser.AmoutGem_S = new double[Constants.Max_floor];
        mUser.AmoutGem_SS = new double[Constants.Max_floor];
        mUser.AmoutGem_SSS = new double[Constants.Max_floor];

        mUser.PlayerLevel = 0;
        mUser.CoworkerLevelArr = new int [Constants.Max_floor];

        mUser.Stage = 1;
        mUser.PlayerPos = 1;
        mUser.Progress = 0;


    }

    protected void Save()
    {

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        formatter.Serialize(stream, mUser);
        string data = Convert.ToBase64String(stream.GetBuffer());
        PlayerPrefs.SetString("SaveData", data);
    }
}