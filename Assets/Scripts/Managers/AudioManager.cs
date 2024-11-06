using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.Utilities;

/// <summary>
/// 音频和语言管理器
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource bgmSource; // 播放bgm的音频
    private AudioSource intfSource; // 播放界面音的音频
    private AudioSource effectSource; // 播放音效的音频

    public float allValue; // 总音量
    public bool isAll; // 是否静音
    public float bgmValue; // bgm的音量
    public bool isBgm; // 是否静音
    public float intfValue; // 界面音的音量
    public bool isIntf; // 是否静音
    public float effectValue; // 音效的音量
    public bool isEffect; // 是否静音

    public int LanguageIndex; // 当前语言

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
        LanguageIndex = 0;
        allValue = 1;
        isAll = true;
        bgmValue = 0.5f;
        isBgm = true;
        intfValue = 0.35f;
        isIntf = false;
        effectValue = 0.75f;
        isEffect = true;
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
}
