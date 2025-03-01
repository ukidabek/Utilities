using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Profiling;

namespace Utilities.General
{
    public class TypeProvider : ScriptableObject, ISearchWindowProvider
    {
        private static Type[] Types = null;
        public SerializedProperty Property { get; set; }
        private readonly List<SearchTreeEntry> m_searchTreeEntry = new List<SearchTreeEntry>();
            
        public void GenerateTreeEntries(Type baseType)
        {
            m_searchTreeEntry.Clear();
            m_searchTreeEntry.Add(new SearchTreeGroupEntry(new GUIContent(baseType.Name)));

            bool TypeValidateLogic(Type type)
            {
                if (baseType.IsInterface)
                    return baseType.IsAssignableFrom(type);

                return type.IsSubclassOf(baseType) && !baseType.IsAssignableFrom(type);
            }
            
            CacheTypesIfNecessary();
            
            Profiler.BeginSample($"({nameof(TypeProvider)}) - Generate Tree Entries");
            
            var selectedTypes = Types.Where(TypeValidateLogic);
                
            foreach (var type in selectedTypes)
                m_searchTreeEntry.Add(new SearchTreeEntry(new GUIContent(type.Name))
                {
                    level = 1,
                    userData = type,
                });
            Profiler.EndSample();
        }

        private static void CacheTypesIfNecessary()
        {
            if (Types != null) return;
            Profiler.BeginSample($"({nameof(TypeProvider)}) - Cache Types");
            Types = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !type.IsAbstract && 
                               !type.IsInterface && 
                               !type.IsSubclassOf(typeof(UnityEngine.Object)) &&
                               type.GetCustomAttributes(true).OfType<SerializableAttribute>().Any())
                .ToArray();
            Profiler.EndSample();
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