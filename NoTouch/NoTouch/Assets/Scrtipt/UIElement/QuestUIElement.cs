using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIElement : MonoBehaviour
{
    [SerializeField]
    private Button mButton;
    [SerializeField]
    private GaugeBar mGaugeBar;
    [SerializeField]
    private int mRequire;
    int mPlayerlevel;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ShowGaugeBar(double current, double max)
    {
        string progressStr = string.Format("{0} / {1}",
                                            current,
                                            max);
        float progress = (float)(current / max);
        mGaugeBar.ShowGaugeBar(progress, progressStr);
    }
}
