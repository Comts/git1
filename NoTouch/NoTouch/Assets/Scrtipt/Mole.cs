using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mole : MonoBehaviour
{
    private Animator mAnim;
#pragma warning disable 0649
    [SerializeField]
    float mTime;
#pragma warning restore 0649
    private void OnEnable()
    {
        mAnim = GetComponent<Animator>();
        StartCoroutine(TimeOut());
    }
    private IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(mTime);
        mAnim.SetBool(AnimHash.Down,true);
    }
    public void SetactiveFalse()
    {
        gameObject.SetActive(false);
        MoleController.Instance.MoleCount--;

    }
    public void Moledaed()
    {
        mAnim.SetBool(AnimHash.Dead, true);
        MoleController.Instance.AddScore();
        Invoke("SetactiveFalse",0.5f);
    }
}
