using System.Collections;
using System.Collections.Generic;
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
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (rayHit.collider == null || rayHit.collider.gameObject.GetComponent<ISelectable>() == null) {
                DeselectCurrentSelectable();
                return;
            }

            ISelectable newSelectable = rayHit.collider.gameObject.GetComponent<ISelectable>();
            DeselectCurrentSelectable();
            SelectNew(newSelectable);
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
