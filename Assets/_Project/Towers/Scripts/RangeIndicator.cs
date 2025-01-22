using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CompositeCollider2D)), RequireComponent(typeof(TowerRangeData))]
public class RangeIndicator : MonoBehaviour, ISelectable
{

    public List<Enemy> EnemiesInRange;
    private Tower _tower;
    private bool _isDragging;
    private float _initialAngleOffset;
    private bool _updateQueued;
    // TODO: Don't need this anymore, because everything will adhere to grid?
    [SerializeField] private BoxCollider2D _towerHitbox;    // Tower hitbox needs to rotate with the range indicator and sprites
    private TowerRangeData _towerRangeData;
    private CompositeCollider2D _collider;
    private List<GameObject> _rangeCells;
    [SerializeField] private GameObject _rangeCellPrefab;

    public void Initialize(Tower tower)
    {
        _tower = tower;
        _collider = GetComponent<CompositeCollider2D>();
        _towerRangeData = GetComponent<TowerRangeData>();
        EnemiesInRange = new List<Enemy>();
        _rangeCells = new List<GameObject>();
        
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

    // TODO: Change this later so that players can grab the grid and drag it to rotate the tower. It would be cool if there was some kind of cool animation
    // so that it feels like the tower is stuck in place until it kind of snaps and clicks over. Could be accomplished with some nice sound effects and
    // messing with how much the tower rotates relative to the amount the player has "pulled". 
    public bool IsSelectable()
    {
        return false;
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
        foreach (GameObject rangeCell in _rangeCells)
        {
            rangeCell.SetActive(visible);
        }
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

    // TODO: Change to snap to 90 degree increments
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

        // Make sure they are positioned properly
        for (int i = 0; i < _towerRangeData.Width; i++)
        {
            for (int j = 0; j < _towerRangeData.Height; j++)
            {
                if (_towerRangeData.GetCellState(i, j) == TowerCellState.InRange)
                {
                    Debug.Log("Cell");
                    GameObject rangeCell = Instantiate(_rangeCellPrefab);
                    rangeCell.transform.parent = transform;
                    rangeCell.transform.localPosition = (new Vector2(i, j) - towerPosition) * new Vector2(1, -1);
                    _rangeCells.Add(rangeCell);
                }
            }
        }

        // Stitch together CompositeCollider2D using these boxes

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