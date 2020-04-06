using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public class SpriteScriptableEventInvoker : BaseScriptableEventInvoker<Sprite>
    {
        [SerializeField] private SpriteScriptableEvent m_event = null;

        public override void Invoke(Sprite value) => m_event?.Invoke(value);
    }
}