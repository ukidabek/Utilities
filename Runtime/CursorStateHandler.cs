using UnityEngine;

namespace Utilities.General
{
    public class CursorStateHandler
    {
        private  CursorLockMode _lockState = CursorLockMode.None;
        private bool _visible = false;

        public CursorStateHandler(){}

        public void Set(CursorLockMode lockState, bool visible)
        {
            Store(Cursor.lockState, Cursor.visible);
            Cursor.lockState = lockState;
            Cursor.visible = visible;
        }

        private void Store(CursorLockMode lockState, bool visible)
        {
            _lockState = lockState;
            _visible = visible;
        }

        public void Restore()
        {
            Cursor.lockState = _lockState;
            Cursor.visible = _visible;
        }
    }
}