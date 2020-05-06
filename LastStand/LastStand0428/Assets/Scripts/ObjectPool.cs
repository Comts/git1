using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T :Component
 {
    [SerializeField]
    protected T[] mOriginArr;
    protected List<T>[] mPool;
    // Start is called before the first frame update
    void Awake()
    {
        PoolSetup();
    }
    protected void PoolSetup()
    {
        mPool = new List<T>[mOriginArr.Length];
        for (int i = 0; i < mPool.Length; i++)
        {
            mPool[i] = new List<T>();
        }

    }
    public T GetFromPool(int id=0)
    {
        for( int i =0;i<mPool[id].Count;i++)
        {
            if (!mPool[id][i].gameObject.activeInHierarchy)
            {
                mPool[id][i].gameObject.SetActive(true);
                return mPool[id][i];
            }
        }
        return CreateNewObj(id);
    }
    virtual protected T CreateNewObj(int id) //virtual 오버라이딩 가능하게.
    {
        T newobj = Instantiate(mOriginArr[id]);
        mPool[id].Add(newobj);
        return newobj;
    }
}
