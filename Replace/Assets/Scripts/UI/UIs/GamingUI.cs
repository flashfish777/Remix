using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 游戏界面
/// </summary>
public class GamingUI : UIBase
{
    private Slider waterSld;

    private void Awake()
    {
        waterSld = transform.Find("water").GetComponent<Slider>();

        // 返回主菜单
        Register("quitBtn").onClick = onQuitBtn;
        // 设置
        Register("settings").onClick = onSettingsBtn;
        // 图鉴
        Register("dictionary").onClick = onDictionaryBtn;
        // 完成
        Register("complete").onClick = onCompleteBtn;
    }

    private void onCompleteBtn(GameObject @object, PointerEventData data)
    {
        UIManager.Instance.CloseAllUI();
        
        GamingManager.Instance.ChangeType(GamingType.Complete);
    }

    private void onDictionaryBtn(GameObject @object, PointerEventData data)
    {
        Time.timeScale = 0;

        UIManager.Instance.ShowUI<DictionaryUI>("DictionaryUI");
    }

    private void onSettingsBtn(GameObject @object, PointerEventData data)
    {
        Time.timeScale = 0;

        UIManager.Instance.ShowUI<SettingsUI>("SettingsUI");
    }

    private void onQuitBtn(GameObject @object, PointerEventData data)
    {
        UIManager.Instance.CloseAllUI();

        GamingManager.Instance.ChangeType(GamingType.None);

        UIManager.Instance.ShowUI<LoginUI>("LoginUI");
    }

    public void UpdateWater()
    {
        waterSld.value = GamingManager.Instance.GetWaterSld();
        waterSld.transform.Find("WaterCountDown").GetComponent<Text>().text = GamingManager.Instance.GetWaterTxt();
    }
}
