using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    [SerializeField]
    private EnemyPool mEnemyPool;
    [SerializeField]
    private Transform mLeftPos, mRightPos;
    private float mCoin;
    [SerializeField]
    private InGameUIController mUIController;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    public void AddCoin(float value)
    {
        mCoin += value;
        mUIController.ShowCoin(mCoin);
    }

    private  IEnumerator SpawnEnemy()
    {
        WaitForSeconds Three = new WaitForSeconds(3);
        while(true)
        {
            yield return Three;
            bool IsLeft = Random.value < .5f;
            bool IsMale = Random.value < .5f;
            Transform spawnPos;
            if (IsLeft) 
            {
                spawnPos = mLeftPos;
            }
            else
            {
                spawnPos = mRightPos;

            }
            Enemy enemy;
            if(IsMale)
            {
                enemy = mEnemyPool.GetFromPool(0);
            }
            else
            {
                enemy = mEnemyPool.GetFromPool(1);
            }
            enemy.transform.position = spawnPos.position;
            enemy.transform.rotation = spawnPos.rotation;
            enemy.StartMoving();

        }
    }
}
