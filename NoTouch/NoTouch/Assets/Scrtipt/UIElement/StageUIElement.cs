using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIElement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Animator PlayerAnim;
    [SerializeField]
    private Animator CoworkerAnim;
    [SerializeField]
    private Transform CoworkerPos;
#pragma warning restore 0649
    private Transform Coworker;
    private Image mImage;
    private float mAlphaAnimPeriod = 2;
    // Start is called before the first frame update
    public void PlayerActive(bool b)
    {
        PlayerAnim.gameObject.SetActive(b);
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
