using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Utilities.General.Animation.Editor
{
	[CustomEditor(typeof(AnimatorParameterDefinition))]
    public class AnimatorParameterDefinitionEditor : UnityEditor.Editor
    {
        private SerializedProperty _animatorFieldInfo = null;
        private SerializedProperty _nameFieldInfo = null;
        private SerializedProperty _hashFieldInfo = null;

        private int _selectedParameterIndex = -1;
        
        private void OnEnable()
        {
            _animatorFieldInfo = serializedObject.FindProperty("_animator");
            _nameFieldInfo = serializedObject.FindProperty("_name");
            _hashFieldInfo = serializedObject.FindProperty("_hash");
        }

        public override void OnInspectorGUI()
        {
			base.OnInspectorGUI();
			GUILayout.Space(EditorGUIUtility.singleLineHeight);
			var animator = _animatorFieldInfo.objectReferenceValue as AnimatorController;
            
            EditorGUI.BeginChangeCheck();
            {
                animator = EditorGUILayout.ObjectField("Animator Controller:",
                    animator, typeof(AnimatorController),
                    false) as AnimatorController;
            
                _animatorFieldInfo.objectReferenceValue = animator;
                if (animator != null)
                {
                    var name = _nameFieldInfo.stringValue;
                    var parametersNames = animator.parameters
                        .Select(parameter => parameter.name)
                        .ToList();

                    _selectedParameterIndex = parametersNames.IndexOf(name);
                    if (_selectedParameterIndex < 0)
                        _selectedParameterIndex = 0;

                    _selectedParameterIndex = EditorGUILayout.Popup(_selectedParameterIndex, parametersNames.ToArray());

                    var selectedName = parametersNames[_selectedParameterIndex];
                    var generatedHash = Animator.StringToHash(selectedName);
                    _nameFieldInfo.stringValue = selectedName;
                    _hashFieldInfo.intValue = generatedHash;

                    GUILayout.Label($"Name:     {selectedName}");
                    GUILayout.Label($"Hash:     {generatedHash}");
                }
                else
                {
                    var name = _nameFieldInfo.stringValue;
                    name = EditorGUILayout.TextField("Name:", name);
                    var generatedHash = Animator.StringToHash(name);

                    _nameFieldInfo.stringValue = name;
                    _hashFieldInfo.intValue = generatedHash;
                    GUILayout.Label($"Hash:     {generatedHash}");
                }
            }
            
            if (!EditorGUI.EndChangeCheck()) return;
            EditorUtility.SetDirty(target);
            serializedObject.SetIsDifferentCacheDirty();
            serializedObject.ApplyModifiedProperties();
        }
    }
}