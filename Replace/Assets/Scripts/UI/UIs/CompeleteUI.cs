using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 展示UI
/// </summary>
public class CompeleteUI : UIBase
{
    private void Awake()
    {
        // 获取格子中的衣服

        // 返回主菜单
        Register("quitBtn").onClick = onQuitBtn;
        // 继续游戏
        Register("continueBtn").onClick = onContinueBtn;
    }

    private void onQuitBtn(GameObject @object, PointerEventData data)
    {
        Close();

        UIManager.Instance.ShowUI<LoginUI>("LoginUI");
    }

    private void onContinueBtn(GameObject @object, PointerEventData data)
    {
        Close();

        GamingManager.Instance.Init();
        GamingManager.Instance.ChangeType(GamingType.Init);
    }
}
