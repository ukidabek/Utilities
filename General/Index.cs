using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic.Utilities
{
    [Serializable] public class Index
    {
        [SerializeField] private int _current = 0;
        public int Count = 0;

        public Index() {}

        public Index(int count)
        {
            Count = count;
        }

        public Index(int current, int count) : this(count)
        {
            _current = current < 0 ? 0 : current > count ? count - 1 : current;
        }

        public Index(IList list) : this (list.Count) {}

        public Index(int current, IList list) : this(current, list.Count) {}

        private void LoopIndex()
        {
            _current = (_current + Count) % Count;
        }

        public static implicit operator int(Index i)
        {
            return i._current;
        }

        public static Index operator ++(Index i)
        {
            i._current++;
            i.LoopIndex();
            return i;
        }

        public static Index operator --(Index i)
        {
            i._current--;
            i.LoopIndex();
            return i;
        }
    }
}
