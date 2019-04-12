using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic.Utilities
{
    public class Pool<T> where T : Component
    {
        [SerializeField] private T prefab = null;
        [SerializeField] private Transform parrent = null;
        [SerializeField] private int maxCount = -1;

        private List<T> List = new List<T>();

        public Pool(T prefab, Transform parrent, int initialCount = 5, int maxCount = -1)
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
            List.Add(instance);
            return instance;
        }

        public T Get()
        {
            T instance = null;
            for (int i = 0; i < List.Count; i++)
            {
                if (!instance.gameObject.activeSelf)
                {
                    instance = List[i];
                    List.RemoveAt(i);
                    List.Add(instance);
                    break;
                }
            }

            return instance == null ? CreateNewInstance() : instance;
        }
    }
}