using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.General
{
    public class Latch
    {
        private bool _isLatch = true;

        public Latch(bool isLatch = true)
        {
            _isLatch = isLatch;
        }

        public void Set()
        {
            _isLatch = false;
        }

        public void Reset()
        {
            _isLatch = true;
        }

        public static implicit operator bool (Latch l)
        {
            var stats = l._isLatch;
            l.Set();
            return stats;
        }
    }
}
