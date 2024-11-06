using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaming_Init : GamingUnit
{
    public override void Init()
    {
        // 初始化数值
        GamingManager.Instance.InitOnce();
        // 显示游戏界面
        UIManager.Instance.CloseUI("GamingUI");
        UIManager.Instance.ShowUI<GamingUI>("GamingUI");
        UIManager.Instance.CloseUI("GridUI");
        UIManager.Instance.ShowUI<GridUI>("GridUI");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
