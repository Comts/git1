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
        double Add = Mathf.Pow(10, GameController.Instance.Achive_Earth);
        EarthLastProgress = Progress * 1000 * 100 * 10 * Add; // 1Km =1000m, 1m = 100cm, 1cm =10mm  // 0:1 1:10 2:100 3:1000 4:10000
        mTouchPower = GameController.Instance.ManPower;
        Earth_Window[GameController.Instance.Achive_Earth].ShowGaugeBar(CurrentProgress, EarthLastProgress);

    }
    public void Touch()
    {
        if (CurrentProgress >= EarthLastProgress)
        {
            GameController.Instance.EarthCurrentProgress = 0;
            Earth_Window[GameController.Instance.Achive_Earth].Complete();
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
            GameController.Instance.EarthCurrentProgress = CurrentProgress;

            Earth_Window[GameController.Instance.Achive_Earth].ShowGaugeBar(CurrentProgress, EarthLastProgress);
        }
    }
}
