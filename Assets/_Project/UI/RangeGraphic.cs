using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeGraphic : MonoBehaviour
{
    private List<RectTransform> _cells;
    [SerializeField] private RectTransform _rangeCellPrefab;
    [SerializeField] private RectTransform _towerCellPrefab;
    [SerializeField] private RectTransform _supportCellPrefab;
    [SerializeField] private RectTransform _emptyCellPrefab;

    public void DrawRangeIndicator(TowerRangeData towerRangeData)
    {
        _cells = new List<RectTransform>();
        RectTransform parentTransform = GetComponent<RectTransform>();
        parentTransform.sizeDelta = new Vector2(towerRangeData.Width, towerRangeData.Height) * _towerCellPrefab.sizeDelta.x;
        for (int i = 0; i < towerRangeData.Width; i++)
        {
            for (int j = 0; j < towerRangeData.Height; j++)
            {
                RectTransform cell;
                switch (towerRangeData.GetCellState(i, j))
                {
                    case TowerCellState.Tower:
                        cell = Instantiate<RectTransform>(_towerCellPrefab);
                        break;
                    case TowerCellState.InRange:
                        cell = Instantiate<RectTransform>(_rangeCellPrefab);
                        break;
                    default:
                        cell = Instantiate<RectTransform>(_emptyCellPrefab);
                        break;
                }
                if (cell != null)
                {
                    cell.transform.parent = transform;
                    cell.transform.localPosition = new Vector2(i, j+1) * new Vector2(1, -1) * _towerCellPrefab.sizeDelta.x;
                    Vector3 offset = new Vector3(-parentTransform.sizeDelta.x, parentTransform.sizeDelta.y / 2, 0);
                    cell.transform.localPosition += offset;
                    cell.transform.localRotation = Quaternion.identity;
                    _cells.Add(cell);
                }
            }
        }
    }

    public void DestroyRangeIndicator()
    {
        int count = _cells.Count;
        for (int i = count - 1; i >= 0; i--)
        {
            Destroy(_cells[i].gameObject);
        }
    }
}
