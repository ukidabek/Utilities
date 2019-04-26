using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic.Singleton
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    (_instance = Resources.Load(typeof(T).Name) as T).Initialize();

                return _instance;
            }
        }

        protected abstract void Initialize();
    }
}