using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Image BodyImage,HeadImage;
    [SerializeField]
    private Animator CoworkerAnim;
    [SerializeField]
    private Transform CoworkerPos;  
#pragma warning restore 0649
    private Transform Coworker;
    private Sprite[] mBodyIconArr;
    private Sprite[] mHeadIconArr;
    private Image mImage;
    private float mAlphaAnimPeriod = 2;
    // Start is called before the first frame update
    private void Awake()
    {

        mBodyIconArr = Resources.LoadAll<Sprite>(Paths.PLAYER_BODY);
        mHeadIconArr = Resources.LoadAll<Sprite>(Paths.PROFILE);
        BodyImage.sprite = mBodyIconArr[0];
        Sprite spr = CustomImage.Instance.CheckCustompath();
        if (spr != null)
        {
            mHeadIconArr[mHeadIconArr.Length - 1] = spr;
        }
        ChangePlayerHead();
    }

    public void ChangePlayerHead()
    {
        HeadImage.sprite = mHeadIconArr[GameController.Instance.PlayerProfile];
    }
    public void ChangeCustomImage(Sprite spr)
    {
        mHeadIconArr[mHeadIconArr.Length - 1] = spr;
        ChangePlayerHead();
    }

    public void ChangePlayerImage(int i)
    {

        BodyImage.sprite = mBodyIconArr[i];
    }
    public void PlayerActive(bool b)
    {
        BodyImage.gameObject.SetActive(b);
    }
    public Transform CheckPlayePos()
    {
        return BodyImage.gameObject.transform;
    }
    public void CoworkerActive(bool b)
    {
        CoworkerAnim.gameObject.SetActive(b);
    }
    private int mID;
    public void Init(int id,
                     Animator anim)
    {
        mID = id;
        CoworkerAnim = Instantiate(anim, CoworkerPos);
        CoworkerAnim.gameObject.SetActive(false);
        mImage = GetComponent<Image>();


    }
    public void HaveStage()
    {
        mImage.color += Color.black;
    }
    public Transform GetCoworkerPos()
    {
        return CoworkerPos;
    }
    public void ShowStage()
    {
        StartCoroutine(AlphaAnim());
    }
    private IEnumerator AlphaAnim()
    {
        WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
        Color color = new Color(0, 0, 0, 1 / mAlphaAnimPeriod * Time.fixedDeltaTime);
        while (mImage.color.a < 1)
        {
            yield return fixedUpdate;
            mImage.color += color;
        }
    }
}
