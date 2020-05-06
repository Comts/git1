using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{

    private float mDamage;
    public void SetDamage(float value)
    {
        mDamage = value;
    }
    //List<GameObject> EnteredArr;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            //1명때릴때
            //if(!EnteredArr.Contains(other.gameObject))
            //
            //{
            //    EnteredArr.Add(other.gameObject);
            //}
            //EnteredArr[0].SendMessage("Hit", mDamage);
            //if(!EnteredArr[0].activeInHierarchy)
            //{
            //    EnteredArr.RemoveAt(0);
            //}
            other.gameObject.GetComponent<Enemy>().Hit(mDamage);
            //other.gameObject.SendMessage("Hit", mDamage);
            //mPlayer.GetDmg(); 데미지가져와서 공격
        }
    }
}
