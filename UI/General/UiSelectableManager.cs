using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.General.UI
{
    public class UiSelectableManager : MonoBehaviour
    {
        [SerializeField] private bool m_autoInitialize = true;
        [SerializeField] private bool m_reactOnHierarchyChange = false;
        [SerializeField] private List<Selectable> m_initialSelectable = new List<Selectable>();
        [SerializeField] private List<Selectable> m_selectable = null;
        [SerializeField] private List<Selectable> m_selectableExceptions = null;
        [SerializeField] private List<Transform> m_excludedTransforms = null;
        private IEnumerator Start()
        {
            if (!m_autoInitialize) yield break;
            yield return null;
            GetSelectables();
        }
        
        private void GetSelectables()
        {
            m_selectable = gameObject.GetComponentsInChildren<Selectable>(true)
                .Where(selectable => !m_selectableExceptions.Contains(selectable))
                .Where(selectable => m_excludedTransforms.All(transform1 => !selectable.transform.IsChildOf(transform1)))
                .Concat(m_initialSelectable)
                .ToList();
        }

        private void OnTransformChildrenChanged()
        {
            if(!m_reactOnHierarchyChange) return;
            GetSelectables();
        }

        public void SetStatus(bool isInteractable)
        {
            foreach (var selectable in m_selectable)
            {
                if(selectable == null) continue;
                if (selectable.interactable != isInteractable)
                    selectable.interactable = isInteractable;
            }
        }

        public void BlockInput() => SetStatus(false);
        public void UnlockInput() => SetStatus(true);
    }
}