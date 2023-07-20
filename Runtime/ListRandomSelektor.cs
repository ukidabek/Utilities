using System.Collections;
using System.Collections.Generic;

namespace Utilities.General
{
    public class ListRandomSelektor
    {
        private IList _list = null;

        public ListRandomSelektor(IList list)
        {
            _list = list;
        }

        public object Select()
        {
            return _list[UnityEngine.Random.Range(0, _list.Count)];
        }
    }
}