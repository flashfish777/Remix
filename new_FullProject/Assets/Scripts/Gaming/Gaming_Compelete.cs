using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏完成
/// </summary>
public class Gaming_Compelete : GamingUnit
{
    public override void Init()
    {
        // 切换bgm（如果有）
        AudioManager.Instance.PlayBGM("CompeleteBgm");
        // 显示展示界面
        UIManager.Instance.ShowUI<CompeleteUI>("CompeleteUI");
    }
}
