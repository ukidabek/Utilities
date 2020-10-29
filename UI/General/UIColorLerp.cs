using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.General.UI
{
    public class UIColorLerp : MonoBehaviour
    {
        private enum Mode { AtoB, BtoA }
        [SerializeField] private Mode _mode = Mode.AtoB;
        [SerializeField] private Graphic _graphic = null;
        [SerializeField] private Color _a = new Color();
        [SerializeField] private Color _b = new Color();

        private void Awake()
        {
            switch (_mode)
            {
                case Mode.AtoB:
                    _graphic.color = _a;
                    break;
                case Mode.BtoA:
                    _graphic.color = _b;
                    break;
            }
        }

        public void LerpColor(float fill)
        {
            var t = _mode == Mode.AtoB ? fill : 1 - fill;
            _graphic.color = Color.Lerp(_a, _b, t);
        }

        public void SetModeToAToB() => _mode = Mode.AtoB;
        public void SetModeToBToA() => _mode = Mode.BtoA;
    }
}