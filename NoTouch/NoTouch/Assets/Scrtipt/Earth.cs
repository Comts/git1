using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Earth : MonoBehaviour
{
    public static Earth Instance;
    [SerializeField]
    private Slider mSlider;
#pragma warning disable 0649
    [SerializeField]
    private Image Earth_Complete_Window;
    [SerializeField]
    private GaugeBar mGaugeBar;

#pragma warning restore 0649
    public void Complete()
    {
        Earth_Complete_Window.gameObject.SetActive(true);
    }
    public void ShowGaugeBar(double current, double max)
    {
        string progressStr = string.Format("{0} / {1}",
                                            UnitSetter.GetUnitStr(current),
                                            UnitSetter.GetUnitStr(max));
        float progress = (float)(current / max);
        mGaugeBar.ShowGaugeBar(progress, progressStr);
        mSlider.value = progress;
    }
}
