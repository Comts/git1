using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPool : ObjectPool<Gem>
{
    public static GemPool Instance;
#pragma warning disable 0649
    [SerializeField]
    private Transform mCanvas;
#pragma warning restore 0649
    private void Awake()
    {
            mOriginArr = Resources.LoadAll<Gem>(Paths.GEM_PREFAB);
            PoolSetup();
    }
    protected override Gem CreateNewObj(int id)
    {
        Gem newObj = Instantiate(mOriginArr[id], mCanvas);
        mPools[id].Add(newObj);
        return newObj;
    }
}
