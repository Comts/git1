using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public static Gem Instance;
#pragma warning disable 0649
    [SerializeField]
    private SpriteRenderer mRenderer;
    [SerializeField]
    private Sprite[] mSprites;
#pragma warning restore 0649

    private double[] mShiftGap;
    private double mGemCost;
    private float mGemProgress;
    private int mCurrentImageIndex;
    private double GapCal;

    // Start is called before the first frame update
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

    private void OnEnable()
    {
        mShiftGap = new double[4];
        mCurrentImageIndex = 0;
        mRenderer.sprite = mSprites[0];
    }
    public double SetShiftGap(int id)
    {
        mGemCost = GameController.Instance.GetGemCost[id];
        mGemProgress = GameController.Instance.GetRequireProgerss[id];
        GapCal= mGemProgress * mGemCost * 1000 * 0.8;
        for (int i = 0; i < mShiftGap.Length; i++)
        {
            switch(i)
            {
                case 0:
                    mShiftGap[i] = GapCal * 0.2;
                    break;
                case 1:
                    mShiftGap[i] = GapCal * 0.5 ;
                    break;
                case 2:
                    mShiftGap[i] = GapCal * 1;
                    break;
                case 3:
                    mShiftGap[i] = GapCal * 2;
                    break;
                default:
                    Debug.LogError("mShiftGap.Length Over " + i);
                    break;
            }
            
        }
        return mShiftGap[3];
    }

    public int SetProgress(double progress)
    {
        for (int i = mCurrentImageIndex; i < mShiftGap.Length; i++)
        {
            if (progress >= mShiftGap[i])
            {
                mRenderer.sprite = mSprites[i];
                if (mCurrentImageIndex != i)
                {
                    mCurrentImageIndex = i;
                    //TODO image shift effect
                }
            }

        }
        return mCurrentImageIndex;
    }
}
