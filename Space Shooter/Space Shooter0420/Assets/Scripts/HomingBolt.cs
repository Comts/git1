using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBolt : Bolt //볼트상속받음.
{
    [SerializeField]
    private string mTargetTag;
    private void OnEnable()
    {
        StartCoroutine(Guid());
    }
    private IEnumerator Guid()
    {
        WaitForSeconds pointThree = new WaitForSeconds(.3f);
        while(true)
        {
            GameObject obj = GameObject.FindGameObjectWithTag(mTargetTag);
            if (obj != null)
            {
                Vector3 pos = obj.transform.position;
                Vector3 dir = pos - transform.position;
                dir = dir.normalized;
                mRB.velocity = dir * mSpeed;
                mRB.transform.LookAt(pos);//목적지 방향으로 방향변경
            }
            yield return pointThree;
        }
    }
}
