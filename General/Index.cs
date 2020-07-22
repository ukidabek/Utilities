using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.General
{
    [Serializable] public class Index
    {
        [SerializeField] private int _current = 0;
        public int Current
        {
            get { return _current; }
            set { _current = LoopIndex(value, Count); }
        }

        private IList _linkedList = null;
        
        private int _count = 0;
        public int Count { get { return _linkedList?.Count ?? _count; } }

        public int Next { get { return LoopIndex(_current + 1, Count); } }

        public int Previous { get { return LoopIndex(_current - 1, Count); } }

        public int Last { get { return Count - 1; } }

        public bool IsLast { get { return _current == Count - 1; } }

        public Index() { }

        public Index(int count)
        {
            _count = count;
        }

        public Index(int current, int count) : this(count)
        {
            _current = current < 0 ? 0 : current > count ? count - 1 : current;
        }

        public Index(IList list)
        {
            _linkedList = list;
        }

        public Index(int current, IList list) : this(current, list.Count)
        {
            _linkedList = list;
        }

        private void LoopIndex()
        {
            _current = LoopIndex(_current, Count);
        }

        private static int LoopIndex(int current, int count)
        {
            return (current + count) % count;
        }

        public static implicit operator int(Index i)
        {
            return i.Current;
        }

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

        public int GetOffsetIndex(int offset)
        {
            return LoopIndex(_current + offset, Count);
        }
    }
}
