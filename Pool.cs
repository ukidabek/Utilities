using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class OnDeactivateNotifier : MonoBehaviour
    {
        public event Action<GameObject> ObjectDeactivated = null;

        private void OnDisable()
        {
            ObjectDeactivated?.Invoke(gameObject);
        }
    }

    public class Pool<T> where T : Component
    {
        private class PoolData
        {
            public IList List = null;
            public Transform Parrent = null;

            public PoolData(IList list, Transform parrent)
            {
                List = list;
                Parrent = parrent;
            }
        }

        private static Dictionary<GameObject, PoolData> DictionaryOfLists = new Dictionary<GameObject, PoolData>();

        [SerializeField] private T prefab = null;
        [SerializeField] private Transform parrent = null;

        private List<T> List = new List<T>();

        public Pool(T prefab, Transform parrent = null, int initialCount = 5)
        {
            this.prefab = prefab;
            this.parrent = parrent;

            if (DictionaryOfLists.ContainsKey(prefab.gameObject))
            {
                PoolData poolData = DictionaryOfLists[prefab.gameObject];
                List = poolData.List as List<T>;
                this.parrent = poolData.Parrent;
            }
            else
            {
                if (parrent == null)
                {
                    this.parrent = new GameObject(string.Format("Pool of {0}", prefab.name)).transform;
                    GameObject.DontDestroyOnLoad(this.parrent);
                }
                List = new List<T>();
                DictionaryOfLists.Add(prefab.gameObject, new PoolData(List, this.parrent));
            }

            if (List.Count < initialCount)
                for (int i = 0; i < initialCount; i++)
                    CreateNewInstance();
        }

        private T CreateNewInstance()
        {
            T instance = GameObject.Instantiate(prefab, parrent, false);
            instance.gameObject.SetActive(false);
            List.Add(instance);
            OnDeactivateNotifier deactivateNotifier = instance.gameObject.AddComponent<OnDeactivateNotifier>();
            deactivateNotifier.ObjectDeactivated += OnObjectDeactivated;
            return instance;
        }

        public void Clear()
        {
            foreach (var item in List)
                if (item != null) GameObject.Destroy(item.gameObject);

            List.Clear();
        }

        private void OnObjectDeactivated(GameObject gameObject)
        {
            if (parrent != gameObject.transform.parent)
                gameObject.transform.SetParent(parrent);
        }

        public T Get()
        {
            T instance = null;
            for (int i = 0; i < List.Count; i++)
            {
                if (!List[i].gameObject.activeSelf)
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