using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    private const string MIXER_MASTER = "MixerMaster";
    private const string MIXER_BGM = "MixerBGM";
    private const string MIXER_FX = "MixerEffect";
    [SerializeField]
    private AudioSource mBGM, mEffect;
    [SerializeField]
    private AudioClip[] mBGMArr, mEffectArr;
    [SerializeField]
    private AudioMixer mMixer;

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
            mMixer.SetFloat(MIXER_MASTER, vol);
        }
    }
    public void MuteMasterVolume()
    {
        if(LoadMaster != 0.00001f)
        {
            MasterVolume = 0.00001f;
        }
        else
        {
            MasterVolume = LoadMaster;
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
            mMixer.SetFloat(MIXER_BGM, vol);
        }
    }
    public void MuteBGMVolume()
    {
        if (LoadBGM != 0.00001f)
        {
            BGMVolume = 0.00001f;
        }
        else
        {
            BGMVolume = LoadBGM;
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
            mMixer.SetFloat(MIXER_FX, vol);
        }
    }
    public void MuteEffectVolume()
    {
        if (LoadFX != 0.00001f)
        {
            EffectVolume = 0.00001f;
        }
        else
        {
            EffectVolume = LoadFX;
        }
    }

    public void FXSound(int num)
    {
        mEffect.PlayOneShot(mEffectArr[num]);
    }
}
