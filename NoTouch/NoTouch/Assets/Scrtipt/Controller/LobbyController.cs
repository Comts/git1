using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class LobbyController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Button mStartButton;
    [SerializeField]
    private Text mStartText;
    [SerializeField]
    private Image BlockImage;
#pragma warning restore 0649
    [SerializeField]
    private float mAlphaAnimPeriod = 2;
    // Start is called before the first frame update
    void Start()
    {

        mStartButton.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        mStartButton.interactable = false; //버튼은 켜져있는데 비활성화 
        ActivateGameStart();
        BlockAnyInputDuringSplash();
    }
    
    private IEnumerator BlockAnyInputDuringSplash()
    {
        WaitForFixedUpdate gap = new WaitForFixedUpdate();
        while(!SplashScreen.isFinished)
        {
            yield return gap;
        }
        BlockImage.gameObject.SetActive(false);
    }

    public void ActivateGameStart()
    {
        mStartButton.interactable = true;
        StartCoroutine(AlphaAnim());
    }
    private IEnumerator AlphaAnim()
    {
        WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
        bool bAscending = true;
        float HalfTime = mAlphaAnimPeriod / 2;
        Color color = new Color(0, 0, 0, 1 / HalfTime * Time.fixedDeltaTime);
        while (true)
        {
            yield return fixedUpdate;
            if (bAscending)
            {
                mStartText.color += color;
                if (mStartText.color.a >= 1)
                {
                    bAscending = false;
                }
            }
            else
            {
                mStartText.color -= color;
                if (mStartText.color.a <= 0)
                {
                    bAscending = true;
                }
            }
        }
    }
}
