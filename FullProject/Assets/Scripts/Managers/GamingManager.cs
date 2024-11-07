using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

/// <summary>
/// 游戏状态
/// </summary>
public enum GamingType
{
    None,
    Init,
    Complete,
    Lose
}

/// <summary>
/// 游戏管理器
/// </summary>
public class GamingManager : MonoBehaviour
{
    public static GamingManager Instance;

    public GamingUnit gamingUnit; // 战斗单元

    public int MaxQi; // 最大水滴数
    public int CurQi; // 当前水滴数

    // 初始化
    public void Init()
    {
        MaxQi = 20;
    }

    // 单局初始化
    public void InitOnce()
    {
        CurQi = MaxQi;
    }

    private void Awake()
    {
        Instance = this;
    }

    // 切换战斗类型
    public void ChangeType(GamingType type)
    {
        switch (type)
        {
            case GamingType.None:
                gamingUnit = null;
                break;
            case GamingType.Init:
                gamingUnit = new Gaming_Init();
                break;
            case GamingType.Complete:
                gamingUnit = new Gaming_Compelete();
                break;
            case GamingType.Lose:
                gamingUnit = new Gaming_Lose();
                break;
        }
        gamingUnit?.Init(); // 初始化
    }

    private void Update()
    {
        if (gamingUnit != null)
        {
            gamingUnit.OnUpdate();
        }
    }

    public float GetWaterSld()
    {
        return CurQi / (float)MaxQi;
    }

    public string GetWaterTxt()
    {
        return CurQi.ToString() + "/" + MaxQi.ToString();
    }

    public void UseQi() {
        if (CurQi == 0)
        {
            return;
        }
        CurQi--;
        if (CurQi == 0) { 

        
        }

    }
}
