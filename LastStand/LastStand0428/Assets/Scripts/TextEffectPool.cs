using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectPool : ObjectPool<TextEffect>
{
    [SerializeField]
    private Transform mCanvas; //부모설정시 트랜스폼만있으면된다.
    protected override TextEffect CreateNewObj(int id)
    {
        TextEffect newEffect = Instantiate(mOriginArr[id], mCanvas);
        mPool[id].Add(newEffect);
        return newEffect;
    }
}

