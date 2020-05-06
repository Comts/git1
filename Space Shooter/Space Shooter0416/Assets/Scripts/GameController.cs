using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int mScore;
    [SerializeField]
    private AsteroidPool mAstPool;
    [SerializeField]
    private float mSpawnXMin, mSpawnXMax, mSpawnZ;
    [SerializeField]
    private float mSpawnRate;
    private float mCurrentSpawnRate;
    [SerializeField]
    private EnemyPool mEemyPool;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnHazard());
    }
    public void AddScore(int amount)
    {
        mScore += amount;
        //UI
    }
    

    private IEnumerator SpawnHazard()
    {
        WaitForSeconds PiointThree = new WaitForSeconds(0.3f);
        WaitForSeconds spawnRate = new WaitForSeconds(mSpawnRate);
        int enemyCount = 3;
        int astCount = 5;
        int CurrentEnemyCount;
        int CurrentAstCount;
        float ratio = 1f / 3;
        while (true)
        {
            CurrentAstCount = astCount;
            CurrentEnemyCount = enemyCount;
            while (CurrentAstCount > 0 && CurrentEnemyCount > 0)
            {
                float rand = Random.Range(0, 1f);
               // float rand = Random.value(0, 1f); 
               if(rand< ratio)
                {

                    Enemy enemy = mEemyPool.GetFromPool();
                    enemy.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                         0,
                                                         mSpawnZ);
                    CurrentEnemyCount--;
                    yield return PiointThree;
                }
               else
                {

                    AsteroidMovement ast = mAstPool.GetFromPool(Random.Range(0, 3));
                    ast.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                         0,
                                                         mSpawnZ);
                    CurrentAstCount--;
                    yield return PiointThree;
                }
            }
            for (int i = 0; i < CurrentAstCount; i++)
            {
                AsteroidMovement ast = mAstPool.GetFromPool(Random.Range(0, 3));
                ast.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                     0,
                                                     mSpawnZ);
                yield return PiointThree;
            }
            for(int i= 0; i< CurrentEnemyCount; i++)
            {
                Enemy enemy = mEemyPool.GetFromPool();
                enemy.transform.position = new Vector3(Random.Range(mSpawnXMin, mSpawnXMax),
                                                     0,
                                                     mSpawnZ);
                yield return PiointThree;
            }
            yield return spawnRate;
        }
    }


    private void Update()
    {
        new WaitForSeconds(5);
    }
}
