using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.General
{
    [Serializable]
    public class Pool<T> where T : Component
    {
        [SerializeField] private T prefab = null;
        [SerializeField] private Transform parrent = null;
        [SerializeField] private int maxCount = -1;

        private List<T> list = new List<T>();

        public Func<T, bool> ValidateIfPoolElementInactive = null;
        public Action<T> OnPoolElementSelected = null;
        public Action<T> OnPoolElementCreated = null;
        public Action<T> OnPoolElementDisabled = null;

        public List<T> ActiveObjectList => list
            .Where(component => !ValidateIfPoolElementInactive(component))
            .ToList();
        
        private Pool()
        {
            ValidateIfPoolElementInactive = component => !component.gameObject.activeSelf;
        }
        
        public Pool(T prefab, Transform parrent, int initialCount = 5, int maxCount = -1) : this()
        {
            this.prefab = prefab;
            this.parrent = parrent;
            this.maxCount = maxCount;

            for (int i = 0; i < initialCount; i++)
                CreateNewInstance();
        }

        private T CreateNewInstance()
        {
            T instance = GameObject.Instantiate(prefab, parrent, false);
            instance.gameObject.SetActive(false);
            list.Add(instance);
            OnPoolElementCreated?.Invoke(instance);
            return instance;
        }

        public T Get()
        {
            T instance = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (ValidateIfPoolElementInactive.Invoke(list[i]))
                {
                    instance = list[i];
                    list.RemoveAt(i);
                    list.Add(instance);
                    break;
                }
            }
            instance = instance == null ? CreateNewInstance() : instance;
            OnPoolElementSelected?.Invoke(instance);
            return instance;
        }

        public void DeactivateAllObjects()
        {
            foreach (var component in list)
            {
                OnPoolElementDisabled?.Invoke(component);
                component.gameObject.SetActive(false);
            }
        }
    }
}