using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class UIEventTrigger : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Action<GameObject, PointerEventData> onClick;
    public Action<GameObject, PointerEventData> onEnter;
    public Action<GameObject, PointerEventData> onExit;

    public static UIEventTrigger Get(GameObject obj)
    {
        UIEventTrigger trigger = obj.GetComponent<UIEventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<UIEventTrigger>();
        }
        return trigger;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlayIntf("UIIntf");

        onClick?.Invoke(gameObject, eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onEnter?.Invoke(gameObject, eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onExit?.Invoke(gameObject, eventData);
    }
}
