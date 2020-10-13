using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController : MonoBehaviour
{
    public static GameDataController Instance;
    [SerializeField]
    private int mAtk, mMaxHP;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//씬이 바뀌어도 메모리를 지우지않음 //전체 게임에 데이터 or 기능 동작, 저장, 관리 하위도 같이 적용
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddAtk(int value)
    {
        mAtk += value;
    }
}
