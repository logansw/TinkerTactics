using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CompositeCollider2D))]
public class RangeIndicator : MonoBehaviour, ISelectable
{

    public List<Enemy> EnemiesInRange;
    private Tower _tower;
    private bool _isDragging;
    private float _initialAngleOffset;
    private bool _updateQueued;
    // TODO: Don't need this anymore, because everything will adhere to grid?
    [SerializeField] private BoxCollider2D _towerHitbox;    // Tower hitbox needs to rotate with the range indicator and sprites
    [SerializeField] private TowerRangeData _towerRangeData;
    private CompositeCollider2D _collider;
    private List<SpriteRenderer> _rangeCells;
    [SerializeField] private SpriteRenderer _rangeCellPrefab;
    private bool _isVisible;

    public void Initialize(Tower tower)
    {
        _tower = tower;
        _collider = GetComponent<CompositeCollider2D>();
        EnemiesInRange = new List<Enemy>();
        _rangeCells = new List<SpriteRenderer>();
        
        DrawRangeIndicator();

        PlayingState.e_OnPlayingStateEnter += EnableCollider;
        IdleState.e_OnIdleStateEnter += DisableCollider;
        IdleState.e_OnIdleStateEnter += ClearEnemiesList;
    }

    public void OnDestroy()
    {
        PlayingState.e_OnPlayingStateEnter -= EnableCollider;
        IdleState.e_OnIdleStateEnter -= DisableCollider;
        IdleState.e_OnIdleStateEnter -= ClearEnemiesList;
    }

    void Update()
    {
        if (_updateQueued)
        {
            DrawRangeIndicator();
            _updateQueued = false;
        }
        // Dragging
        if (Input.GetMouseButton(0) && (object)SelectionManager.s_Instance.CurrentSelectable == this)
        {
            Drag();
        }
        // OnMouseUp
        if (Input.GetMouseButtonUp(0) && (object)SelectionManager.s_Instance.CurrentSelectable == this)
        {
            // Calculate which 90 degree offset is the closest
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 towerPos = transform.position;
            Vector3 difference = mousePos - towerPos;

            float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            float finalAngle = angle - _initialAngleOffset;

            switch (finalAngle)
            {
                case <= 90 and > 0:
                    finalAngle = 45f;
                    break;
                case <= 0 and > -90:
                    finalAngle = -45f;
                    break;
                case <= 90 and > -180:
                    finalAngle = -135f;
                    break;
                default:
                    finalAngle = -225f;
                    break;
            }
            
            _towerHitbox.transform.rotation = Quaternion.AngleAxis(finalAngle, Vector3.forward);
            transform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
        }
    }

    public void OnSelect()
    {
        SetVisible(true);
        TooltipManager.s_Instance.DisplayTooltip(_tower.GetTooltipText());

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

    public void OnDeselect()
    {
        SetVisible(false);
        TooltipManager.s_Instance.HideTooltip();
    }

    // TODO: Change this later so that players can grab the grid and drag it to rotate the tower. It would be cool if there was some kind of cool animation
    // so that it feels like the tower is stuck in place until it kind of snaps and clicks over. Could be accomplished with some nice sound effects and
    // messing with how much the tower rotates relative to the amount the player has "pulled". 
    public bool IsSelectable()
    {
        return _isVisible;
    }

    public void SetVisible(bool visible)
    {
        foreach (SpriteRenderer rangeCell in _rangeCells)
        {
            rangeCell.enabled = visible;
        }
        _isVisible = visible;
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

    private void Drag()
    {
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
        _towerHitbox.transform.rotation = Quaternion.AngleAxis(finalAngle, Vector3.forward);
    }

    private void DrawRangeIndicator()
    {
        // Spawn RangeCell objects (cleanup previous ones if necessary)
        // Find Tower Position from grid
        Vector2 towerPosition = new Vector2(-1, -1);
        bool found = false;
        for (int i = 0; i < _towerRangeData.Width; i++)
        {
            for (int j = 0; j < _towerRangeData.Height; j++)
            {
                TowerCellState cellState = _towerRangeData.GetCellState(i, j);
                if (cellState == TowerCellState.Tower)
                {
                    towerPosition = new Vector2(i, j);
                    found = true;
                    break;
                }
            }
            if (found)
            {
                break;
            }
        }

        if (!found)
        {
            Debug.LogError("Tower position not found in TowerRangeData");
        }

        for (int i = 0; i < _towerRangeData.Width; i++)
        {
            for (int j = 0; j < _towerRangeData.Height; j++)
            {
                if (_towerRangeData.GetCellState(i, j) == TowerCellState.InRange)
                {
                    SpriteRenderer rangeCell = Instantiate<SpriteRenderer>(_rangeCellPrefab) as SpriteRenderer;
                    rangeCell.transform.parent = transform;
                    rangeCell.transform.localPosition = (new Vector2(i, j) - towerPosition) * new Vector2(1, -1);
                    rangeCell.transform.localRotation = Quaternion.identity;
                    _rangeCells.Add(rangeCell);
                }
            }
        }
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