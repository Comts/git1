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
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowGaugeBar(double current, double max)
    {
        string progressStr = string.Format("{0}/{1}",
                                            //current.ToString("N0"),
                                            UnitSetter.GetUnitStr(current),
                                            UnitSetter.GetUnitStr(max));
                                            //max.ToString("C0")); //"N0"는 소수점 자리가 안 보이게 함. N1~2는 소수점까지 보여줌.
        float progress = (float)(current / max);
        //string progressStr = progress.ToString("P");
        mGaugeBar.ShowGaugeBar(progress, progressStr);
    }
}
