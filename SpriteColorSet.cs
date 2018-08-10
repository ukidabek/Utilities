using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable] public class SpriteColorSet
{
    [SerializeField] private SpriteRenderer _graphic = null;
    [SerializeField] private Color _a = new Color();

    public void SetColor()
    {
        _graphic.color = _a;
    }
}
