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

    public double BuffAmount;
}

[Serializable]
public class CoworkerTextInfo
{
    public int ID;
    public string Title;
    public string ContentsFormat;
    public string GemName;

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
    public string GemName;

}
public class QuestTextInfo
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
public class GemSellInfo
{
    public int ID;
    public double Cost;

}
[Serializable]
public class GemSellTextInfo
{
    public int ID;
    public string Title;
    public string ContentsFormat;

}
[Serializable]
public class EarthInfo
{
    public int ID;
    public double MaxProgress;

}
[Serializable]
public class SaveData
{
    public double Gold;

    public double[] AmoutGem_O;
    public double[] AmoutGem_B;
    public double[] AmoutGem_A;
    public double[] AmoutGem_S;

    public int PlayerLevel;
    public int[] CoworkerLevelArr;
    public float[] ItemCooltimeArr;
    public float[] ItemMaxCooltimeArr;
    public int[] HaveItmeArr;

    public int Stage;
    public int PlayerPos;
    public double Progress;

    public int[] MineArr;
    public double[] GetFromMine;

    public bool[] AutoSellCheck;
    public bool AllSellCheck;
    public bool ScrollPinCheck;

    public double StartTime;
    public double EndTime;
    public double CheckDay;
    public double RandomDaziFinishTime;
    public double AutoClilckAdsFinishTime;

    public int PlayMoleCount;
    public int WhackCount;
    public int MineCount;
    public int RandomDaziCount;

    public int Quest_PlayerLevel;
    public int Quest_CoworkerLevelSum;
    public int Quest_DigCount;
    public int Quest_CraftGem;
    public int Quest_MoleCount;
    public int Quest_MineCount;
    public int Quest_SilverDazi;
    public int Quest_GoldDazi;

    public int ClickAmount;
    public int Achieve_Click;

    public int Achive_Mole;
    public int Achive_AutoClick;
    public int Achive_Norini;
    public int Achive_Coal;
    public int Achive_Ame;
    public int Achive_Gold;
    public int Achive_Dia;
    public int Achive_Vib;

    public int EatAmount;
    public int Achive_Dosirak;

    public int UseSilverAmount;
    public int UseGoldAmount;
    public int Achive_Silver_FLEX;
    public int Achive_Gold_FLEX;

    public int Achive_Earth;
    public double EarthCurrentProgress;

    public int Attend_Reward;
    public int Check_Attend_Reward;

    public int PlayerProfile;
    public int FirstTry;
    public int ChangeName;
    public int GemSellTutorial;
    public int PlayerLevelUpTutorial;
    public int MoleTutorial;
    public int StagePinTutorial;
    public int CraftGemTutorial;
    public int MineTutorial;

}