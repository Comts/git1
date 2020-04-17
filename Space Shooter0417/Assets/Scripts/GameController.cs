using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int mScore;
    private bool mbGameOver;
    [SerializeField]
    private Player mPlayer;
    [SerializeField]
    private UIController mUIController;
    [Header("EnemySpawn")]
    [SerializeField]
    private AsteroidPool mAstPool;
    [SerializeField]
    private float mSpawnXMin, mSpawnXMax, mSpawnZ;
    [SerializeField]
    private float mSpawnRate;
    private float mCurrentSpawnRate;
    [SerializeField]
    private EnemyPool mEemyPool;
    private Coroutine mHazardRoutine;

    // Start is called before the first frame update
    void Start()
    {
        mUIController.ShowScore(mScore);
        mUIController.ShowMessagetext("");
        mUIController.ShowRestart(false);
        mHazardRoutine = StartCoroutine(SpawnHazard());
    }
    public void AddScore(int amount)
    {
        mScore += amount;
        //UI
        mUIController.ShowScore(mScore);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        //mbGameOver = false;
        //mScore = 0;
        //mPlayer.transform.position = Vector3.zero;
        //mPlayer.gameObject.SetActive(true);
        //if (mHazardRoutine==null) //꺼진거 확인후 재시작
        //{
        //    mHazardRoutine = StartCoroutine(SpawnHazard());
        //}
        ////UI
        //mUIController.ShowScore(mScore);
        //mUIController.ShowMessagetext("");
        //mUIController.ShowRestart(false);
    }
    public void PlayerDie()
    {
        //=====완전히 게임 오버 일 때=====
        //스폰 멈추기
        //StopCoroutine(SpawnHazard());   //새로운걸 생성한뒤 끄는 상황.
        //                                //문자열로 끄면 다른것들도 다꺼짐.
        StopCoroutine(mHazardRoutine);  //mHazardRoutine 사용하여 원하는것하나끄기.
        mHazardRoutine = null;          //꺼졌는지확인용.
        //리셋기능 활성화
        mbGameOver = true;

        //최종스코어 표시(게임오버 표시 밑에)
        mUIController.ShowMessagetext("Game Over");
        mUIController.ShowRestart(true);

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
        //new WaitForSeconds(5);
        if (mbGameOver && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }
}
