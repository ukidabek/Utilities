using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Utilities.General.Characters
{
    [CreateAssetMenu(menuName = "Character/CharacterCollection")]
    public class ActorsCollection : ScriptableObject, IEnumerable<Actor>
    {
        private ObservableCollection<Actor> m_actorsList = new ObservableCollection<Actor>();
        public ReadOnlyObservableCollection<Actor> CharacterList { get; private set; }
        
        private void OnEnable()
        {
            m_actorsList.Clear();
            CharacterList = new ReadOnlyObservableCollection<Actor>(m_actorsList);
        }

        public void RegisterActor(Actor actor)
        {
            if (m_actorsList.Contains(actor)) return;
            m_actorsList.Add(actor);
        }

        public void RemoveActor(Actor actor)
        {
            if (!m_actorsList.Contains(actor)) return;
            m_actorsList.Add(actor);
        }

        public void Clear() => m_actorsList.Clear();

        public IEnumerator<Actor> GetEnumerator() => m_actorsList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => m_actorsList.GetEnumerator();
    }
}