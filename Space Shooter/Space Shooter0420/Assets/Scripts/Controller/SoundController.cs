using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSFXType
{
    ExpAst,
    ExpEnemy,
    ExpPlayer,
    FireEnemy,
    FirePlayer
}
public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource mBGM, mEffect;
    [SerializeField]
    private AudioClip[] mBGMClip, mEffectClip;
    // Start is called before the first frame update
    void Start()
    {
        //audioclip 로드
        //audio setting 로드
    }

    public void SetBGMVolume(float value)
    {
        mBGM.volume = value;
        //끄고키기가능
        //mBGM.Stop(); //멈추기
        //mBGM.Play(); //play
        //bool b = mBGM.isPlaying; //play중인지 확인
    }

    public void SetEffectVolume(float value) //0~1 0~100%  
    {
        mEffect.volume = value;
    }

    public void ChangeBGM(int index)
    {//멈춘뒤 바꾸고 실행
        mBGM.Stop(); 
        mBGM.clip = mBGMClip[index];
        mBGM.Play(); 
    }
    public void PlayEffectSound (int index)
    {
        mEffect.PlayOneShot(mEffectClip[index]);
        //AudioSource.PlayClipAtPoint(mEffectClip[index], Vector3.zero);//기능은 같은데 만들었다 디스트로이함. 사용x
    }
}
