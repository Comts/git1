using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class EarthController : InformationLoader
{
    public static EarthController Instance;
    [SerializeField]
    private EarthInfo[] mInfoArr;
    [SerializeField]
    private Slider mSlider;
    private double EarthLastProgress;
    private double CurrentProgress;
    private double mTouchPower;
#pragma warning disable 0649
    [SerializeField]
    private Image[] Earth_Window;
    [SerializeField]
    private GaugeBar mGaugeBar;

#pragma warning restore 0649
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
    // Start is called before the first frame update
    void Start()
    {
        LoadJson(out mInfoArr, Paths.Earth_INFO_TABLE);

    }
    public void Open_Earth_Window()
    {
        Earth_Window[GameController.Instance.Achive_Earth].gameObject.SetActive(true);
        Start_Earth_Dig();
    }
    public void Start_Earth_Dig()
    {
        CurrentProgress = 0;
        EarthLastProgress = mInfoArr[GameController.Instance.Achive_Earth].MaxProgress*1000*100*10;
        mTouchPower = GameController.Instance.ManPower;
        ShowGaugeBar(CurrentProgress, EarthLastProgress); 
        mSlider.value = 0;

    }
    public void Touch()
    {
        if (CurrentProgress >= EarthLastProgress)
        {
            GameController.Instance.Achive_Earth++;
            //TODO 지구업적

        }
        else
        {
            CurrentProgress += (mTouchPower * ItemUseController.Instance.GetGemMulti[0]);
            if (CurrentProgress > EarthLastProgress)
            {
                CurrentProgress = EarthLastProgress;
            }
        }
        ShowGaugeBar(CurrentProgress, EarthLastProgress);
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
