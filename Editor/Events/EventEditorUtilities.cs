using System.Reflection;
using UnityEditor;

namespace Utilities.General.Events.Core
{
    internal static class EventEditorUtilities
    {
        public const string Color_Field_Name = "m_color";
        public const string Listeners_Field_Name = "m_listeners";
        public const string Invoke_Method_Name = "Invoke";
        public const string Event_Property_Name = "Event";
        
        public static readonly BindingFlags Binding_Flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy;
    }
}