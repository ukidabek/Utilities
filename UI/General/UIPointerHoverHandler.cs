using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIPointerHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Serializable]
    public class OnPointerEventHandler : UnityEvent<PointerEventData>
    {
    }

    public OnPointerEventHandler OnPointerEnterCallback = new OnPointerEventHandler();
    public OnPointerEventHandler OnPointerExitCallback = new OnPointerEventHandler();

    private void OnEnable() {}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!enabled) return;
        OnPointerEnterCallback.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!enabled) return;
        OnPointerExitCallback.Invoke(eventData);
    }
}