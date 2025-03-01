using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Utilities.General
{
    public class TypeProvider : ScriptableObject, ISearchWindowProvider
    {
        public SerializedProperty Property { get; set; }
        private readonly List<SearchTreeEntry> m_searchTreeEntry = new List<SearchTreeEntry>();
            
        public void GenerateTreeEntries(Type baseType)
        {
            m_searchTreeEntry.Clear();
            m_searchTreeEntry.Add(new SearchTreeGroupEntry(new GUIContent(baseType.Name)));

            bool TypeValidateLogic(Type type)
            {
                if (type.IsAbstract) return false;
                if (type.IsInterface) return false;
                if (type.IsSubclassOf(typeof(UnityEngine.Object))) return false;
                if (!type.GetCustomAttributes(true).OfType<SerializableAttribute>().Any()) return false;
                if (baseType.IsInterface)
                {
                    if (!baseType.IsAssignableFrom(type)) return false;
                }
                else
                {
                    if (!type.IsSubclassOf(baseType) && !baseType.IsAssignableFrom(type)) return false;
                }
                 
                return true;
            }
                
            var selectedTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(TypeValidateLogic);
                
            foreach (var type in selectedTypes)
                m_searchTreeEntry.Add(new SearchTreeEntry(new GUIContent(type.Name))
                {
                    level = 1,
                    userData = type,
                });
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context) => m_searchTreeEntry;

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            try
            {
                var Type = SearchTreeEntry.userData as Type;
                var instance = Activator.CreateInstance(Type);
                if (Property.isArray)
                {
                    var index = Property.arraySize++;
                    var newElement = Property.GetArrayElementAtIndex(index);
                    newElement.managedReferenceValue = instance;
                }
                else
                {
                    Property.managedReferenceValue = instance;
                }
                Property.serializedObject.ApplyModifiedProperties();
                Property.serializedObject.UpdateIfRequiredOrScript();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
                
            return true;
        }
    }
}