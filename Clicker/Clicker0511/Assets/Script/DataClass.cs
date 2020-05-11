using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegates
{
    public delegate void VoidCallback();
    public delegate void TwoIntInVoidCallBack(int value1,int value2);
}
[Serializable] 
public class PlayerStat //저장할데이터 크면 안좋다
{
    public int ID;
    public int CurrentLevel;
    public int MaxLevel;

    public eCostType CostType;
    public double CostBase;
    public double CostWeight;
    public double CostCurrent;

    public bool IsPercent;
    public double ValueBase;
    public double ValueWeight;
    public double ValueCurrent;

    public float Cooltime;
    //public float CooltimeCurrent; // 데이터테이블에서 로드
    public float Duration;
    //public float DurationCurrent;

}