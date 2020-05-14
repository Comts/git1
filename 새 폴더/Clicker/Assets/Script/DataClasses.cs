using System; //PlayerStat을 인스펙터에서 보이게 하기위해
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegates
{
    public delegate void VoidCallback();
    public delegate void TwoIntInVoidCallback(int value1, int value2);
}

[Serializable] //System 불러오고 이걸 써주면 인스펙터에 나타남.
public class PlayerStat //데이터 테이블에서 불러올 항목들
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

    //쿨타임 정보
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
