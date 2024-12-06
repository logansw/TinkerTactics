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
        IsHovered = IsPointerOverUIElement();

    }

    void OnDisable()
    {
        IdleState.e_OnIdleStateEnter -= OnIdleStateEnter;
        IsHovered = false;
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

    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    //Returns 'true' if we touched or are hovering over this gameObject.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.Equals(this.gameObject))
            {
                return true;
            }
        }
        return false;
    }

    //Gets all event system raycast results of current mouse or touch position.
    private List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults;
    }
}
