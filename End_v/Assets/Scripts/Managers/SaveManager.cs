using System.Collections;
using System.Collections.Generic;
using SaveSystemTutorial;
using UnityEngine;

public class SaveManager
{
    public static SaveManager Instance = new SaveManager();

    [System.Serializable]
    public class SettingsData
    {
        public float allValue = 1; // 总音量
        public bool isAll = true; // 开关
        public float bgmValue = 0.5f; // bgm的音量
        public bool isBgm = true; // 开关
        public float intfValue = 1; // 界面音的音量
        public bool isIntf = true; // 开关
        public float effectValue = 1; // 音效的音量
        public bool isEffect = true; // 开关
        public string LanguageIndex = "en"; // 当前语言

        public int num;
        public List<string> dic = new List<string>();
    }

    SettingsData sett = new SettingsData();

    private void Awake()
    {
        Instance = this;
    }

    public SettingsData GetData()
    {
        sett = SaveSystem.ReadFromJson<SettingsData>("settings.xdata") ?? new SettingsData();
        return sett;
    }

    public void SaveGameset(SettingsData sdata)
    {
        sett.allValue = sdata.allValue;
        sett.isAll = sdata.isAll;
        sett.bgmValue = sdata.bgmValue;
        sett.isBgm = sdata.isBgm;
        sett.intfValue = sdata.intfValue;
        sett.isIntf = sdata.isIntf;
        sett.effectValue = sdata.effectValue;
        sett.isEffect = sdata.isEffect;
        sett.LanguageIndex = sdata.LanguageIndex;
        SaveSystem.SaveByJson("settings.xdata", sdata);
    }

    public void SaveGamedic(SettingsData sdata)
    {
        sett.num = sdata.num;
        sett.dic = sdata.dic;
        Debug.Log(sett.dic);
        SaveSystem.SaveByJson("settings.xdata", sdata);
    }
}
