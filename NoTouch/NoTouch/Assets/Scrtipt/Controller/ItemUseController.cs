using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseController : MonoBehaviour
{
    public static ItemUseController Instance;
    [SerializeField]
    private float[] mItemCooltimeArr, mItemMaxCooltimeArr;
    [SerializeField]
    private List<int> mItemIndexList;
    public double[] GetGemMulti { get; set; }
    public double SellGemMulti { get; set; }
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
        for(int i=0;i< GetGemMulti.Length;i++)
        {
            GetGemMulti[i] = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator GetGemMultiRoutine(int id,float duration, double value) //0 player 1 coworker
    {
        GetGemMulti[id] = value;
        yield return new WaitForSeconds(duration);
        GetGemMulti[id] = 1;
    }
    private IEnumerator SellGemMultiRoutine(float duration, double value)
    {
        SellGemMulti = value;
        yield return new WaitForSeconds(duration);
        SellGemMulti = 1;
    }
}
