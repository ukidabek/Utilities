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

        public Index(int current, int count)
        {
            _current = current < 0 ? 0 : current > count ? count - 1 : current;
            Count = count;
        }

        public Index(int count)
        {
            Count = count;
        }

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
