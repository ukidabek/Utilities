using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public class BoolScriptableEventInvoker : BaseScriptableEventInvoker<bool>
    {
        public BoolScriptableEvent Event = null;
        
        public override void Invoke(bool value) => Event?.Invoke(value);
    }
}