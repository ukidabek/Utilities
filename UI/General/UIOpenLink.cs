using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOpenLink : MonoBehaviour
{
    [SerializeField] private string m_link = string.Empty;

    public void OpenLink() => Application.OpenURL(m_link);
}
