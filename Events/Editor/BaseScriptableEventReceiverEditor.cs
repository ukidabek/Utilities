using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Utilities.Events
{
    [CustomEditor(typeof(BaseScriptableEventReceiver), true)]
    public class BaseScriptableEventReceiverEditor : UnityEditor.Editor
    {
        private BaseScriptableEventReceiver eventReciver;

        private void Awake()
        {
            eventReciver = target as BaseScriptableEventReceiver;
        }

        public override void OnInspectorGUI()
        {
            var oldColor = GUI.backgroundColor;
            GUI.backgroundColor = eventReciver.Event.LogColor;
            base.OnInspectorGUI();
            GUI.backgroundColor = oldColor;
        }
    }
}