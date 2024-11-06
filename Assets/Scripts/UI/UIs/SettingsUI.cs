using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SettingsUI : UIBase
{
    private Slider allAudioS;
    private Toggle allAudioT;
    private Slider effectS;
    private Toggle effectT;
    private Slider bgS;
    private Toggle bgT;
    private Slider intfS;
    private Toggle intfT;
    private Dropdown languageDropdown;

    private void Awake()
    {
        allAudioS = transform.Find("VolumeScreen/MasterVolume/Slider").GetComponent<Slider>();
        allAudioT = transform.Find("VolumeScreen/MasterVolume/Toggle").GetComponent<Toggle>();
        effectS = transform.Find("VolumeScreen/SoundEffects/Slider").GetComponent<Slider>();
        effectT = transform.Find("VolumeScreen/SoundEffects/Toggle").GetComponent<Toggle>();
        bgS = transform.Find("VolumeScreen/BgMusic/Slider").GetComponent<Slider>();
        bgT = transform.Find("VolumeScreen/BgMusic/Toggle").GetComponent<Toggle>();
        intfS = transform.Find("VolumeScreen/InterfaceMusic/Slider").GetComponent<Slider>();
        intfT = transform.Find("VolumeScreen/InterfaceMusic/Toggle").GetComponent<Toggle>();
        languageDropdown = transform.Find("LanguageScreen/Dropdown").GetComponent<Dropdown>();

        allAudioS.value = AudioManager.Instance.allValue;
        allAudioT.isOn = AudioManager.Instance.isAll;
        effectS.value = AudioManager.Instance.effectValue;
        effectT.isOn = AudioManager.Instance.isEffect;
        bgS.value = AudioManager.Instance.bgmValue;
        bgT.isOn = AudioManager.Instance.isBgm;
        intfS.value = AudioManager.Instance.intfValue;
        intfT.isOn = AudioManager.Instance.isIntf;
        languageDropdown.value = AudioManager.Instance.LanguageIndex;

        // 返回
        Register("SettingsOptions/BackButton").onClick = onBackBtn;
        // 音频设置
        Register("SettingsOptions/AudioButton").onClick = onAudioBtn;
        // 语言设置
        Register("SettingsOptions/LanguageButton").onClick = onLanguageBtn;
    }

    private void onAudioBtn(GameObject @object, PointerEventData data)
    {
        transform.Find("VolumeScreen").gameObject.SetActive(true);
        transform.Find("LanguageScreen").gameObject.SetActive(false);
    }

    private void onLanguageBtn(GameObject @object, PointerEventData data)
    {
        transform.Find("VolumeScreen").gameObject.SetActive(false);
        transform.Find("LanguageScreen").gameObject.SetActive(true);
    }

    private void onBackBtn(GameObject @object, PointerEventData data)
    {
        Time.timeScale = 1;
        Close();
    }

    private void Update()
    {
        if (languageDropdown.value != AudioManager.Instance.LanguageIndex)
        {
            //将下拉框当前选中选项的下标作为参数设置到LocalzationSettings的SelectedLocale达到实现语言切换的效果
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageDropdown.value];
            AudioManager.Instance.LanguageIndex = languageDropdown.value;
        }
        if (allAudioS.value != AudioManager.Instance.allValue)
        {
            AudioManager.Instance.allValue = allAudioS.value;
            AudioManager.Instance.UpdateVolume();
        }
        if (allAudioT.isOn != AudioManager.Instance.isAll)
        {
            AudioManager.Instance.isAll = allAudioT.isOn;
            AudioManager.Instance.UpdateVolume();
        }
        if (bgS.value != AudioManager.Instance.bgmValue)
        {
            AudioManager.Instance.bgmValue = bgS.value;
            AudioManager.Instance.UpdateVolume();
        }
        if (bgT.isOn != AudioManager.Instance.isBgm)
        {
            AudioManager.Instance.isBgm = bgT.isOn;
            AudioManager.Instance.UpdateVolume();
        }
        if (intfS.value != AudioManager.Instance.intfValue)
        {
            AudioManager.Instance.intfValue = intfS.value;
            AudioManager.Instance.UpdateVolume();
        }
        if (intfT.isOn != AudioManager.Instance.isIntf)
        {
            AudioManager.Instance.isIntf = intfT.isOn;
            AudioManager.Instance.UpdateVolume();
        }
        if (effectS.value != AudioManager.Instance.effectValue)
        {
            AudioManager.Instance.effectValue = effectS.value;
            AudioManager.Instance.UpdateVolume();
        }
        if (effectT.isOn != AudioManager.Instance.isEffect)
        {
            AudioManager.Instance.isEffect = effectT.isOn;
            AudioManager.Instance.UpdateVolume();
        }
    }
}
