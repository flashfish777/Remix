using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 界面基类
/// </summary>
public class UIBase : MonoBehaviour
{
    // 注册事件
    public UIEventTrigger Register(string name)
    {
        Transform tf = transform.Find(name);
        return UIEventTrigger.Get(tf.gameObject);
    }
    
    // 显示
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    // 隐藏
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    // 关闭界面（销毁）
    public virtual void Close()
    {
        UIManager.Instance.CloseUI(gameObject.name);
    }
}
