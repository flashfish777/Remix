using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;

/// <summary>
/// 开始界面（继承UIBase）
/// </summary>
public class LoginUI : UIBase
{
    private GameObject EnTitle;
    private GameObject ChTitle;

    private void Awake()
    {
        // 开始游戏
        Register("ButtonController/startBtn").onClick = onStartGameBtn;
        // 设置
        Register("ButtonController/settings").onClick = onSettingsBtn;
        // 衣柜
        Register("ButtonController/dictionary").onClick = onDictionaryBtn;
        // 退出游戏
        Register("ButtonController/quitBtn").onClick = onExitGameBtn;

        EnTitle = transform.Find("GameTitle/English_Title").gameObject;
        ChTitle = transform.Find("GameTitle/Chinese_Title").gameObject;

        UpDateTitle();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(AudioManager.Instance.LanguageIndex);
    }

    public void UpDateTitle()
    {
        if (AudioManager.Instance.LanguageIndex == "en")
        {
            EnTitle.SetActive(true);
            ChTitle.SetActive(false);
        }
        else
        {
            EnTitle.SetActive(false);
            ChTitle.SetActive(true);
        }
    }

    private void onStartGameBtn(GameObject obj, PointerEventData pData)
    {
        // 关闭login界面
        Close();

        // 游戏初始化
        GamingManager.Instance.Init(); // 初始化战斗数值
        GamingManager.Instance.ChangeType(GamingType.Init);
    }

    private void onSettingsBtn(GameObject @object, PointerEventData data)
    {
        UIManager.Instance.ShowUI<SettingsUI>("SettingsUI");
    }

    private void onDictionaryBtn(GameObject @object, PointerEventData data)
    {
        UIManager.Instance.ShowUI<DictionaryUI>("DictionaryUI");
    }

    private void onExitGameBtn(GameObject obj, PointerEventData pData)
    {

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif

    }
}
