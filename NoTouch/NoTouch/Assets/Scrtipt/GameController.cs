using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public int LanguageType { get; set; }
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
            //TODO 게임로드
            if(Application.systemLanguage==SystemLanguage.Korean)
            {
                Debug.Log("Kor " + (int)Application.systemLanguage);
                LanguageType = 0;
            }
            else
            {
                Debug.Log("Non Kor" + (int)Application.systemLanguage);
                LanguageType = 1;
            }
        }
    }

    public void Touch()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
