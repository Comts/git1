using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.Json; //Json 파일 불러옴. InformationLader에서 불러오므로 삭제

public class PlayerUpgradeController : InformationLoader
{
    public static PlayerUpgradeController Instance;

    private int[] mLevelArr;
    [SerializeField]
    private PlayerStat[] mInfoArr, test;
    [SerializeField]
    private PlayerStatText[] mTextInfoArr;

    public PlayerStat[] GetInfoArr() //Json Generator 테스트용
    {
        return mInfoArr;
    }

    public PlayerStatText[] GetTextInfoArr() //Json Generator 테스트용
    {
        return mTextInfoArr;
    }

    private Sprite[] mIconArr;

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
        //원래 하던 방식 (>데이터 테이블로 처리)
        //mInfoArr = new PlayerStat[5];
        //mInfoArr[0] = new PlayerStat();
        //mInfoArr[0].ID = 0;
        //mInfoArr[0].CurrentLevel = 1;
        //mInfoArr[0].CostType = eCostType.Gold; ....

        LoadJson(out mInfoArr, Paths.PLAYER_ITEM_TABLE); //아래 2줄과 같은 코드
        //string data = Resources.Load<TextAsset>(Paths.PLAYER_ITEM_TABLE).text; //Json파일 불러옴.
        //mInfoArr = JsonConvert.DeserializeObject<PlayerStat[]>(data);
        //Debug.Log(data);
        //test = JsonConvert.DeserializeObject<PlayerStat[]>(data);

        LoadJson(out mTextInfoArr, Paths.PLAYER_ITEM_TEXT_TABLE); //아래 2줄과 같은 코드
        //data = Resources.Load<TextAsset>(Paths.PLAYER_ITEM_TEXT_TABLE).text; //언어팩 형식도 이런 방식으로 진행
        //mTextInfoArr = JsonConvert.DeserializeObject<PlayerStatText[]>(data);

        mIconArr = Resources.LoadAll<Sprite>(Paths.PLAYER_ITEM_ICON);

        mLevelArr = GameController.Instance.GetPlayerItemLevelArr();

        //세이브데이터 불러오기

        for(int i=0; i<mInfoArr.Length; i++)
        {
            mInfoArr[i].CurrentLevel = mLevelArr[i];
            mInfoArr[i].CostTenWeight = (Math.Pow(mInfoArr[i].CostWeight, 10) - 1) /
                                        (mInfoArr[i].CostWeight - 1);
            CalcData(i);
        }

        mElementList = new List<UIElement>();
        for(int i=0; i<mInfoArr.Length; i++)
        {
            UIElement elem = Instantiate(mElementPrefab, mElementArea);
            elem.Init(i, mIconArr[i], 
                      mTextInfoArr[i].Title, 
                      mInfoArr[i].CurrentLevel.ToString(), 
                      string.Format(mTextInfoArr[i].ContentsFormat,
                                    UnitSetter.GetUnitStr(mInfoArr[i].ValueCurrent),
                                    mInfoArr[i].Duration.ToString()),
                      UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent), 
                      UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent*mInfoArr[i].CostTenWeight),

                      LevelUP); //가능하면 데이터 순서랑 ID순서 동일하게 설정
            mElementList.Add(elem); //나중에 지우려고 만듦.
        }      
    }

    //public int[] GetSaveData() //수동 세이브 방식
    //{
    //    int[] result = new int[mInfoArr.Length];
    //    for(int i=0; i<mInfoArr.Length; i++)
    //    {
    //        result[i] = mInfoArr[i].CurrentLevel;
    //    }
    //    return result;
    //}

    //private int mSelectedID, mSelectedAmount; //LevelUP메서드에서 LevelUPCallback을 불러오기 위해 Delegates사용

    public void LevelUP(int id, int amount)
    {
        //mSelectedID = id;
        //mSelectedAmount = amount;
        Delegates.VoidCallback callback = () => { LevelUPCallback(id, amount); };

        switch (mInfoArr[id].CostType)
        {
            case eCostType.Gold:
                { //지역변수 cost를 Ruby에서도 사용하기 위함
                    GameController.Instance.GoldCallback = callback; //대입 (중첩+= X) 
                    //등비수열 합공식을 이용한 비용 계산
                    double cost = mInfoArr[id].CostCurrent;
                    if(amount==10)
                    {
                        cost *= mInfoArr[id].CostTenWeight;
                    }
                    GameController.Instance.Gold -= cost;
                    //GameController.Instance.Gold -= mInfoArr[id].CostCurrent; 위의 공식을 사용해서 계산 (X10UP도 있으므로)
                }
                break;
            case eCostType.Ruby:
                {
                    double cost = 10 * amount;
                }
                break;
            case eCostType.Soul:
                break;
            default: //잘못된 코스트타입이 입력될 때
                Debug.LogError("Wrong cost type" + mInfoArr[id].CostType);
                break;
        }
        //GameController.Instance.GoldCallback = LevelUPCallback;
        //GameController.Instance.Gold -= 2;
    }

    public void LevelUPCallback(int id, int level)
    {
        //int id = mSelectedID;
        //int level = mSelectedAmount;
        mInfoArr[id].CurrentLevel += level;
        if(mInfoArr[id].CurrentLevel==mInfoArr[id].MaxLevel)
        {
            //레벨업 잠금 UIElement에 잠금해제 기능 추가
            mElementList[id].SetButtonActive(false);

        }
        if (mInfoArr[id].CurrentLevel+10>mInfoArr[id].MaxLevel)
        {
            mElementList[id].SetTenButtonActive(false);
        }
        mLevelArr[id] = mInfoArr[id].CurrentLevel; //자동 데이터 저장

        CalcData(id);

        //계산된 값 적용 UI, GameLogic
        if(mInfoArr[id].Cooltime<=0)
        {
            switch(id)
            {
                case 0:
                    GameController.Instance.TouchPower = mInfoArr[id].ValueCurrent;
                    break;
                case 1:
                    GameController.Instance.CriticalRate = mInfoArr[id].ValueCurrent;
                    break;
                case 2:
                    GameController.Instance.CriticalValue = mInfoArr[id].ValueCurrent;
                    break;
                default:
                    Debug.LogError("Wrong cooltime value on player stat " + id);
                    break;
            }
        }
        mElementList[id].Refresh(mInfoArr[id].CurrentLevel.ToString(),
                                 string.Format(mTextInfoArr[id].ContentsFormat,
                                               UnitSetter.GetUnitStr(mInfoArr[id].ValueCurrent),
                                               mInfoArr[id].Duration.ToString()),
                                 UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent),
                                 UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent * 
                                                       mInfoArr[id].CostTenWeight));

        //GameController.Instance.TouchPower++; //바뀌는 데이터가 달라져 수정.
        //UIElement elem = Instantiate(mElementPrefab, mElementArea);
        //elem.Init(0, null, "test1", "1", "Power UP", "2", LevelUP);
        //mElementList.Add(elem);
    }

    private void CalcData(int id)
    {
        mInfoArr[id].CostCurrent = mInfoArr[id].CostBase * Math.Pow(mInfoArr[id].CostWeight, mInfoArr[id].CurrentLevel);

        if (mInfoArr[id].IsPercent)
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase + mInfoArr[id].ValueWeight * mInfoArr[id].CurrentLevel;
        }
        else
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase * Math.Pow(mInfoArr[id].ValueWeight, mInfoArr[id].CurrentLevel);
        }
    }
}
