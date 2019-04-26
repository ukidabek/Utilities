using BaseGameLogic.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic.CustomTypes
{
    [CreateAssetMenu(fileName = "TypesManager", menuName = "Custom types/Type manager")]
    public class TypesManager : SingletonScriptableObject<TypesManager>
    {
        [Serializable]
        public class Group
        {
            [SerializeField] private string name = "New group";
            public string Name { get => name; }

            [SerializeField] private List<string> types = new List<string>();
            public List<string> Types { get => types; }
        }

        public List<string> GetGroups()
        {
            List<string> groups = new List<string>();
            foreach (var item in this.groups)
                groups.Add(item.Name);
            return groups;
        }

        [SerializeField] private List<Group> groups = new List<Group>();
        public List<Group> Groups { get => groups;}

        protected override void Initialize() { }

        public List<string> GetTypes(string value)
        {
            foreach (var item in groups)
                if (item.Name == value)
                    return item.Types;
            return null;
        }
    }
}