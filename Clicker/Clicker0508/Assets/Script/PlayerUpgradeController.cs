using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class PlayerUpgradeController : MonoBehaviour
{
    //[SerializeField]
    //private Button mUpgrade;
    private List<UIElement> mElementList;
    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;

    // Start is called before the first frame update
    void Start()
    {
        //세이브데이터 불러오기
        mElementList = new List<UIElement>();
        UIElement elem = Instantiate(mElementPrefab, mElementArea);
        elem.Init(0, null, "test1", "1", "power up", "2", LevelUP);
        mElementList.Add(elem); //나중에 지울때 필요

        //mUpgrade.onClick.AddListener(LevelUP);
        //mUpgrade.onClick.Invoke(); //버튼을 클릭하지않더라도 기능 동작 가능
    }

    public void LevelUP(int id, int amount)
    {
        GameController.Instance.GoldCallback = LevelUpCallback; //골드가 충분한지 확인후 진행. 어려우면 USEGOLD ex)textrpg
        GameController.Instance.Gold -= 2;
    }

    public void LevelUpCallback()
    {
        GameController.Instance.TouchPower++;
        UIElement elem = Instantiate(mElementPrefab, mElementArea);
        elem.Init(0, null, "test1", "1", "power up", "2", LevelUP);
        mElementList.Add(elem);
    }
}
