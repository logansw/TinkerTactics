using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TowerRangeData))]
public class TowerRangeDataEditor : Editor
{
    private TowerRangeData towerData;

    // The size of each cell in our grid drawing, in pixels.
    private const float cellSize = 20f;

    private void OnEnable()
    {
        towerData = (TowerRangeData)target;
    }

    public override void OnInspectorGUI()
    {
        // Draw the default inspector for width/height
        EditorGUI.BeginChangeCheck();
        int newWidth = EditorGUILayout.IntField("Width", towerData.Width);
        int newHeight = EditorGUILayout.IntField("Height", towerData.Height);
        if (EditorGUI.EndChangeCheck())
        {
            // If width or height changed, apply them
            Undo.RecordObject(towerData, "Change Grid Size");
            // We need to directly set the serialized fields or handle them
            var serializedObjectRef = new SerializedObject(towerData);
            serializedObjectRef.FindProperty("_width").intValue = newWidth;
            serializedObjectRef.FindProperty("_height").intValue = newHeight;
            serializedObjectRef.ApplyModifiedProperties();

            // Force OnValidate() to resize the grid as needed
            EditorUtility.SetDirty(towerData);
        }

        // Draw the 2D grid in the Inspector
        DrawGridEditor();
    }

    private void DrawGridEditor()
    {
        TowerRangeData towerData = (TowerRangeData)target;
        int width  = towerData.Width;
        int height = towerData.Height;

        var grid = towerData.Grid;
        if (grid == null) return;

        // We'll store some style for labels
        GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter
        };

        for (int y = 0; y < height; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < width; x++)
            {
                // Reserve layout space for one cell
                Rect cellRect = EditorGUILayout.GetControlRect(
                    false, cellSize, GUILayout.Width(cellSize), GUILayout.Height(cellSize));
                
                // Draw background color based on cell state
                TowerCellState cellState = towerData.GetCellState(x, y);
                Color backgroundColor;
                string labelText = "";
                switch (cellState)
                {
                    case TowerCellState.None:
                        backgroundColor = Color.white;
                        labelText       = "";
                        break;
                    case TowerCellState.Tower:
                        backgroundColor = Color.yellow;
                        labelText       = "T";
                        break;
                    case TowerCellState.InRange:
                        backgroundColor = Color.green;
                        labelText       = "R";
                        break;
                    default:
                        backgroundColor = Color.gray; // fallback
                        labelText       = "?";
                        break;
                }

                // Draw the background rect
                EditorGUI.DrawRect(cellRect, backgroundColor);
                // Draw the label on top
                GUI.Label(cellRect, labelText, labelStyle);

                // Handle left-/right-click
                Event e = Event.current;
                if (e.type == EventType.MouseDown && cellRect.Contains(e.mousePosition))
                {
                    if (e.button == 0) // left-click
                    {
                        OnLeftClick(towerData, x, y);
                        e.Use(); // consume the click event
                    }
                    else if (e.button == 1) // right-click
                    {
                        OnRightClick(towerData, x, y);
                        e.Use(); // consume the click event
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void OnLeftClick(TowerRangeData towerData, int x, int y)
    {
        Undo.RecordObject(towerData, "Left Click - InRange Toggle");
        // Example logic: toggle the cell as InRange vs None
        if (towerData.GetCellState(x, y) == TowerCellState.InRange)
            towerData.SetCellState(x, y, TowerCellState.None);
        else
            towerData.SetCellState(x, y, TowerCellState.InRange);

        EditorUtility.SetDirty(towerData);
    }

    private void OnRightClick(TowerRangeData towerData, int x, int y)
    {
        Undo.RecordObject(towerData, "Right Click - Set Tower");
        // Example logic: remove any existing tower, then set current cell to Tower
        for (int i = 0; i < towerData.Width; i++)
        {
            for (int j = 0; j < towerData.Height; j++)
            {
                if (towerData.GetCellState(i, j) == TowerCellState.Tower)
                {
                    towerData.SetCellState(i, j, TowerCellState.None);
                }
            }
        }
        towerData.SetCellState(x, y, TowerCellState.Tower);
        EditorUtility.SetDirty(towerData);
    }
}
