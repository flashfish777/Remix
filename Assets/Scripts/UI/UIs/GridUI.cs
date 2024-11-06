using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

/*
这里的格子UI我是直接在预制体中放了一堆Buttton组成的，分几圈，是测试用的（可以理解为写着玩的）
实现的时候我想的是一个格子UI只对应一个格子，生成的时候在特定位置生成，4*5+25个
GridUI里有获取点击的逻辑，格子移动的逻辑等等，然后通过GridManager管理它们，格子也可以不一定是Button，
可以用射线检测点击之类的，反正看你们来嘛，有好的想法也可以。然后点击格子之后会生成水滴的UI，
水滴UI里就写一些水滴的相关逻辑，比如小中大水，大水破裂按照格子属性四个方向发射该属性小水，
被射入的格子如果射入水滴克制被射入格子水属性抵消相应容量（大水变中，中变小），相生就反向增大等等，
水滴的大小可以通过换水滴UI的Image，或者其他想法，然后通过水滴Manager管理水滴这样
*/

/// <summary>
/// 格子UI
/// </summary>
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
