using UnityEngine;

namespace Utilities.General.Reference.Core
{
    public abstract class SimpleReferenceHostSetter<ReferenceHostType, Type> : ReferenceHostSetter<ReferenceHostType, Type>
        where ReferenceHostType : ReferenceHost<Type>
        where Type : Object
    {
        [SerializeField] private Type m_reference = default;
        protected override Type Reference => m_reference;

        protected virtual void Reset()
        {
            m_reference = GetComponent<Type>();
        }
    }
}