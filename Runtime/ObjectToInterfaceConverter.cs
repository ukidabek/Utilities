using System;
using UnityEngine;

namespace Utilities.General
{
	[Serializable]
	public class ObjectToInterfaceConverter<T> where T : class
	{
		[SerializeField] private UnityEngine.Object m_object;
		private T m_instance = default(T);

		private bool m_converted = false;

		public static implicit operator T(ObjectToInterfaceConverter<T> convertyer)
		{
			if (convertyer.m_converted == false)
			{
 				convertyer.m_converted = convertyer.m_object is T;
				convertyer.m_instance = convertyer.m_object as T;
			}

			return convertyer.m_instance;
		}
	}
}
