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

    private void OnEnable()
    {
        mCurrentImageIndex = 0;
        mRenderer.sprite = mSprites[0];
    }
    public double SetShiftGap(int id)
    {
        mShiftGap = new double[Constants.GEM_RANK_COUNT];
        mGemCost = GameController.Instance.GetGemCost[id];
        mGemProgress = GameController.Instance.GetRequireProgerss[id];
        GapCal= mGemProgress * mGemCost * 1000 * 0.8;
        for (int i = 0; i < mShiftGap.Length; i++)
        {
            switch(i)
            {
                case 0:
                    mShiftGap[i] = GapCal * 0.2;
                    Debug.Log(i+"번째 갭"+mShiftGap[i]);
                    break;
                case 1:
                    mShiftGap[i] = GapCal * 0.5;
                    Debug.Log(i + "번째 갭" + mShiftGap[i]);
                    break;
                case 2:
                    mShiftGap[i] = GapCal * 1;
                    Debug.Log(i + "번째 갭" + mShiftGap[i]);
                    break;
                case 3:
                    mShiftGap[i] = GapCal * 2;
                    Debug.Log(i + "번째 갭" + mShiftGap[i]);
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
                int NextID = i + 1;
                mRenderer.sprite = mSprites[NextID];
                if (mCurrentImageIndex != NextID)
                {
                    mCurrentImageIndex = NextID;
                    //TODO image shift effect
                }
            }

        }
        Debug.Log(mCurrentImageIndex);
        return mCurrentImageIndex;
    }
}
