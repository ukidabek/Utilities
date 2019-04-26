using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace BaseGameLogic.Singleton
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        public static T Instance { get; protected set; }

        [SerializeField] private bool _dontDestroyOnLoad = false;

        protected virtual void CreateInstance()
        {
            if (Instance == null)
                Instance = this as T;
            else
            {
                Debug.LogErrorFormat("GameObject {0} will be destroyed.", gameObject.name);
                Destroy(gameObject);
            }

            if(Instance != null && _dontDestroyOnLoad)
                DontDestroyOnLoad(this.gameObject);
        }

        protected virtual void Awake()
        {
            CreateInstance();
        }

        protected virtual void Start() {}

        protected virtual void Reset()
        {
            gameObject.name = GetType().Name;
        }
    }
}