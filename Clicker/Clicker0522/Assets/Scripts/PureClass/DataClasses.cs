﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class PlayerStat
{
    public int ID;
    public int CurrentLevel;
    public int MaxLevel;

    public eCostType CostType;
    public double CostBase;
    public double CostWeight;
    public double CostCurrent;
    public double CostTenWeight;

    public bool IsPercent;
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

    public eCostType CostType;
    public double CostBase;
    public double CostWeight;
    public double CostCurrent;
    public double CostTenWeight;

    public float PeriodBase;
    public float PeriodCurrent;
    public float PeriodUpgradeAmount;
    public int PeriodLevelStep;

    public eCalculationType ValueCalcType;
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
public class SaveData
{
    public double Gold;

    public int Stage;
    public int LastGemID;
    public double Progress;

    public int[] PlayerItemLevelArr;
    public float[] SkillCooltimeArr;
    public float[] SkillMaxCooltimeArr;

    public int[] CoworkerLevelArr;
}