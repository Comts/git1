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

    public double CostBase;
    public double CostWeight;
    public double CostCurrent;

    public double ValueBase;
    public double ValueWeight;
    public double ValueCurrent;
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
    public double TotalGold;

    public double[] AmoutGem_A;
    public double[] AmoutGem_S;
    public double[] AmoutGem_SS;
    public double[] AmoutGem_SSS;

    public int PlayerLevel;
    public int[] CoworkerLevelArr;

    public int Stage;
    public int PlayerPos;
    public double Progress;


}