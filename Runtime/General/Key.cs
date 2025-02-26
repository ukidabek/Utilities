using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.General
{
    [CreateAssetMenu(fileName = "NewKey", menuName = "Utilities/Key")]
    public class Key : ScriptableObject, IEquatable<Key>
    {
        private static Dictionary<int, Key> m_registeredKeys = new Dictionary<int, Key>();
        
        [SerializeField, HideInInspector] private int m_hash = default;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Clear() => m_registeredKeys = new Dictionary<int, Key>();

        private void OnEnable()
        {
            if(m_registeredKeys.ContainsKey(m_hash)) return;
            m_registeredKeys.Add(m_hash, this);
        }

        public static Key GetKey(int hash) => m_registeredKeys.GetValueOrDefault(hash);

        private void OnValidate() => m_hash = HashFunctions.FNVHash(name);

        private void Reset() => m_hash = HashFunctions.FNVHash(name);

        public static bool operator ==(Key a, Key b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.m_hash == b.m_hash;
        }

        public static bool operator !=(Key a, Key b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.m_hash != b.m_hash;
        }

        public bool Equals(Key other)
        {
            if (other is null) return false;
            return m_hash == other.m_hash;
        }

        public override bool Equals(object obj) => obj is Key other && Equals(other);

        public override int GetHashCode() => m_hash;
    }
}