using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SaveSystemTutorial;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using System;

/// <summary>
/// 音频和语言管理器
/// </summary>
public class AudioManager : MonoBehaviour
{
    /*
    bgm : GameApp \ Gaming_Init \ Gaming_Compelete \ GamingUI \ CompeleteUI \ Gaming_Lose \ LoseUI
    IntfaceBgm : GamingUI \ UIEventTrigger \ GridUI
    Effects : GridUI
    */
    public static AudioManager Instance;

    private AudioSource bgmSource; // 播放bgm的音频
    private AudioSource intfSource; // 播放界面音的音频
    private AudioSource effectSource; // 播放音效的音频

    public float allValue; // 总音量
    public bool isAll; // 开关
    public float bgmValue; // bgm的音量
    public bool isBgm; // 开关
    public float intfValue; // 界面音的音量
    public bool isIntf; // 开关
    public float effectValue; // 音效的音量
    public bool isEffect; // 开关

    public string LanguageIndex; // 当前语言

    private void Awake()
    {
        Instance = this;
    }

    // 初始化
    public void Init()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        intfSource = gameObject.AddComponent<AudioSource>();
        effectSource = gameObject.AddComponent<AudioSource>();

        // 从存档中调取音量、静音、语言值
        allValue = SaveManager.Instance.GetData().allValue;
        isAll = SaveManager.Instance.GetData().isAll;
        bgmValue = SaveManager.Instance.GetData().bgmValue;
        isBgm = SaveManager.Instance.GetData().isBgm;
        intfValue = SaveManager.Instance.GetData().intfValue;
        isIntf = SaveManager.Instance.GetData().isIntf;
        effectValue = SaveManager.Instance.GetData().effectValue;
        isEffect = SaveManager.Instance.GetData().isEffect;
        LanguageIndex = SaveManager.Instance.GetData().LanguageIndex;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(LanguageIndex);
    }

    // 播放bgm
    public void PlayBGM(string name, bool isLoop = true)
    {
        // 加载bgm声音剪辑
        AudioClip clip = Resources.Load<AudioClip>("Sounds/BGM/" + name);

        bgmSource.clip = clip; // 设置音频
        bgmSource.loop = isLoop; // 循环
        bgmSource.volume = bgmValue * allValue * (isAll == true ? 1 : 0) * (isBgm == true ? 1 : 0); // 音量
        bgmSource.Play();
    }

    // 播放界面音
    public void PlayIntf(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/Intface/" + name);

        intfSource.clip = clip; // 设置音频
        intfSource.loop = false; // 不循环
        intfSource.volume = intfValue * allValue * (isAll == true ? 1 : 0) * (isIntf == true ? 1 : 0); // 音量
        intfSource.Play();
    }

    // 播放音效
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/Effect/" + name);

        effectSource.clip = clip; // 设置音频
        effectSource.loop = false; // 不循环
        effectSource.volume = effectValue * allValue * (isAll == true ? 1 : 0) * (isEffect == true ? 1 : 0); // 音量
        effectSource.Play();
    }

    // 播放时刷新音量
    public void UpdateVolume()
    {
        bgmSource.volume = bgmValue * allValue * (isAll == true ? 1 : 0) * (isBgm == true ? 1 : 0);
        intfSource.volume = intfValue * allValue * (isAll == true ? 1 : 0) * (isIntf == true ? 1 : 0);
        effectSource.volume = effectValue * allValue * (isAll == true ? 1 : 0) * (isEffect == true ? 1 : 0);
    }

    public void SaveSettings()
    {
        SaveManager.SettingsData settingsData = new()
        {
            allValue = allValue,
            isAll = isAll,
            bgmValue = bgmValue,
            isBgm = isBgm,
            intfValue = intfValue,
            isIntf = isIntf,
            effectValue = effectValue,
            isEffect = isEffect,
            LanguageIndex = LanguageIndex
        };

        SaveManager.Instance.SaveGameset(settingsData);
    }
}
