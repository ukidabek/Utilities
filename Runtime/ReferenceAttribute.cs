using System;
using UnityEngine;

namespace Utilities.General
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ReferenceAttribute : PropertyAttribute
    {
    }
}