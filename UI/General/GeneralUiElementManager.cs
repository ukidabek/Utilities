using UnityEngine;
using UnityEngine.UI;

namespace Utilities.General.UI
{
    public abstract class GeneralUiElementManager : MonoBehaviour
    {
        [SerializeField] protected Text text = null;
        public string Text
        {
            get => text.text;
            set => text.text = value;
        }
        
        [SerializeField] protected Image image = null;
        public Sprite Image
        {
            get => image.sprite;
            set => image.sprite = value;
        }
        public abstract bool Interactable { get; set; }
    }
    
    public abstract class GeneralUiElementManager<T> : GeneralUiElementManager where T : Selectable
    {
        [SerializeField] protected T m_selectable = null;
        public T Selectable => m_selectable;
        
        public override bool Interactable
        {
            get => m_selectable.interactable;
            set => m_selectable.interactable = value;
        }
    }
}