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

    public void OnPointerEnter(PointerEventData eventData) => OnPointerEnterCallback.Invoke(eventData);

    public void OnPointerExit(PointerEventData eventData) => OnPointerExitCallback.Invoke(eventData);
}