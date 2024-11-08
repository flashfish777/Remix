using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 图鉴界面
/// </summary>
public class HelpUI : UIBase
{
    private void Awake()
    {
        // 返回
        Register("Back/BackButton").onClick = onBackBtn;
    }

    private void onBackBtn(GameObject @object, PointerEventData data)
    {
        Close();
    }
}
