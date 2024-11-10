using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;

/// <summary>
/// 游戏界面
/// </summary>
public class GamingUI : UIBase
{
    private Slider waterSld;
    private GameObject completeBtn;

    private void Awake()
    {
        waterSld = transform.Find("water").GetComponent<Slider>();

        // 返回主菜单
        Register("quitBtn").onClick = onQuitBtn;
        // 设置
        Register("settings").onClick = onSettingsBtn;
        // 图鉴
        Register("dictionary").onClick = onDictionaryBtn;
        // 帮助
        Register("help").onClick = onHelpBtn;
        // 完成
        completeBtn = transform.Find("complete").gameObject;
        completeBtn.GetComponent<Button>().onClick.AddListener(() => onCompleteBtn());
        completeBtn.SetActive(false);
    }

    private void onHelpBtn(GameObject @object, PointerEventData data)
    {
        UIManager.Instance.ShowUI<HelpUI>("HelpUI");
    }

    public void ShowCompleteBtn()
    {
        completeBtn.SetActive(true);
    }

    private void onCompleteBtn()
    {
        UIManager.Instance.CloseAllUI();
        
        GamingManager.Instance.ChangeType(GamingType.Complete);
    }

    private void onDictionaryBtn(GameObject @object, PointerEventData data)
    {
        UIManager.Instance.ShowUI<DictionaryUI>("DictionaryUI");
    }

    private void onSettingsBtn(GameObject @object, PointerEventData data)
    {
        UIManager.Instance.ShowUI<SettingsUI>("SettingsUI");
    }

    private void onQuitBtn(GameObject @object, PointerEventData data)
    {
        UIManager.Instance.CloseAllUI();

        GamingManager.Instance.ChangeType(GamingType.None);

        UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        AudioManager.Instance.PlayBGM("happy");
    }

    public void UpdateWater()
    {
        waterSld.value = GamingManager.Instance.GetWaterSld();
        waterSld.transform.Find("WaterCountDown").GetComponent<Text>().text = GamingManager.Instance.GetWaterTxt();
    }
}
