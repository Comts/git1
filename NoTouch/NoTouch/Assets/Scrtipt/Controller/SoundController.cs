using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    private const string MIXER_MASTER = "MixerMaster";
    private const string MUTE_MIXER_MASTER = "MuteMixerMaster";
    private const string MIXER_BGM = "MixerBGM";
    private const string MUTE_MIXER_BGM = "MuteMixerBGM";
    private const string MIXER_FX = "MixerEffect";
    private const string MUTE_MIXER_FX = "MuteMixerEffect";
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
    private void Start()
    {
        if (PlayerPrefs.GetFloat(MIXER_MASTER, LoadMaster)!=0)
        {
            mMasterSlider.value = MasterVolume = PlayerPrefs.GetFloat(MIXER_MASTER, LoadMaster);
        }
        if (PlayerPrefs.GetInt(MUTE_MIXER_MASTER) == 1)
        {
            MasterVolume = 0.00001f;
            mMuteMaster.gameObject.SetActive(true);
            mMasterSlider.interactable = false;
        }

        if (PlayerPrefs.GetFloat(MIXER_BGM, LoadBGM)!=0)
        {
            mBGMSlider.value = BGMVolume = PlayerPrefs.GetFloat(MIXER_BGM, LoadBGM);
        }
        if (PlayerPrefs.GetInt(MUTE_MIXER_BGM) == 1)
        {
            BGMVolume = 0.00001f;
            mMuteBGM.gameObject.SetActive(true);
            mBGMSlider.interactable = false;
        }

        if(PlayerPrefs.GetFloat(MIXER_FX, LoadFX)!=0)
        {
            mEffectSlider.value = EffectVolume = PlayerPrefs.GetFloat(MIXER_FX, LoadFX);
        }
        if (PlayerPrefs.GetInt(MUTE_MIXER_FX) == 1)
        {
            EffectVolume = 0.00001f;
            mMuteEffect.gameObject.SetActive(true);
            mEffectSlider.interactable = false;
        }

    }
    public void ChangeBGM(int num)
    {
        mBGM.clip = mBGMArr[num];
        if (num == 1)
        {
            mBGM.PlayOneShot(mBGMArr[num]);
        }
        else
        {
            mBGM.Play();
        }
    }
    public void FXSound(int num)
    {
        mEffect.PlayOneShot(mEffectArr[num]);
    }
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
                PlayerPrefs.SetFloat(MIXER_MASTER, LoadMaster);
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
            PlayerPrefs.SetInt(MUTE_MIXER_MASTER, 1);
        }
        else
        {
            mMuteMaster.gameObject.SetActive(false);
            MasterVolume = LoadMaster;
            mMasterSlider.interactable = true;
            PlayerPrefs.SetInt(MUTE_MIXER_MASTER, 0);
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
                PlayerPrefs.SetFloat(MIXER_BGM, LoadBGM);
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
            PlayerPrefs.SetInt(MUTE_MIXER_BGM, 1);
        }
        else
        {
            mMuteBGM.gameObject.SetActive(false);
            BGMVolume = LoadBGM;
            mBGMSlider.interactable = true;
            PlayerPrefs.SetInt(MUTE_MIXER_BGM, 0);
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
                PlayerPrefs.SetFloat(MIXER_FX, LoadFX);
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
            PlayerPrefs.SetInt(MUTE_MIXER_FX, 1);
        }
        else
        {
            mMuteEffect.gameObject.SetActive(false);
            EffectVolume = LoadFX;
            mEffectSlider.interactable = true;
            PlayerPrefs.SetInt(MUTE_MIXER_FX, 0);
        }
    }

}
