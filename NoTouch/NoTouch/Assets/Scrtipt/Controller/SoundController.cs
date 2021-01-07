using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    private const string MIXER_MASTER = "MixerMaster";
    private const string MIXER_BGM = "MixerBGM";
    private const string MIXER_FX = "MixerEffect";
#pragma warning disable 0649
    [SerializeField]
    private AudioSource mBGM, mEffect;
    [SerializeField]
    private AudioClip[] mBGMArr, mEffectArr;
    [SerializeField]
    private AudioMixer mMixer;
    [SerializeField] 
    private Image mMuteMaster,mMuteBGM, mMuteEffect;
    [SerializeField]
    private Slider mMasterSlider, mBGMSlider, mEffectSlider;
#pragma warning restore 0649
    private float LoadMaster,LoadBGM,LoadFX;

    public float MasterVolume
    {
        
        get
        {
            float vol;
            mMixer.GetFloat(MIXER_MASTER, out vol);
            return vol;
        }
        set
        {
            float vol = 20f * Mathf.Log10(value);
            if(value != 0.00001f)
            {
                LoadMaster = value;
            }
            if(!mMuteMaster.gameObject.activeInHierarchy)
            {
                mMixer.SetFloat(MIXER_MASTER, vol);
            }
        }
    }
    public void MuteMasterVolume()
    {
        if (!mMuteMaster.gameObject.activeInHierarchy)
        {
            MasterVolume = 0.00001f;
            mMuteMaster.gameObject.SetActive(true);
            mMasterSlider.interactable = false;
        }
        else
        {
            mMuteMaster.gameObject.SetActive(false);
            MasterVolume = LoadMaster;
            mMasterSlider.interactable = true;
        }
    }

    public float BGMVolume
    {
        get
        {
            float vol;
            mMixer.GetFloat(MIXER_BGM, out vol);
            return vol;
        }
        set
        {
            float vol = 20f * Mathf.Log10(value);
            if (value != 0.00001f)
            {
                LoadBGM = value;
            }
            if (!mMuteBGM.gameObject.activeInHierarchy)
            {
                mMixer.SetFloat(MIXER_BGM, vol);
            }
        }
    }
    public void MuteBGMVolume()
    {
        if (!mMuteBGM.gameObject.activeInHierarchy)
        {
            BGMVolume = 0.00001f;
            mMuteBGM.gameObject.SetActive(true);
            mBGMSlider.interactable = false;
        }
        else
        {
            mMuteBGM.gameObject.SetActive(false);
            BGMVolume = LoadBGM;
            mBGMSlider.interactable = true;
        }
    }

    public float EffectVolume
    {
        get
        {
            float vol;
            mMixer.GetFloat(MIXER_FX, out vol);
            return vol;
        }
        set
        {
            float vol = 20f * Mathf.Log10(value);
            if (value != 0.00001f)
            {
                LoadFX = value;
            }
            if (!mMuteEffect.gameObject.activeInHierarchy)
            {
                mMixer.SetFloat(MIXER_FX, vol);
            }
        }
    }
    public void MuteEffectVolume()
    {
        if (!mMuteEffect.gameObject.activeInHierarchy)
        {
            EffectVolume = 0.00001f;
            mMuteEffect.gameObject.SetActive(true);
            mEffectSlider.interactable = false;
        }
        else
        {
            mMuteEffect.gameObject.SetActive(false);
            EffectVolume = LoadFX;
            mEffectSlider.interactable = true;
        }
    }

    public void FXSound(int num)
    {
        mEffect.PlayOneShot(mEffectArr[num]);
    }
}
