using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Popup : MonoBehaviour
{
    [HideInInspector] public RectTransform RectTransform;
    public delegate void PopupClosedDelegate(Popup popup);
    public PopupClosedDelegate OnPopupClosed;
    public Popup ParentPopup;
    public bool IsLocked;

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void Close()
    {
        OnPopupClosed.Invoke(this);
    }
}
