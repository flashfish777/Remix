using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡管理器
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int level;

    private void Awake()
    {
        Instance = this;
    }

    public void NextLevel()
    {
        level++;
    }
}
