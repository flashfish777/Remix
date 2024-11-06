using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 格子管理器
/// </summary>
public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
