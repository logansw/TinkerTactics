using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer)), RequireComponent(typeof(PolygonCollider2D))]
public class RangeIndicator : MonoBehaviour, ISelectable
{
    public List<Enemy> EnemiesInRange;
    private Tower _tower;
    private LineRenderer _rangeIndicator;
    private PolygonCollider2D _collider;
    private bool _isDragging;
    private float _initialAngleOffset;
    private bool _updateQueued;

    public void Initialize(Tower tower)
    {
        _rangeIndicator = GetComponent<LineRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
        _tower = tower;
        _tower.Range.e_OnStatChanged += QueueUpdate;
        _tower.Sweep.e_OnStatChanged += QueueUpdate;
        EnemiesInRange = new List<Enemy>();
        DrawRangeIndicator();
        DrawCollider();
        PlayingState.e_OnPlayingStateEnter += EnableCollider;
        IdleState.e_OnIdleStateEnter += DisableCollider;
        IdleState.e_OnIdleStateEnter += ClearEnemiesList;
    }

    public void OnDestroy()
    {
        _tower.Range.e_OnStatChanged -= QueueUpdate;
        _tower.Sweep.e_OnStatChanged -= QueueUpdate;
        PlayingState.e_OnPlayingStateEnter -= EnableCollider;
        IdleState.e_OnIdleStateEnter -= DisableCollider;
        IdleState.e_OnIdleStateEnter -= ClearEnemiesList;
    }

    void Update()
    {
        if (_updateQueued)
        {
            DrawRangeIndicator();
            DrawCollider();
            _updateQueued = false;
        }    
    }

    public void OnSelect()
    {
        SetVisible(true);
        TooltipManager.s_Instance.DisplayTooltip(_tower.GetTooltipText());
    }

    public void OnDeselect()
    {
        SetVisible(false);
        TooltipManager.s_Instance.HideTooltip();
    }

    public bool IsSelectable()
    {
        return _rangeIndicator.enabled;
    }

    public void SetVisible(bool visible)
    {
        if (StateController.CurrentState.Equals(StateType.Playing))
        {
            _collider.enabled = true;
        }
        else
        {
            _collider.enabled = visible;
        }
        _rangeIndicator.enabled = visible;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            EnemiesInRange.Add(other.GetComponent<Enemy>());
            other.GetComponent<Enemy>().e_OnEnemyDeath += RemoveEnemyFromList;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            RemoveEnemyFromList(other.GetComponent<Enemy>());
        }
    }

    private void RemoveEnemyFromList(Enemy enemy)
    {
        EnemiesInRange.Remove(enemy);
    }

    private void OnMouseDown()
    {
        if (StateController.CurrentState.Equals(StateType.Playing)) { return; }

        // Calculate and store the initial angle offset when the mouse is first clicked
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 towerPos = transform.position;
        Vector3 difference = mousePos - towerPos;

        float initialAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.z;
        
        _initialAngleOffset = Mathf.DeltaAngle(currentAngle, initialAngle);
        _isDragging = true;
    }

    private void OnMouseDrag() {
        if (StateController.CurrentState.Equals(StateType.Playing))
        {
            ToastManager.s_Instance.AddToast("Cannot rotate towers during battle");
            return;
        }
        if (!_isDragging) { return; }
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 towerPos = transform.position;
        Vector3 difference = mousePos - towerPos;

        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float finalAngle = angle - _initialAngleOffset;
        
        transform.rotation = Quaternion.AngleAxis(finalAngle, Vector3.forward);
    }

    private void DrawRangeIndicator()
    {
        _rangeIndicator.useWorldSpace = false;
        Vector3[] points = GetRangePoints();
        _rangeIndicator.positionCount = points.Length;
        _rangeIndicator.SetPositions(points);
    }

    private void DrawCollider()
    {
        Vector3[] points = GetRangePoints();
        Vector2[] colliderPoints = new Vector2[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            colliderPoints[i] = points[i];
        }
        _collider.points = colliderPoints;
    }

    private Vector3[] GetRangePoints()
    {
        int arcPointCount = 30;
        Vector3[] points = new Vector3[arcPointCount + 2];
        // First Point
        points[0] = Vector2.zero;
        float angle = _tower.Sweep.CalculatedFinal * Mathf.PI / 180f;
        float startAngle = -angle / 2f;
        float angleDelta = angle / (arcPointCount - 1);
        for (int i = 0; i < arcPointCount; i++)
        {
            float x = _tower.Range.CalculatedFinal * Mathf.Cos(startAngle + angleDelta * i);
            float y = _tower.Range.CalculatedFinal * Mathf.Sin(startAngle + angleDelta * i);
            points[i+1] = new Vector2(x, y);
        }
        // Last Point
        points[arcPointCount+1] = Vector2.zero;
        return points;
    }

    public List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange = TargetCalculator.GetMostTraveled(EnemiesInRange);
        return TargetCalculator.GetTargetable(enemiesInRange);
    }

    public bool HasEnemyInRange()
    {
        return GetEnemiesInRange().Count > 0;
    }

    private void QueueUpdate()
    {
        _updateQueued = true;
    }

    private void EnableCollider()
    {
        _collider.enabled = true;
    }
    
    private void DisableCollider()
    {
        _collider.enabled = false;
    }

    private void ClearEnemiesList()
    {
        foreach (Enemy enemy in EnemiesInRange)
        {
            if (enemy == null)
            {
                Debug.LogError("Enemy in range indicator is null");
            }
        }
        EnemiesInRange.Clear();
    }
}