using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHandle : MonoBehaviour, ISelectable
{
    private bool _selected;
    private bool _held;
    [SerializeField] private Transform _tower;
    [SerializeField] private Transform _wrapper;

    public void Update()
    {
        if (_selected && Input.GetMouseButton(0))
        {
            // Rotate object towards mouse cursor
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 towerPos = _tower.position;
            Vector3 direction = mousePos - towerPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _wrapper.parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void Initialize(float range)
    {
        _wrapper.localScale = new Vector3(range / transform.parent.parent.parent.localScale.x, 1, 1);
    }
    
    public void OnMouseUp()
    {
        _held = false;
    }

    public void OnSelect()
    {
        _selected = true;
        _held = true;
    }

    public void OnDeselect()
    {
        _selected = false;
        _held = false;
    }
}
