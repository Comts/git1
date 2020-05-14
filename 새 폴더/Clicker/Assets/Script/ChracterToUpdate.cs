using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChracterToUpdate : MonoBehaviour //상용한글을 유니코드화 시킴
{
    [SerializeField]
    private string mOrigin;

    void Start()
    {
        char[] charArr = mOrigin.ToCharArray();
        for (int i = 0; i < charArr.Length; i++)
        {
            int charToInt = (int)charArr[i];
            Debug.LogFormat("{0}: {1} //{2}", charArr[i], charToInt, charToInt.ToString("X4"));
        }
    }
}
