using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 图鉴界面
/// </summary>
public class GamingDicUI : UIBase
{
    private void Awake()
    {
        // 返回
        Register("BackButton").onClick = onBackBtn;
    }

    private void onBackBtn(GameObject @object, PointerEventData data)
    {
        Close();
    }
}
