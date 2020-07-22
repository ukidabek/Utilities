using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.General.UI
{
    public class GeneralButtonManager : GeneralUiElementManager<Button>
    {
        public Button.ButtonClickedEvent OnClick => m_selectable.onClick;
    }
}