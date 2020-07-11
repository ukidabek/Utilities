using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.General.UI
{
    public class UIPanelMover : MonoBehaviour
    {
        [Serializable]
        public class OnMoveCallback : UnityEvent<float>
        {
        }
        
        public enum Status
        {
            Open,
            Close
        }

        [SerializeField] private RectTransform m_rectTransform = null;
        [SerializeField] private Status m_status = Status.Close;
        [SerializeField] private Vector2 m_openPosition = Vector2.zero;
        [SerializeField] private Vector2 m_closePosition = Vector2.zero;
        [SerializeField] private float m_speed = 5f;
        [SerializeField] private float value = 0f;
     
        [Space]
        private float openClosePositionDistance = 0f;
        public OnMoveCallback OnMove = new OnMoveCallback();
        
        private Coroutine coroutine = null;

        private void OnEnable() {}

        private void Awake()
        {
            openClosePositionDistance = Vector2.Distance(m_openPosition, m_closePosition);
            if (m_rectTransform == null && transform is RectTransform rectTransform)
                m_rectTransform = rectTransform;
            
            switch (m_status)
            {
                case Status.Open:
                    m_rectTransform.anchoredPosition = m_openPosition;
                    break;
                case Status.Close:
                    m_rectTransform.anchoredPosition = m_closePosition;
                    break;
            }
        }

        private IEnumerator MoveCoroutine(Vector2 position)
        {
            while (m_rectTransform.anchoredPosition != position)
            {
                m_rectTransform.anchoredPosition = Vector2.MoveTowards(m_rectTransform.anchoredPosition, position, m_speed);
                var distance = Vector2.Distance(m_rectTransform.anchoredPosition, position);
                OnMove.Invoke(value = 1 - distance / openClosePositionDistance);
                yield return null;
            }

            coroutine = null;
        }

        private void HandleCoroutine(Vector2 position)
        {
            if(!enabled) return;
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(MoveCoroutine(position));
        }

        [ContextMenu("Open")]
        public void Open() => HandleCoroutine(m_openPosition);

        [ContextMenu("Close")]
        public void Close() => HandleCoroutine(m_closePosition);
    }
}