using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Rigidbody mRB;
    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private eItemType mType;
    private ItemController mContoller;
    private void Awake()
    {
        
        mRB = GetComponent<Rigidbody>();
        mRB.velocity = Vector3.back * mSpeed;
    }
    public void SetController(ItemController controller)
    {
        mContoller = controller;
    }
        
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            mContoller.ItemFunction(mType);
            gameObject.SetActive(false);
        }
    }
}
