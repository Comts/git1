using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json; //json.dll이있어야 나옴.
//using UnityEngine.UI;

public class PlayerUpgradeController : MonoBehaviour
{
    public static PlayerUpgradeController Instance;

    [SerializeField]
    private PlayerStat[] mInfoArr,test;
    //[SerializeField]
    //private Button mUpgrade;
    private List<UIElement> mElementList;
    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;


    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //이런방법도있는데 SerializeField로 한다.
        //mInfoArr = new PlayerStart[5];
        //mInfoArr[0] = new PlayerStart();
        //mInfoArr[0].ID = 

        string data = JsonConvert.SerializeObject(mInfoArr); //데이터테이블 파일화
        Debug.Log(data);
        test = JsonConvert.DeserializeObject<PlayerStat[]>(data); //파일화된걸 불러오는거 연습 string data를 PlayerStat형식인test에 꽂기
        
        //세이브데이터 불러오기
        mElementList = new List<UIElement>();
        
        for (int i = 0; i < mInfoArr.Length; i++)
        {
            UIElement elem = Instantiate(mElementPrefab, mElementArea);
            elem.Init(i , null, "test1", "1", "power up", "2", LevelUP);
            mElementList.Add(elem); //나중에 지울때 필요
        }
        //mUpgrade.onClick.AddListener(LevelUP);
        //mUpgrade.onClick.Invoke(); //버튼을 클릭하지않더라도 기능 동작 가능
    }

    private int mSelectedID, mSelectedAmount; //필드변수 사용하여 처리하는방법

    public void LevelUP(int id, int amount)
    {
        //mSelectedID = id;//필드변수 사용하여 처리하는방법
        //mSelectedAmount = amount;//필드변수 사용하여 처리하는방법
        Delegates.VoidCallback callback = () => { LevelUpCallback(id, amount); };//필드변수x 람다식
        switch (mInfoArr[id].CostType)
        {
            case eCostType.Gold:
                GameController.Instance.GoldCallback = callback; //골드가 충분한지 확인후 진행. 어려우면 USEGOLD ex)textrpg
                GameController.Instance.Gold -= mInfoArr[id].CostCurrent;
                break;
            case eCostType.Rubby:
                break;
            case eCostType.Soul:
                break;
            default:
                Debug.LogError("wrong cost type " + mInfoArr[id].CostType);
                break;
        }
    }

    public void LevelUpCallback(int id, int level)
    {
        //int id = mSelectedID;//필드변수 사용하여 처리하는방법
        //int level = mSelectedAmount;//필드변수 사용하여 처리하는방법
        mInfoArr[id].CurrentLevel += level;
        if(mInfoArr[id].CurrentLevel == mInfoArr[id].MaxLevel)
        {
            //레벨업잠금
        }
        mInfoArr[id].CostCurrent = mInfoArr[id].CostBase 
                                    * Math.Pow(mInfoArr[id].CostWeight, mInfoArr[id].CurrentLevel);
        if(mInfoArr[id].IsPercent)
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase + mInfoArr[id].ValueWeight * mInfoArr[id].CurrentLevel;
        }
        else
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase * Math.Pow( mInfoArr[id].ValueWeight , mInfoArr[id].CurrentLevel);
        }

        //계산된 값 적용 UI, GameLogic

        //GameController.Instance.TouchPower++;
        //UIElement elem = Instantiate(mElementPrefab, mElementArea);
        //elem.Init(0, null, "test1", "1", "power up", "2", LevelUP);
        //mElementList.Add(elem);
    }
}
