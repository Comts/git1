using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitSetter
{
    private static readonly string[] UnitArr = { "", "만", "억", "조", "경", "해", "자", "양", "구", "간", "정", "재", "극", "항", "아", "나", "불", "무", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };
    public static string GetUnitStr(double value)
    {
        double baseGold = value;
        int calcount = 0;
        if (baseGold >= 10000)
        {
            int FrontGold = 1;
            int BackGold = 0;
            while (baseGold >= 10000)
            {
                BackGold = (int)(baseGold % 10000);
                baseGold = baseGold / 10000;
                FrontGold = (int)baseGold;
                calcount++;
            }
            BackGold /= 100;
            return string.Format("{0}.{1} {2}",
                           FrontGold,
                           BackGold, 
                           UnitArr[calcount]);
        }
        else
        {
            return string.Format("{0}", value);
        }
    }
}
