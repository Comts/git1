using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolePool : ObjectPool<Mole>
{
    private void Awake()
    {
        PoolSetup();
    }
}
