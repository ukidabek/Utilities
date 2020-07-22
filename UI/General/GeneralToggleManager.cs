using UnityEngine.UI;

namespace Utilities.General.UI
{
    public class GeneralToggleManager : GeneralUiElementManager<Toggle>
    {
        public Toggle.ToggleEvent OnValueChanged => m_selectable.onValueChanged;
    }
}