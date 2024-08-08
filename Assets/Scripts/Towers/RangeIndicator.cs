using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(LineRenderer)), RequireComponent(typeof(PolygonCollider2D))]
public class RangeIndicator : MonoBehaviour
{
    public List<Enemy> EnemiesInRange;
    private Tower _tower;
    private LineRenderer _rangeIndicator;
    private PolygonCollider2D _collider;
    private bool _isDragging;
    private float _initialAngleOffset;
    public bool HasEnemyInRange => EnemiesInRange.Count > 0;

    public void Initialize(Tower tower)
    {
        _rangeIndicator = GetComponent<LineRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
        _tower = tower;
        DrawRangeIndicator(tower.Attack);
        DrawCollider(tower.Attack);
        EnemiesInRange = new List<Enemy>();
    }

    void Update()
    {
        DrawRangeIndicator(_tower.Attack);
        DrawCollider(_tower.Attack);
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
        if (!_isDragging) { return; }
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 towerPos = transform.position;
        Vector3 difference = mousePos - towerPos;

        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float finalAngle = angle - _initialAngleOffset;
        
        transform.rotation = Quaternion.AngleAxis(finalAngle, Vector3.forward);
    }

    private void DrawRangeIndicator(IAbility ability)
    {
        _rangeIndicator.useWorldSpace = false;
        Vector3[] points = GetRangePoints(ability);
        _rangeIndicator.positionCount = points.Length;
        _rangeIndicator.SetPositions(points);
    }

    private void DrawCollider(IAbility ability)
    {
        Vector3[] points = GetRangePoints(ability);
        Vector2[] colliderPoints = new Vector2[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            colliderPoints[i] = points[i];
        }
        _collider.points = colliderPoints;
    }

    private Vector3[] GetRangePoints(IAbility ability)
    {
        int arcPointCount = 30;
        Vector3[] points = new Vector3[arcPointCount + 2];
        // First Point
        points[0] = Vector2.zero;
        float angle = ability.Sweep * Mathf.PI / 180f;
        float startAngle = -angle / 2f;
        float angleDelta = angle / (arcPointCount - 1);
        for (int i = 0; i < arcPointCount; i++)
        {
            float x = ability.Range * Mathf.Cos(startAngle + angleDelta * i);
            float y = ability.Range * Mathf.Sin(startAngle + angleDelta * i);
            points[i+1] = new Vector2(x, y);
        }
        // Last Point
        points[arcPointCount+1] = Vector2.zero;
        return points;
    }

    public List<Enemy> GetEnemiesInRange()
    {
        return TargetCalculator.GetClosest(EnemiesInRange, _tower);
    }
}
