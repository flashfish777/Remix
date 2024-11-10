using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 失败界面
/// </summary>
public class LoseUI : UIBase
{
    private void Awake()
    {
        // 返回主菜单
        Register("Panel/quitBtn").onClick = onBackBtn;
        // 重新开始
        Register("Panel/again").onClick = onAgainBtn;
    }

    private void onAgainBtn(GameObject @object, PointerEventData data)
    {
        Close();
        GamingManager.Instance.Init();
        GamingManager.Instance.ChangeType(GamingType.Init);
    }

    private void onBackBtn(GameObject @object, PointerEventData data)
    {
        UIManager.Instance.CloseAllUI();

        GamingManager.Instance.ChangeType(GamingType.None);

        UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        AudioManager.Instance.PlayBGM("happy");
    }
}
