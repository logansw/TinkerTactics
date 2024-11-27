using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnTray : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsHovered;
    public static ReturnTray s_Instance;

    void Awake()
    {
        s_Instance = this;
    }

    void OnEnable()
    {
        IdleState.e_OnIdleStateEnter += OnIdleStateEnter;
    }

    void OnDisable()
    {
        IdleState.e_OnIdleStateEnter -= OnIdleStateEnter;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHovered = false;
    }

    private void OnIdleStateEnter()
    {
        gameObject.SetActive(false);
    }
}
