using UnityEngine;

namespace Utilities.General.Pool
{
	/// <summary>
	/// ScriptableObject wrapper for UnityEngine.Pool.ObjectPool;
	/// </summary>
	/// <typeparam name="T">Type</typeparam>
	public abstract class ObjectPool<T> : ScriptableObject where T : Object
	{
		[SerializeField] protected T m_object;
		[SerializeField] protected bool m_collectionCheck = true;
		[SerializeField] protected int m_defaultCapacity = 10;
		[SerializeField] protected int m_maxSize = 10000;

		protected UnityEngine.Pool.ObjectPool<T> m_pool;

		protected ObjectPool()
		{
			m_pool = new UnityEngine.Pool.ObjectPool<T>(
				OnPoolCreate,
				OnPoolGet,
				OnPoolRelease,
				OnPoolDestroy,
				m_collectionCheck,
				m_defaultCapacity,
				m_maxSize);
		}

		protected virtual T OnPoolCreate() => GameObject.Instantiate(m_object);

		protected virtual void OnPoolGet(T character) { }

		protected virtual void OnPoolRelease(T character) { }

		protected virtual void OnPoolDestroy(T character) => GameObject.Destroy(character);

		public T Get() => m_pool.Get();

		public void Release(T character) => m_pool.Release(character);
	}
}