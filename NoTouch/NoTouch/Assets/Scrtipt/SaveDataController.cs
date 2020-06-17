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
        if (mUser.PlayerLevelArr == null)
        {
            mUser.PlayerLevelArr = new int[Constants.PLAYER_STAT_COUNT];
            mUser.PlayerLevelArr[0] = 1;
        }
        else if (mUser.PlayerLevelArr.Length != Constants.PLAYER_STAT_COUNT)
        {
            int[] temp = new int[Constants.PLAYER_STAT_COUNT];
            int count = Mathf.Min(Constants.PLAYER_STAT_COUNT, mUser.PlayerLevelArr.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.PlayerLevelArr[i];
            }
            mUser.PlayerLevelArr = temp;
        }

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
        if (mUser.CoworkerLevelArr == null)
        {
            mUser.CoworkerLevelArr = new int[Constants.MAX_fLOOR];
            for (int i = 0; i < mUser.CoworkerLevelArr.Length; i++)
            {
                mUser.CoworkerLevelArr[i] = -1;
            }
            mUser.CoworkerLevelArr[0] = 0;
        }
        else if (mUser.CoworkerLevelArr.Length != Constants.MAX_fLOOR)
        {
            int[] temp = new int[Constants.MAX_fLOOR];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = -1;
            }
            temp[0] = 0;
            int count = Mathf.Min(Constants.MAX_fLOOR, mUser.CoworkerLevelArr.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.CoworkerLevelArr[i];
            }
            mUser.CoworkerLevelArr = temp;
        }

        if (mUser.MineArr == null)
        {
            mUser.MineArr = new int[Constants.MINE_COUNT];
        }
        else if (mUser.MineArr.Length != Constants.MINE_COUNT)
        {
            int[] temp = new int[Constants.MINE_COUNT];
            int count = Mathf.Min(Constants.MINE_COUNT, mUser.MineArr.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.MineArr[i];
            }
            mUser.MineArr = temp;
        }
    }

    protected void CreateNewSaveData()
    {
        mUser = new SaveData();
        mUser.Gold = 0;
        mUser.AmoutGem_A = new double[Constants.MAX_fLOOR];
        mUser.AmoutGem_S = new double[Constants.MAX_fLOOR];
        mUser.AmoutGem_SS = new double[Constants.MAX_fLOOR];
        mUser.AmoutGem_SSS = new double[Constants.MAX_fLOOR];


        mUser.Stage = 0;
        mUser.PlayerPos = 0;
        mUser.Progress = 0;

        mUser.PlayerLevelArr = new int[Constants.PLAYER_STAT_COUNT];
        mUser.SkillCooltimeArr = new float[Constants.SKILL_COUNT];
        mUser.SkillMaxCooltimeArr = new float[Constants.SKILL_COUNT];

        mUser.CoworkerLevelArr = new int [Constants.MAX_fLOOR];
        for (int i = 0; i < mUser.CoworkerLevelArr.Length; i++)
        {
            mUser.CoworkerLevelArr[i] = -1;
        }
        mUser.CoworkerLevelArr[0] = 0;

        mUser.MineArr = new int[Constants.MINE_COUNT];


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