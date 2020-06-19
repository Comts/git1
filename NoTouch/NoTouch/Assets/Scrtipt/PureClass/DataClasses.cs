using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    public int ID;
    public int CurrentLevel;
    public int MaxLevel;

    public double CostBase;
    public double CostWeight;
    public double CostCurrent;

    public double ValueBase;
    public double ValueWeight;
    public double ValueCurrent;

    public float Cooltime;
    public float Duration;
}
[Serializable]
public class PlayerStatText
{
    public int ID;
    public string Title;
    public string ContentsFormat;
}
[Serializable]
public class CoworkerInfo
{
    public int ID;
    public int CurrentLevel;
    public int MaxLevel;
    
    public double CostBase;
    public double CostWeight;
    public double CostCurrent;

    public float PeriodBase;
    public float PeriodCurrent;
    public float PeriodUpgradeAmount;
    public int PeriodLevelStep;

    public double ValueBase;
    public double ValueWeight;
    public double ValueCurrent;
}

[Serializable]
public class CoworkerTextInfo
{
    public int ID;
    public string Title;
    public string ContentsFormat;

}
[Serializable]
public class MineInfo
{
    public int ID;
    public double Cost;

}
[Serializable]
public class MineTextInfo
{
    public int ID;
    public string Title;
    public string ContentsFormat;

}

[Serializable]
public class CraftInfo
{
    public int ID;
    public double MaxProgress;

}
[Serializable]
public class CraftTextInfo
{
    public int ID;
    public string Title;
    public string ContentsFormat;

}
[Serializable]
public class SaveData
{
    public double Gold;

    public double[] AmoutGem_A;
    public double[] AmoutGem_S;
    public double[] AmoutGem_SS;
    public double[] AmoutGem_SSS;

    public int[] PlayerLevelArr;
    public int[] CoworkerLevelArr;
    public float[] SkillCooltimeArr;
    public float[] SkillMaxCooltimeArr;

    public int Stage;
    public int PlayerPos;
    public double Progress;

    public int[] MineArr;
    public double[] GetFromMine;


}