using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    

    private Material mMat;

    private Coroutine mAnimRoutine;

    private bool IsDesolve;
    private void Start()
    {
        mMat = GetComponent<Renderer>().material;
        IsDesolve = false;
    }

    public void Desolve(float time)
    {
        if (mAnimRoutine == null)
        {
            if (!IsDesolve) { 
            mAnimRoutine = StartCoroutine(DesolveRoutine(time));
            }
            else
            {
                mAnimRoutine = StartCoroutine(UnDesolveRoutine(time));
            }
        }
    }
    
    private IEnumerator DesolveRoutine(float time)
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        float currenttime = 0;
        while(currenttime < time)
        {
            yield return frame;
            currenttime += Time.fixedDeltaTime;
            mMat.SetFloat("_Desolve",1-(currenttime / time));       
        }
        mAnimRoutine = null;
        IsDesolve = true;
    }
    private IEnumerator UnDesolveRoutine(float time)
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        float currenttime = 0;
        while (currenttime < time)
        {
            yield return frame;
            currenttime += Time.fixedDeltaTime;
            mMat.SetFloat("_Desolve", currenttime / time);
        }
        mAnimRoutine = null;
        IsDesolve = false;
    }
}
