using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<Enemy>
{
    protected override Enemy CreateNewObj(int id)
    {
        Enemy newObj = Instantiate(mOriginArr[id]);
        mPool[id].Add(newObj);
        return newObj;
    }
    //private void Awake() //objectPool도 Awake면 겹침 
    //{

    //}

}
