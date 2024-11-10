using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏失败
/// </summary>
public class Gaming_Lose : GamingUnit
{
    public override void Init()
    {
        // 切换bgm（如果有）（不循环）
        AudioManager.Instance.PlayBGM("LoseBgm", false);
        // 显示失败界面
        UIManager.Instance.ShowUI<LoseUI>("LoseUI");
    }
}
