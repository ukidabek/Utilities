using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.General.UI
{
    [Serializable] public class UIColorSet
    {
        [SerializeField] private Graphic _graphic = null;
        [SerializeField] private Color _a = new Color();

        public void SetColor()
        {
            _graphic.color = _a;
        }
    }
}