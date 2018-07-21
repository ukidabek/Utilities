using UnityEngine;
using UnityEngine.UI;

namespace BaseGameLogic.Utilities.UI
{
    public class ColorLerp : MonoBehaviour
    {
        private enum Mode { AtoB, BtoA }
        [SerializeField] private Mode _mode = Mode.AtoB;
        [SerializeField] private Image _image = null;
        [SerializeField] private Color _a = new Color();
        [SerializeField] private Color _b = new Color();

        private void Awake()
        {
            _image.color = _a;
        }

        public void LerpColor(float fill)
        {
            var t = _mode == Mode.AtoB ? fill : 1 - fill;
            _image.color = Color.Lerp(_a, _b, t);
        }
    }
}