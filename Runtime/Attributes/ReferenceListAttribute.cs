using System;
using UnityEngine;

namespace Utilities.General
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ReferenceListAttribute :
#if UNITY_2023
        PropertyCollectionAttribute
#else
        PropertyAttribute
#endif
    {
#if UNITY_6000
        public ReferenceListAttribute() : base(true)
        {
        }
#endif
    }
}