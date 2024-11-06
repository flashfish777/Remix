using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DictionaryUI : UIBase
{
    private void Awake()
    {
        // 返回
        Register("BackButton").onClick = onBackBtn;
    }

    private void onBackBtn(GameObject @object, PointerEventData data)
    {
        Time.timeScale = 1;
        Close();
    }
}
