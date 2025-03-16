namespace Utilities.General
{
    public static class ReferencePropertyDroverHelper
    {
        public const float Margin =
#if UNITY_6000
            10f;
#else
            0f;
#endif
        
    }
}