using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Camera mMainCamera;
    [SerializeField]
    private GameObject mDummy; //터치할 때 화면에 리액션
    // Start is called before the first frame update
    void Start()
    {
        mMainCamera = Camera.main;
    }

    private Ray GenerateRay(Vector3 screenPos)
    {
        screenPos.z = mMainCamera.nearClipPlane; //스크린을 NearClip으로 잡음
        Vector3 origin = mMainCamera.ScreenToWorldPoint(screenPos);
        screenPos.z = mMainCamera.farClipPlane;
        Vector3 dest = mMainCamera.ScreenToWorldPoint(screenPos);

        return new Ray(origin, dest - origin);
    }

    private bool CheckTouch(out Vector3 vec)
    {
        if (Input.touchCount > 0) //멀티터치가 됨으로 0보다 큰 지 확인 (액티브된 것이 가장 앞번호)
        {
            for (int i = 0; i < Input.touchCount; i++) //한 손가락으로 누르고 있을때 다른 손가락으로 터치하는 경우를 위해 for문 사용
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = GenerateRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit)) //3D Raycast
                    {
                        if (gameObject == hit.collider.gameObject)
                        {
                            vec = hit.point;
                            return true; //터치되는 거 딱 하나만 검출되게 하기 위해 return (한 프레임에 3개씩 안 되게)
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
        if(Input.GetMouseButtonDown(0)) //0은 왼쪽 버튼 1은 오르쪽 2는 가운데
        {
            //Debug.Log(Input.mousePosition); //월드좌표

            //Vector3 screen = Input.mousePosition;
            //screen.z = mMainCamera.nearClipPlane; //스크린을 NearClip으로 잡음
            //Vector3 origin = mMainCamera.ScreenToWorldPoint(screen);
            //screen.z = mMainCamera.farClipPlane;
            //Vector3 dest = mMainCamera.ScreenToWorldPoint(screen);
            ////Debug.Log(origin); //스크린좌표
            ////Debug.Log(dest);

            //Ray ray = new Ray(origin, dest - origin);
            Ray ray = GenerateRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(gameObject==hit.collider.gameObject)
                {
                    GameObject gameObj = Instantiate(mDummy);
                    gameObj.transform.position = hit.point;
                    GameController.Instance.Touch(); //터치에 따른 동작 singleton
                }
                
                //Debug.Log(hit.collider.gameObject.name);
            }
            //LayerMask mask = (1 << LayerMask.NameToLayer("Building")) +
            //                 (1 << LayerMask.NameToLayer("Enemy")); //"<<"과">>"는 int의 범위에서 해당 방향으로 이동하라는 의미
            //int layerMask = 256 + 512;
            //layerMask = (1 << 8) + (1 << 9);
            //RaycastHit2D hit2D = Physics2D.Raycast(Vector2.one, Vector2.zero, 0, layerMask); //위의 2개(빌딩,적)만 검출
        }
        Vector3 pos;
        if(CheckTouch(out pos))
        {
            GameObject gameObj = Instantiate(mDummy);
            gameObj.transform.position = pos;
            GameController.Instance.Touch();
        }
    }
}
