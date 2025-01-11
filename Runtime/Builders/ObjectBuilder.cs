using System;

namespace Utilities.General.Builde
{
	public abstract class ObjectBuilder<T> where T : UnityEngine.Object
	{
		protected class BuilderSegmetn
		{
			protected BuilderSegmetn m_next = null;

			protected Action<T> m_processAction = null;

			public BuilderSegmetn(Action<T> processLogic)
			{
				m_processAction = processLogic;
			}

			public BuilderSegmetn Process(T instance)
			{
				m_processAction(instance);
				return m_next;
			}

			public BuilderSegmetn SetNext(BuilderSegmetn builderSegmetn)
				=> m_next = builderSegmetn;
		}

		protected T m_characterPrefab = default;
		protected T m_instance = default;

		protected BuilderSegmetn m_root = null;
		protected BuilderSegmetn m_last = null;

		public ObjectBuilder(T prefab)
		{
			m_characterPrefab = prefab;

			m_last = m_root = new BuilderSegmetn(prefab => m_instance = UnityEngine.Object.Instantiate(prefab));
		}


		protected void SetNext(BuilderSegmetn builderSegmetn) => m_last = m_last.SetNext(builderSegmetn);

		public T Build()
		{
			var segment = m_root;
			m_instance = m_characterPrefab;
			while (segment != null)
			{
				segment = segment.Process(m_instance);
			}
			return m_instance;
		}
	}
}