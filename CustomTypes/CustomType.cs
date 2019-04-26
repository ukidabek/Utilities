using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic.CustomTypes
{
    [Serializable]
    public class CustomType
    {
        [SerializeField] private string groupName = string.Empty;
        [SerializeField] private string typeName = string.Empty;
    }
}