using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public static TouchManager Instance;
    private Camera mMainCamera;
#pragma warning disable 0649
    [SerializeField]
    private EffectPool mEffectPool;
#pragma warning restore 0649
    [SerializeField]
    private int mChangeCount;
    private int mTouchCount;
    private int mTouchImage;
    private int mCurrentImage;
    public int TouchImage
    {
        get { return mCurrentImage; }
        set
        {
            mCurrentImage = value;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        mMainCamera = Camera.main;
        mTouchCount = 0;
        mTouchImage = 1;
        mCurrentImage = 0;
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
            mTouchCount++;
            for (int i=0;i<Input.touchCount;i++)
            {
                Touch touch = Input.GetTouch(i);
                if(touch.phase==TouchPhase.Began)
                {
                    Ray ray = GenerateRay(touch.position);
                    RaycastHit hit;
                    if(Physics.Raycast(ray,out hit))
                    {
                        //if(gameObject==hit.collider.gameObject)
                        // {
                        if (hit.collider.gameObject.CompareTag("Touch"))
                        {
                            mTouchCount++;
                            GameController.Instance.Touch();
                            SoundController.Instance.FXSound(UnityEngine.Random.Range(0, 2));

                            if(mTouchCount>= mChangeCount)
                            {
                                StageController.Instance.ChangePlayerImage(mTouchImage);
                                mCurrentImage = mTouchImage;
                                mTouchImage++;
                                if (mTouchImage > 3)
                                {
                                    mTouchImage = 0;
                                }
                                mTouchCount = 0;
                            }

                        }
                        if (hit.collider.gameObject.CompareTag("Craft"))
                        {
                            CraftController.Instance.Touch();
                            SoundController.Instance.FXSound(UnityEngine.Random.Range(5, 7));
                        }
                        if (hit.collider.gameObject.CompareTag("Mole"))
                        {
                            hit.collider.gameObject.GetComponent<Mole>().Moledaed();
                            SoundController.Instance.FXSound(UnityEngine.Random.Range(2, 5));
                        }
                        if (hit.collider.gameObject.CompareTag("Earth"))
                        {
                            EarthController.Instance.Touch();
                            SoundController.Instance.FXSound(7);
                        }
                        vec = hit.point;
                        return true;
                        // }
                    }
                }
            }
        }
        vec = Vector3.zero;
        return false;
    }

    private int Random(int v1, int v2)
    {
        throw new NotImplementedException();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GenerateRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Touch"))
                {
                    mTouchCount++;
                    GameController.Instance.Touch();
                    SoundController.Instance.FXSound(UnityEngine.Random.Range(0, 2));
                    if (mTouchCount >= mChangeCount)
                    {
                        StageController.Instance.ChangePlayerImage(mTouchImage);
                        mCurrentImage = mTouchImage;
                        mTouchImage++;
                        if (mTouchImage > 3)
                        {
                            mTouchImage = 0;
                        }
                        mTouchCount = 0;
                    }
                }
                if (hit.collider.gameObject.CompareTag("Craft"))
                {
                    CraftController.Instance.Touch();
                    SoundController.Instance.FXSound(UnityEngine.Random.Range(5, 7));
                }
                if (hit.collider.gameObject.CompareTag("Mole"))
                {
                    hit.collider.gameObject.GetComponent<Mole>().Moledaed();
                    SoundController.Instance.FXSound(UnityEngine.Random.Range(2, 5));
                }
                if (hit.collider.gameObject.CompareTag("Earth"))
                {
                    EarthController.Instance.Touch();
                    SoundController.Instance.FXSound(7);
                }
            }
            Timer effect = mEffectPool.GetFromPool();
            effect.transform.position = hit.point + (Vector3.back *3);
        }
        Vector3 pos;
        if (CheckTouch(out pos))
        {
            Timer effect = mEffectPool.GetFromPool();
            effect.transform.position = pos + (Vector3.back * 3);
        }
    }
}
