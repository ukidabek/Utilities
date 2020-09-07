using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.General.UI
{
    public class UiSelectableManager : MonoBehaviour
    {
        [SerializeField] private bool m_autoInitialize = true;
        [SerializeField] private bool m_reactOnHierarchyChange = false;
        [SerializeField] private Selectable[] m_selectable = null;

        private IEnumerator Start()
        {
            if (!m_autoInitialize) yield break;
            yield return null;
            GetSelectables();
        }
        
        private void GetSelectables()
        {
            m_selectable = gameObject.GetComponentsInChildren<Selectable>(true);
        }

        private void OnTransformChildrenChanged()
        {
            if(!m_reactOnHierarchyChange) return;
            GetSelectables();
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