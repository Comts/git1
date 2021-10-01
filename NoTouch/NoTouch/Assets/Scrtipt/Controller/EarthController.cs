using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthController : InformationLoader
{
    public static EarthController Instance;
    [SerializeField]
    private EarthInfo[] mInfoArr;
    private double EarthLastProgress;
    private double CurrentProgress;
    private double mTouchPower;
#pragma warning disable 0649
    [SerializeField]
    private Earth[] Earth_Window;

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
        CurrentProgress = GameController.Instance.EarthCurrentProgress;
        double Progress = mInfoArr[GameController.Instance.Achive_Earth].MaxProgress;
        EarthLastProgress = Progress * 1000 * 1100 * 10; // 1Km =1000m, 1m = 100cm, 1cm = 10mm 
        mTouchPower = GameController.Instance.ManPower;
        Earth_Window[GameController.Instance.Achive_Earth].ShowGaugeBar(CurrentProgress, EarthLastProgress);

    }
    public void Touch()
    {
        if (CurrentProgress >= EarthLastProgress)
        {
            Earth_Window[GameController.Instance.Achive_Earth].Complete();
            GameController.Instance.Achive_Earth++;
            GameController.Instance.EarthCurrentProgress = 0;
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
        GameController.Instance.EarthCurrentProgress = CurrentProgress;

        Earth_Window[GameController.Instance.Achive_Earth].ShowGaugeBar(CurrentProgress, EarthLastProgress);
    }
}
