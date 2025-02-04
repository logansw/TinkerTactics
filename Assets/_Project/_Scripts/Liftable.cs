using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D)), RequireComponent(typeof(ILiftable))]
public class Liftable : MonoBehaviour
{
    private Collider2D _collider;
    private ILiftable _liftable;
    public bool IsLifted;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _liftable = GetComponent<ILiftable>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (CheckClick(mousePos))
        {
            _liftable.OnLift();
            IsLifted = true;
        }
        if (CheckRelease())
        {
            _liftable.OnDrop();
            IsLifted = false;
        }
        if (CheckHover(mousePos))
        {
            _liftable.OnHover();
        }
        if (IsLifted)
        {
            _liftable.OnHeld();
            MoveObject();
        }
    }

    private bool CheckClick(Vector2 mousePos)
    {
        if (StateController.CurrentState.Equals(StateType.Playing)) { return false; }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider == this._collider)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool CheckRelease()
    {
        return Input.GetMouseButtonUp(0) && IsLifted;
    }

    private bool CheckHover(Vector2 mousePos)
    {
        if (IsLifted) { return false; }

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == this._collider)
            {
                return true;
            }
        }
        return false;
    }

    private void MoveObject()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }
}
