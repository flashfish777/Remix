using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GridUI : UIBase
{
    float z1;
    float z2;

    private void Awake()
    {
        // 顺时针旋转
        Register("clockwise").onClick = onClockwiseBtn;
        // 逆时针旋转
        Register("unClockwise").onClick = onUnClockwiseBtn;

        z1 = 0;
        z2 = 0;
    }

    private void onClockwiseBtn(GameObject @object, PointerEventData data)
    {
        z1 -= 90;
        transform.Find("midel1").DORotate(new Vector3(0f, 0f, z1), 1f);
        z1 = z1 == -360 ? 0 : z1;
    }

    private void onUnClockwiseBtn(GameObject @object, PointerEventData data)
    {
        z2 += 90;
        transform.Find("midel2").DORotate(new Vector3(0f, 0f, z2), 1f);
        z2 = z2 == 360 ? 0 : z2;
    }
}
