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
    private Image BlockImage,BackGroundImage, TalkImage;
    [SerializeField]
    private Image[] PrologueImage;
    [SerializeField]
    private Button[] PrologueButton;
    [SerializeField]
    private Text PrologueText,NextText;
    [SerializeField]
    private Sprite TalkImage_Sad, TalkImage_Smile;
#pragma warning restore 0649
    private string mText;
    private Coroutine AnimCor;
    private bool IsTyping;
    private bool SkipTyping;
    private bool PrologueChanging;
    private int PrologueCheck;
    private int PrologueStep;
    private List<string> Dialogue;
    [SerializeField]
    private float mAlphaAnimPeriod = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlockAnyInputDuringSplash());
        mStartButton.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        mStartButton.interactable = false; //버튼은 켜져있는데 비활성화 
        Dialogue = new List<string>();
        PrologueStep = -1;
        PrologueCheck = PlayerPrefs.GetInt("Prologue",0);
        if (PrologueCheck == 0)
        {
            SettingDialogue();
            PrologueStep = 0;
            PrologueChanging = false;
            IsTyping = false;
            BackGroundImage.gameObject.SetActive(false);
            //PrologueImage[0].gameObject.SetActive(true);
            StartCoroutine(ShowPrologue());
        }
        else
        {
            ActivateGameStart();
        }
    }
    private IEnumerator ShowPrologue()
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        while (PrologueCheck == 0)
        {
            if (!IsTyping)
            {
                if (!PrologueChanging)
                {
                    switch (PrologueStep)
                    {
                        case 0:
                            TalkImage.sprite = TalkImage_Sad;
                            ShowPrologueImgae(0);
                            mText = Dialogue[0];
                            StartCoroutine(Typing());
                            break;
                        case 1:
                            mText = Dialogue[1];
                            StartCoroutine(Typing());
                            break;
                        case 2:
                            mText = Dialogue[2];
                            StartCoroutine(Typing());
                            break;
                        case 3:
                            TalkImage.sprite = TalkImage_Smile;
                            mText = Dialogue[3];
                            StartCoroutine(ChangePrologueImage(0));
                            break;
                        case 4:
                            mText = Dialogue[4];
                            StartCoroutine(ChangePrologueImage(1));
                            break;
                        case 5:
                            mText = Dialogue[5];
                            StartCoroutine(ChangePrologueImage(2));
                            break;
                        case 6:
                            mText = Dialogue[6];
                            StartCoroutine(ChangePrologueImage(3));
                            break;
                        default:
                            PrologueStep = -1;
                            PrologueCheck = 1; 
                            EndPrologue();
                            BackGroundImage.gameObject.SetActive(true); 
                            PlayerPrefs.SetInt("Prologue", 1);
                            ActivateGameStart();
                            break;
                    }
                    PrologueChanging = true;
                }

            }
            yield return frame;
        }
    }
    public void CheckPrologue()
    {

        if (!IsTyping)
        {
            if (PrologueChanging)
            {

                PrologueStep++;
                PrologueChanging = false;
            }
        }
        else
        {
            SkipTyping = true;

        }
    }
    public void EndPrologue()
    {
        PrologueText.gameObject.SetActive(false);
        TalkImage.gameObject.SetActive(false);
        NextText.gameObject.SetActive(false);
        for (int i = 0; i < PrologueImage.Length; i++)
        {
            PrologueImage[i].gameObject.SetActive(false);
        }

    }
    public void RestartPrologue()
    {
        PlayerPrefs.SetInt("Prologue", 0);
        if (Dialogue.Count == 0)
        {
            SettingDialogue();
        }
        PrologueStep = 0;
        PrologueChanging = false;
        IsTyping = false;
        BackGroundImage.gameObject.SetActive(false);
        PrologueCheck = 0;
        for (int i = 0; i < PrologueButton.Length; i++)
        {
            PrologueButton[i].interactable = false;
        }
        if (AnimCor != null)
        {
            StopCoroutine(AnimCor);
        }
        StartCoroutine(ShowPrologue());
    }
    public void ShowPrologueImgae(int num)
    {
        PrologueImage[num].gameObject.SetActive(true);
        NextText.gameObject.SetActive(false);
        for (int i =0;i< PrologueImage.Length; i++)
        {
            if (i == num)
            {
                continue;
            }
            PrologueImage[i].gameObject.SetActive(false);
        }
        PrologueButton[num].interactable = true;
    }
    public IEnumerator ChangePrologueImage(int i)
    {
        WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
        bool bChange = true;
        Color color = new Color(0, 0, 0, 1 / 0.5f * Time.fixedDeltaTime);
        if (i+1< PrologueImage.Length)
        {
            PrologueImage[i + 1].color = new Color(1, 1, 1, 0);
            PrologueImage[i + 1].gameObject.SetActive(true);
            PrologueText.gameObject.SetActive(false);
            NextText.gameObject.SetActive(false);
            TalkImage.gameObject.SetActive(false);

            while (PrologueImage[i + 1].color.a <= 1)
            {
                yield return fixedUpdate;
                if (bChange)
                {
                    PrologueImage[i].color -= color;
                    if (PrologueImage[i].color.a <= 0)
                    {
                        bChange = false;
                    }
                }
                else
                {
                    PrologueImage[i + 1].color += color;

                }
            }

            PrologueImage[i].color = Color.white;
            ShowPrologueImgae(i + 1);

            StartCoroutine(Typing());

        }
    }
    public void SettingDialogue()
    {
        Dialogue.Add("올해에는 꼭\n취업해야지\n공고 올라온 거\n있나 볼까? ");
        Dialogue.Add("아니 다 경력직만\n뽑으면 나같은\n신입은 어디서\n경력쌓나...");
        Dialogue.Add("취업하기 참\n어렵네...\n바람 좀 쐬고\n와야겠다.");
        Dialogue.Add("산책하니까\n기분이 좋네!!\n이쪽으로 한번\n가볼까?");
        Dialogue.Add("머야 이 광산은??\n이런 곳도 있었어??");
        Dialogue.Add("시간도 많은데\n한번 들어가 볼까?");
        Dialogue.Add("노다지다 노다지!!");
    }

    private IEnumerator Typing()
    {
        TalkImage.gameObject.SetActive(true);
        PrologueText.gameObject.SetActive(true);
        NextText.gameObject.SetActive(false);
        IsTyping = true;
        for(int i = 0; i <= mText.Length; i++)
        {
            PrologueText.text = mText.Substring(0, i);
            if (SkipTyping)
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        PrologueText.text = mText;
        NextText.gameObject.SetActive(true);
        SkipTyping = false;
        IsTyping = false;
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
        AnimCor=StartCoroutine(AlphaAnim());
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
