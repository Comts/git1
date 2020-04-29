using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<Enemy>
{
    [SerializeField]
    InGameController mIngameController;
    protected override Enemy CreateNewObj(int id)
    {
        Enemy newEnemy = Instantiate(mOriginArr[id]);
        newEnemy.SetIngameController(mIngameController);
        mPool[id].Add(newEnemy);
        return newEnemy;
    }
    //private void Awake() //objectPool도 Awake면 겹침 
    //{

    //}

}
