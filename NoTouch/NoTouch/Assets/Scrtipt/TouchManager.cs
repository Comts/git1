using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Camera mMainCamera;
#pragma warning disable 0649
    [SerializeField]
    private EffectPool mEffectPool;
#pragma warning restore 0649
    // Start is called before the first frame update
    void Start()
    {
        mMainCamera = Camera.main;
    }
    
    private Ray GenerateRay(Vector3 screenPos)
    {
        screenPos.z = mMainCamera.nearClipPlane;
        Vector3 origin = mMainCamera.ScreenToWorldPoint(screenPos);
        screenPos.z = mMainCamera.farClipPlane;
        Vector3 dest = mMainCamera.ScreenToWorldPoint(screenPos);

        return new Ray(origin, dest - origin);
    }

    private bool CheckTouch(out Vector3 vec)
    {
        if(Input.touchCount>0)
        {
            for(int i=0;i<Input.touchCount;i++)
            {
                Touch touch = Input.GetTouch(i);
                if(touch.phase==TouchPhase.Began)
                {
                    Ray ray = GenerateRay(touch.position);
                    RaycastHit hit;
                    if(Physics.Raycast(ray,out hit))
                    {
                        if(gameObject==hit.collider.gameObject)
                        {
                            vec = hit.point;
                            return true;
                        }
                    }
                }
            }
        }
        vec = Vector3.zero;
        return false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GenerateRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Touch"))
            {
                Timer effect = mEffectPool.GetFromPool();
                effect.transform.position = hit.point;
                GameController.Instance.Touch();
                GemSellController.Instance.RefreshGemData();
            }
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Craft"))
            {
                Timer effect = mEffectPool.GetFromPool();
                effect.transform.position = hit.point;
                CraftController.Instance.Touch();
            }
        }
        Vector3 pos;
        if (CheckTouch(out pos))
        {
            Timer effect = mEffectPool.GetFromPool();
            effect.transform.position = pos;
            GameController.Instance.Touch();
        }
    }
}
