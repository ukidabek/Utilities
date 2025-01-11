using System.Collections;

namespace Utilities.General
{
	public class Index
	{
		private int m_current = 0;
		public int Current
		{
			get { return m_current; }
			set { m_current = LoopIndex(value, Count); }
		}

		private IList m_linkedList = null;

		private int m_count = 0;
		public int Count => m_linkedList?.Count ?? m_count;

		public int Next => LoopIndex(m_current + 1, Count);

		public int Previous => LoopIndex(m_current - 1, Count);

		public int Last => Count - 1;

		public bool IsLast => m_current == Count - 1;

		public Index() { }

		public Index(int count)
		{
			m_count = count;
		}

		public Index(int current, int count) : this(count)
		{
			m_current = current < 0 ? 0 : current > count ? count - 1 : current;
		}

		public Index(IList list)
		{
			m_linkedList = list;
		}

		public Index(int current, IList list) : this(current, list.Count)
		{
			m_linkedList = list;
		}

		private void LoopIndex() => m_current = LoopIndex(m_current, Count);


		public static implicit operator int(Index i) => i.Current;

		public static Index operator ++(Index i)
		{
			i.Current++;
			i.LoopIndex();
			return i;
		}

		public static Index operator --(Index i)
		{
			i.Current--;
			i.LoopIndex();
			return i;
		}

		public static Index operator +(Index i, int x)
		{
			i.Current += x;
			return i;
		}

		public static Index operator -(Index i, int x)
		{
			i.Current -= x;
			return i;
		}

		public int GetOffsetIndex(int offset) => LoopIndex(m_current + offset, Count);

		private static int LoopIndex(int current, int count) => (current + count) % count;
	}
}
