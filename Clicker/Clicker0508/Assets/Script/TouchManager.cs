using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Camera mMainCamera;
    [SerializeField]
    private GameObject mDummy;
    // Start is called before the first frame update
    void Start()
    {
        mMainCamera = Camera.main;   //Main Camera 태그 카메라를 가져온다
    }

    private Ray GenerateRay(Vector3 screenPos)
    {
        screenPos.z = mMainCamera.nearClipPlane;
        Vector3 origin = mMainCamera.ScreenToWorldPoint(screenPos);
        screenPos.z = mMainCamera.farClipPlane;
        Vector3 dest = mMainCamera.ScreenToWorldPoint(screenPos);
        //Debug.Log(origin);
        //Debug.Log(dest);

        return new Ray(origin, dest - origin);

    }
    private bool bCheckTouch(out Vector3 vec)
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)//TouchPhase/began moved end 3개만사용
                {
                    Ray ray = GenerateRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (gameObject == hit.collider.gameObject)
                        {
                            //Debug.Log(hit.point);
                            //TODO GameController Touch
                            vec = hit.point;
                            return true; //한프레임에 Began하나만 검출하기위해서 리턴
                        }

                    }
                }
            }
        }
        vec = Vector3.zero;
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //0,1,2 왼쪽 오른쪽 휠클릭
        {
            ////Debug.Log(Input.mousePosition);
            //Vector3 screen = Input.mousePosition;
            //screen.z = mMainCamera.nearClipPlane;
            //Vector3 origin = mMainCamera.ScreenToWorldPoint(screen);
            //screen.z = mMainCamera.farClipPlane;
            //Vector3 dest = mMainCamera.ScreenToWorldPoint(screen);
            ////Debug.Log(origin);
            ////Debug.Log(dest);

            //Ray ray = new Ray(origin, dest - origin);
            Ray ray = GenerateRay(Input.mousePosition);//위와동일
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(gameObject == hit.collider.gameObject)
                {
                    GameObject gameObj = Instantiate(mDummy);
                    gameObj.transform.position = hit.point;
                    //Debug.Log(hit.point);
                    // GameController Touch
                    GameController.Instance.Touch();
                }
                //Debug.Log(hit.collider.gameObject.name);
            }
            LayerMask mask = (1 << LayerMask.NameToLayer("Buliding")) + 
                             (1 << LayerMask.NameToLayer("Enemy"));
            RaycastHit2D hit2D = Physics2D.Raycast(Vector2.one, Vector2.zero, 0, mask); //원하는 레이어의 해당하는애들만 검출. 2D는 Z축이 없기에 방향벡터 zero, distance 0
        }

        Vector3 pos;
        if(bCheckTouch(out pos))
        {
            // GameController Touch
            GameObject gameObj = Instantiate(mDummy);
            gameObj.transform.position = pos;
            GameController.Instance.Touch();
            //set Effect Pos
        }

    }
}
