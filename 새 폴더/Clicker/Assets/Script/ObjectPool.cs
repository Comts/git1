using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ObjectPool<T> : MonoBehaviour where T : Component //제너릭화 시킴 (Component의 하위(where는 하위로 만들어줌)여야지 가능)
{
    [SerializeField]
    protected T[] mOriginArr;
    [SerializeField]
    protected List<T>[] mPool;

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
        for (int i = 0; i < mPool[id].Count; i++)
        {
            if (!mPool[id][i].gameObject.activeInHierarchy)
            {
                mPool[id][i].gameObject.SetActive(true);
                return mPool[id][i];
            }
        }
        return CreateNewObj(id);
        //이 부분도 메서드로 아래와 같이 따로 빼주기 
        //T newObj = Instantiate(mOriginArr[id]);
        //mPool[id].Add(newObj);
        //return newObj;
    }

    virtual protected T CreateNewObj(int id) //명확하게 하기 위해 virtual 사용
    {
        T newObj = Instantiate(mOriginArr[id]);
        mPool[id].Add(newObj);
        return newObj;
    }
}
