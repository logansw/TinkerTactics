using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionManager : Singleton<SelectionManager>
{
    private bool _isSelecting;
    public bool IsSelecting
    {
        get { return _currentSelectable != null; }
        set { _isSelecting = value;}
    }
    private ISelectable _currentSelectable;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] raycastHits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            bool selectableFound = false;
            foreach (RaycastHit2D rayHit in raycastHits)
            {
                if (rayHit.collider == null || rayHit.collider.gameObject.GetComponent<ISelectable>() == null)
                {
                    continue;
                }
                ISelectable newSelectable = rayHit.collider.gameObject.GetComponent<ISelectable>();
                if (!newSelectable.IsSelectable())
                {
                    continue;
                }
                selectableFound = true;
                DeselectCurrentSelectable();
                SelectNew(newSelectable);
            }
            if (!selectableFound)
            {
                DeselectCurrentSelectable();
                return;
            }
        }
    }

    private void DeselectCurrentSelectable()
    {
        if (_currentSelectable != null)
        {
            _currentSelectable.OnDeselect();
            _currentSelectable = null;
        }
    }

    private void SelectNew(ISelectable newSelectable)
    {
        newSelectable.OnSelect();
        _currentSelectable = newSelectable;
    }
}
