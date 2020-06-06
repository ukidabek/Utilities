using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Utilities.General.UI
{
    public class UiSelectableManager : MonoBehaviour
    {
        [SerializeField] private bool m_autoInitialize = true;
        [SerializeField] private Selectable[] m_selectable = null;

        private IEnumerator Start()
        {
            if (!m_autoInitialize) yield break;
            yield return null;
            m_selectable = gameObject.GetComponentsInChildren<Selectable>(true);
        }

        public void SetStatus(bool isInteractable)
        {
            foreach (var selectable in m_selectable)
                if (selectable.interactable != isInteractable)
                    selectable.interactable = isInteractable;
        }

        public void BlockInput() => SetStatus(false);
        public void UnlockInput() => SetStatus(true);
    }
}