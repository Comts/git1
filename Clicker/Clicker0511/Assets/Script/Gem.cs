using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer mRenderer;
    [SerializeField]
    private Sprite[] mSprites;

    private float mShiftGap;
    private int mCurrentImageIndex;
    // Start is called before the first frame update
    void Awake()
    {
        mShiftGap = 1f / mSprites.Length;
    }

    private void OnEnable()
    {
        mCurrentImageIndex = 0;
        mRenderer.sprite = mSprites[0];   
    }

    public void SetProgress(float progress)
    {
        //이미지 설정
        int index = (int)(progress / mShiftGap);
        if(index < mSprites.Length)
        {
            mRenderer.sprite = mSprites[index];
            //이미지 교체순간 이펙트
            if (mCurrentImageIndex != index)
            {
                mCurrentImageIndex = index;
                //TODO image shift effect
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
