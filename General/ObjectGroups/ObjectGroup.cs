using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.General.ObjectGroups
{
    [CreateAssetMenu(menuName = "ObjectGroup")]
    public class ObjectGroup : ScriptableObject
    {
        private readonly Dictionary<string, GameObject> m_objectGroupDictionary = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, List<GameObjectPuller>> m_ObjectPullerDictionary = new Dictionary<string, List<GameObjectPuller>>();

        public void PushObject(GameObjectPusher pusher, bool overrideObject = true)
        {
            if (overrideObject && m_objectGroupDictionary.ContainsValue(pusher.ObjectToPush))
            {
                string key = m_objectGroupDictionary.FirstOrDefault(pair => pair.Value == pusher.ObjectToPush).Key;
                m_objectGroupDictionary.Remove(key);
            }
            m_objectGroupDictionary.Add(pusher.Key, pusher.ObjectToPush);
            if (m_ObjectPullerDictionary.TryGetValue(pusher.Key, out var pullersList))
            {
                pullersList.ForEach(puller => puller.Set(pusher.ObjectToPush));
                m_ObjectPullerDictionary.Remove(pusher.Key);
            }
        }

        public void PullObject(GameObjectPuller puller)
        {
            if (m_objectGroupDictionary.TryGetValue(puller.Key, out var gameObject))
            {
                puller.Set(gameObject);
            }
            else
            {
                if (m_ObjectPullerDictionary.TryGetValue(puller.Key, out var pullersList))
                    pullersList.Add(puller);
                else
                    m_ObjectPullerDictionary.Add(puller.Key, new List<GameObjectPuller>(new[] {puller}));
            }
        }
    }
}