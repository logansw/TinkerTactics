using Unity.Mathematics;
using UnityEngine;

public class TowerRangeData : MonoBehaviour
{
    [SerializeField] private int _width = 5;
    [SerializeField] private int _height = 5;

    [SerializeField] private TowerCellState[] grid;

    public int Width => _width;
    public int Height => _height;

    public TowerCellState[] Grid => grid;

    public TowerCellState GetCellState(int x, int y)
    {
        return grid[y * _width + x];
    }

    public void SetCellState(int x, int y, TowerCellState cellState)
    {
        grid[y * _width + x] = cellState;
    }

    private void OnValidate()
    {
        // Whenever you change width or height in the inspector, you might want
        // to resize the grid array if needed and not lose data you already had.
        if (grid == null || grid.Length != _width * _height)
        {
            grid = new TowerCellState[_width * _height];
        }
    }
}

public enum TowerCellState
{
    None,     // Not part of the tower and not in attack range
    Tower,    // The tower’s own location
    InRange   // A tile the tower can attack
}