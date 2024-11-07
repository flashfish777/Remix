using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SaveSystemTutorial;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

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

    public int LanguageIndex; // 当前语言

    [System.Serializable]
    class SettingsData
    {
        public float allValue = 1; // 总音量
        public bool isAll = true; // 开关
        public float bgmValue = 1; // bgm的音量
        public bool isBgm = true; // 开关
        public float intfValue = 1; // 界面音的音量
        public bool isIntf = true; // 开关
        public float effectValue = 1; // 音效的音量
        public bool isEffect = true; // 开关

        public int LanguageIndex = 0; // 当前语言
    }

    SettingsData settingsData;

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
        settingsData = SaveSystem.ReadFromJson<SettingsData>("settings.xdata") ?? new SettingsData();

        allValue = settingsData.allValue;
        isAll = settingsData.isAll;
        bgmValue = settingsData.bgmValue;
        isBgm = settingsData.isBgm;
        intfValue = settingsData.intfValue;
        isIntf = settingsData.isIntf;
        effectValue = settingsData.effectValue;
        isEffect = settingsData.isEffect;
        LanguageIndex = settingsData.LanguageIndex;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[LanguageIndex];
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
        settingsData = new()
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

        SaveSystem.SaveByJson("settings.xdata", settingsData);
    }
}
