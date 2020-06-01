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
        Debug.Log("LoadGame");
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
            Debug.Log("SaveData");
        }
        FixSaveData();
    }

    protected void FixSaveData()
    {

        if (mUser.SkillCooltimeArr == null)
        {
            mUser.SkillCooltimeArr = new float[Constants.SKILL_COUNT];
        }
        else if (mUser.SkillCooltimeArr.Length != Constants.SKILL_COUNT)
        {
            float[] temp = new float[Constants.SKILL_COUNT];
            int count = Mathf.Min(Constants.SKILL_COUNT, mUser.SkillCooltimeArr.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.SkillCooltimeArr[i];
            }
            mUser.SkillCooltimeArr = temp;
        }

        if (mUser.SkillMaxCooltimeArr == null)
        {
            mUser.SkillMaxCooltimeArr = new float[Constants.SKILL_COUNT];
        }
        else if (mUser.SkillMaxCooltimeArr.Length != Constants.SKILL_COUNT)
        {
            float[] temp = new float[Constants.SKILL_COUNT];
            int count = Mathf.Min(Constants.SKILL_COUNT, mUser.SkillMaxCooltimeArr.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.SkillMaxCooltimeArr[i];
            }
            mUser.SkillMaxCooltimeArr = temp;
        }
    }

    protected void CreateNewSaveData()
    {
        mUser = new SaveData();
        mUser.Gold = 0;
        mUser.AmoutGem_A = new double[Constants.Max_floor];
        mUser.AmoutGem_S = new double[Constants.Max_floor];
        mUser.AmoutGem_SS = new double[Constants.Max_floor];
        mUser.AmoutGem_SSS = new double[Constants.Max_floor];

        mUser.PlayerLevel = 0;
        mUser.CoworkerLevelArr = new int [Constants.Max_floor];
        mUser.SkillCooltimeArr = new float[Constants.SKILL_COUNT];
        mUser.SkillMaxCooltimeArr = new float[Constants.SKILL_COUNT];

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