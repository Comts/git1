using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeController : InformationLoader
{
    public static PlayerUpgradeController Instance;

    private int[] mLevelArr;
    [SerializeField]
    private PlayerStat[] mInfoArr;
    [SerializeField]
    private PlayerStatText[] mTextInfoArr;

    public PlayerStat[] GetInfoArr()
    {
        return mInfoArr;
    }

    public PlayerStatText[] GetTextInfoArr()
    {
        return mTextInfoArr;
    }

    private Sprite[] mIconArr;

    private List<UIElement> mElementList;
    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;

    [SerializeField]
    private SkillButton[] mSkillbuttonArr;
    [SerializeField]
    private float[] mSkillCoolTimeArr;

    [SerializeField]
    private List<int> mSkilIndexList;

    private void Awake()
    {
        if(Instance == null)
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
        LoadJson(out mInfoArr, Paths.PLAYER_ITEM_TABLE);
        LoadJson(out mTextInfoArr, Paths.PLAYER_ITEM_TEXT_TABLE);        

        mIconArr = Resources.LoadAll<Sprite>(Paths.PLAYER_ITEM_ICON);

        //세이브데이터 불러오기
        mLevelArr = GameController.Instance.GetPlayerItemLevelArr();
        mSkilIndexList = new List<int>();


        for (int i = 0; i < mInfoArr.Length; i++)
        {

            if(mInfoArr[i].Cooltime>0) //스킬들만 자동으로 추려서 리스트등록
            {
                mSkilIndexList.Add(i);
            }

            mInfoArr[i].CurrentLevel = mLevelArr[i];
            mInfoArr[i].CostTenWeight = (Math.Pow(mInfoArr[i].CostWeight, 10) - 1) /
                                        (mInfoArr[i].CostWeight - 1);
            CalcData(i);
        }

        mElementList = new List<UIElement>();
        for(int i= 0; i < mInfoArr.Length; i++)
        {
            UIElement elem = Instantiate(mElementPrefab, mElementArea);
            string valueStr = mInfoArr[i].IsPercent ? mInfoArr[i].ValueCurrent.ToString("p0") : UnitSetter.GetUnitStr(mInfoArr[i].ValueCurrent);
            elem.Init(i, mIconArr[i],
                      mTextInfoArr[i].Title,
                      mInfoArr[i].CurrentLevel.ToString(),
                      string.Format(mTextInfoArr[i].ContentsFormat,
                                    valueStr,
                                    mInfoArr[i].Duration.ToString()),
                      UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent),
                      UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent * mInfoArr[i].CostTenWeight),
                      LevelUP);
            mElementList.Add(elem);
        }

        mSkillCoolTimeArr = GameController.Instance.GetSkillCooltimeArr();

        for(int i =0; i< mSkillbuttonArr.Length; i++)
        {
            int SkillID = mSkilIndexList[i];
            if (mInfoArr[i].CurrentLevel >0)
            {
                mSkillbuttonArr[i].SetButtonActive(true);
            }
            mSkillbuttonArr[i].ShowCooltime(mSkillCoolTimeArr[i], mInfoArr[SkillID].Cooltime);
        }

    }
    
    public void ActiveSkill(int buttonID)
    {
        int infoID = mSkilIndexList[buttonID];

        double a = mInfoArr[infoID].ValueCurrent;// 스킬 발동

        StartCoroutine(CooltimeRoutine(buttonID, mInfoArr[infoID].Cooltime, mInfoArr[infoID].Cooltime));
    }

    private IEnumerator CooltimeRoutine(int buttonID,float current,float cooltime)
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        mSkillCoolTimeArr[buttonID] = current;
        while(mSkillCoolTimeArr[buttonID] >= 0)
        {
            yield return frame;
            mSkillCoolTimeArr[buttonID] -= Time.fixedDeltaTime;
            mSkillbuttonArr[buttonID].ShowCooltime(mSkillCoolTimeArr[buttonID], cooltime);
        } 

    }

    public void LevelUP(int id, int amount)
    {
        Delegates.VoidCallback callback = () => { LevelUpCallback(id, amount); };
        switch (mInfoArr[id].CostType)
        {
            case eCostType.Gold:
                {
                    GameController.Instance.GoldCallback = callback;                    
                    double cost = mInfoArr[id].CostCurrent;
                    if (amount == 10)
                    {
                        cost *= mInfoArr[id].CostTenWeight;
                    }
                    GameController.Instance.Gold -= cost;
                }
                
                break;
            case eCostType.Ruby:
                {
                    double cost = 10 * amount;
                }
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

        mInfoArr[id].CurrentLevel += level;
        if(mInfoArr[id].CurrentLevel == mInfoArr[id].MaxLevel)
        {
            mElementList[id].SetButtonActive(false);
        }
        if(mInfoArr[id].CurrentLevel + 10 > mInfoArr[id].MaxLevel)
        {
            mElementList[id].SetTenButtonActive(false);
        }
        mLevelArr[id] = mInfoArr[id].CurrentLevel;

        CalcData(id);

        // 계산된 값 적용 UI, GameLogic
        string valueStr = mInfoArr[id].IsPercent ? mInfoArr[id].ValueCurrent.ToString("p0") : UnitSetter.GetUnitStr(mInfoArr[id].ValueCurrent);
        mElementList[id].Refresh(mInfoArr[id].CurrentLevel.ToString(),
                      string.Format(mTextInfoArr[id].ContentsFormat,
                                    valueStr,//UnitSetter.GetUnitStr(mInfoArr[id].ValueCurrent),%표시
                                    mInfoArr[id].Duration.ToString()),
                      UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent),
                      UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent *
                                    mInfoArr[id].CostTenWeight));
    }

    private void CalcData(int id)
    {
        mInfoArr[id].CostCurrent = mInfoArr[id].CostBase *
                                Math.Pow(mInfoArr[id].CostWeight, mInfoArr[id].CurrentLevel);
        if (mInfoArr[id].IsPercent)
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase +
                                mInfoArr[id].ValueWeight * mInfoArr[id].CurrentLevel;
        }
        else
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase *
                                Math.Pow(mInfoArr[id].ValueWeight, mInfoArr[id].CurrentLevel);
        }

        if(mInfoArr[id].CurrentLevel > 0)
        {
            if (mInfoArr[id].Cooltime <= 0)
            {
                switch (id)
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
                    case 3:
                    case 4:
                        int buttonID=0;
                        for(int i=0;i<mSkilIndexList.Count;i++)
                        {
                            if(mSkilIndexList[i]==id)
                            {
                                buttonID = i;
                                break;
                            }
                        }
                        mSkillbuttonArr[buttonID].SetButtonActive(true);
                        break;
                    default:
                        Debug.LogError("wrong cooltime value on player stat " + id);
                        break;
                }
            }
        }
    }

}
