using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private Button[] mButtonArr;
    [SerializeField]
    private UIElement[] mElementArr;
    [SerializeField]
    private TextMeshProUGUI mText;

    private Delegates.IntInvoidReturn call;
    private Delegates.VoidCallback mCallback;


    [SerializeField]
    private GameController mController;
    // Start is called before the first frame update
    void Start()
    {
        mText.text = ("aaa");
        call = (int a) => { Debug.Log(a); };
        call(1);
        call = LevelUp;
        call(1);
        //for(int i= 0; i<mButtonArr.Length;i++)
        //{
        //   int j = i;
        //    mButtonArr[i].onClick.AddListener(() => { LevelUp(j); });// 람다식 i반복문 문제점/LevelUp(i) 임시값이 들어가므로 나중에 덮어쓰기됨 지역변수쓰면됨.
        //}
        mElementArr[0].Init(0, "공격증가", "공격력이 1 증가합니다", 0, 10, LevelUp);
        mElementArr[1].Init(1, "방어증가", "방어력이 0.1 증가합니다", 0, 15, LevelUp);
        mElementArr[2].Init(2, "체력증가", "체력이 1 증가합니다", 0, 20, LevelUp);

        //for(int i=0;i<mElementArr.Length;i++)
        //{
        //    mElementArr[i].SetButtonOnClick(this);
        //}
    }
    public void LevelUp(int id)
    {
        //id 번째 뎅터 레벨 1업
        Debug.Log("level up: " + id);
    }

    public void GoldSpend1()
    {
        mController.UseGold(1);
    }
    public void GoldSpend2()
    {
        mController.UseGold(150,()=> { Debug.Log("Use 150 gold"); });
    }
    public void GoldSpend3()
    {
        mController.UseGold(10, () => { Debug.Log("Spend 10 gold"); });
    }
    public void ButtonCall() 
    {
        if(mCallback!=null)
        {
            mCallback();
        }
    }
    public void ButtonCallAdd()
    {
        mCallback += () =>
                        {
                            Debug.Log("Test!!");//매서드 여러개 중첩가능
                        };
    }

    bool IsClicked;
    public bool OpenPopup()
    {
        IsClicked = false;
        while (!IsClicked)
        {
             //멈춰있어 뻗음.
        }
        //StartCoroutine(routine(IsClicked));//코루틴사용하면 return바로됨
        return true;
    }
    private IEnumerator routine(bool IsClicked) 
    {
        while (!IsClicked)
        {
            yield return new WaitForFixedUpdate();
        }
    }
    Delegates.VoidCallback mdelegates;
    public void OpenPopup(Delegates.VoidCallback callback)
    {
        mdelegates = callback;
    }
    public void PopupButtonClicked()
    {
        mdelegates();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(OpenPopup())
            {
                Debug.Log(3);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            OpenPopup(() => { Debug.Log(3); });
        }
    }

}
