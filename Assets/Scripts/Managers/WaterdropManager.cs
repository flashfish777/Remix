using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 水滴管理器
/// </summary>
public class WaterdropManager : MonoBehaviour
{
    public static WaterdropManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
