using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField]
    private GaugeBar mGaugeBar;
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowGaugeBar(double current,double max)
    {
        string progressStr = string.Format("{0}/{1}",
                                            UnitSetter.GetUnitStr(current),
                                            //current.ToString("n0"),
                                            UnitSetter.GetUnitStr(max));
                                            //max.ToString("n0")); //"n0" 정수형태 표준 숫자 서식 문자열
        float progress = (float)(current / max);
        //string progressStr2 = progress.ToString("P"); //float 이 0~1사이면 %표시가능
        mGaugeBar.ShowGaugeBar(progress,progressStr);
    }

}
