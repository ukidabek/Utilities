using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic.Utilities.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UIPanelMover : MonoBehaviour
    {
        public enum Status
        {
            Open,
            Close
        }

        [SerializeField] private Status m_status = Status.Close;
        [SerializeField] private Vector2 m_openPosition = Vector2.zero;
        [SerializeField] private Vector2 m_closePosition = Vector2.zero;
        [SerializeField] private float m_speed = 5f;
        [SerializeField] private float value = 0f;
        private Coroutine coroutine = null;
        private RectTransform rectTransform;

        private float openClosePositionDistance = 0f;

        private void Awake()
        {
            openClosePositionDistance = Vector2.Distance(m_openPosition, m_closePosition);
            rectTransform = transform as RectTransform;
            switch (m_status)
            {
                case Status.Open:
                    rectTransform.anchoredPosition = m_openPosition;
                    break;
                case Status.Close:
                    rectTransform.anchoredPosition = m_closePosition;
                    break;
            }
        }

        private IEnumerator MoveCoroutine(Vector2 position)
        {
            while (rectTransform.anchoredPosition != position)
            {
                rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, position, m_speed);
                var distance = Vector2.Distance(rectTransform.anchoredPosition, position);
                value = 1 - (distance / openClosePositionDistance);
                yield return null;
            }

            coroutine = null;
        }

        private void HandleCoroutine(Vector2 position)
        {
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