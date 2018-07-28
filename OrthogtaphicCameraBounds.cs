using System;

namespace BaseGameLogic.Utilities
{
    [Serializable] public class OrthogtaphicCameraBounds
    {
        public float MinHeight = 0f;
        public float MaxHeight = 0f;
        public float MinWidth = 0f;
        public float MaxWidth = 0f;

        public OrthogtaphicCameraBounds() {}

        public OrthogtaphicCameraBounds(float minHeight, float maxHeight, float minWidth, float maxWidth)
        {
            MinHeight = minHeight;
            MaxHeight = maxHeight;
            MinWidth = minWidth;
            MaxWidth = maxWidth;
        }
    }
}
