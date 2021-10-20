using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGemEffectPool : ObjectPool<GetGemEffect>
{
    public static GetGemEffectPool Instance;
#pragma warning disable 0649
    [SerializeField]
    private Transform mCanvas;
#pragma warning restore 0649

    private void Awake()
    {
        PoolSetup();
    }

    protected override GetGemEffect CreateNewObj(int id)
    {
        GetGemEffect newObj = Instantiate(mOriginArr[id], mCanvas);
        mPools[id].Add(newObj);
        return newObj;
    }

}
