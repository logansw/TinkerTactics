using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    public string FileName;
    [SerializeField] private TilePath _tilePathPrefab;
    public static TilePath s_PathStart;

    public PathSetData PathSetData;
    public List<TilePath> CurrentPath = new List<TilePath>();
    public List<TilePath> AllTiles = new List<TilePath>();

    void Start()
    {
        PathSetData = new PathSetData();
        PathSetData.Paths = new List<PathData>();
        LoadPathData();
    }

    public void LoadPathData()
    {
        PathSetData = JSONTool.ReadData<PathSetData>(FileName);
        foreach (PathData pathData in PathSetData.Paths)
        {
            foreach (PathPieceData pathPieceData in pathData.Path)
            {
                TilePath path = Instantiate(_tilePathPrefab, pathPieceData.Position, Quaternion.identity);
                AllTiles.Add(path);
                path.transform.position = new Vector3(path.transform.position.x, path.transform.position.y, 0);
                if (CurrentPath.Count == 0)
                {
                    s_PathStart = path;
                    path.PathType = PathType.Start;
                }
                else
                {
                    CurrentPath[^1].NextTilePath = path;
                    path.PathType = PathType.Path;
                }
                CurrentPath.Add(path);
            }
            CurrentPath[^1].PathType = PathType.End;
            CurrentPath = new List<TilePath>();
        }
    }

    #if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TilePath path = Instantiate(_tilePathPrefab, position, Quaternion.identity);
            path.transform.position = new Vector3(path.transform.position.x, path.transform.position.y, 0);
            if (CurrentPath.Count == 0)
            {
                s_PathStart = path;
                path.PathType = PathType.Start;
            }
            else
            {
                CurrentPath[^1].NextTilePath = path;
                path.PathType = PathType.Path;
            }
            CurrentPath.Add(path);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (CurrentPath.Count > 0)
            {
                CurrentPath[^1].PathType = PathType.End;
                PathData newPath = new PathData(CurrentPath);
                PathSetData.Paths.Add(newPath);
                CurrentPath = new List<TilePath>();
                Debug.Log("Path Recorded");
            }
            else
            {
                JSONTool.WriteData(PathSetData, FileName);
                Debug.Log($"{FileName} Saved");
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            for (int i = AllTiles.Count - 1; i >= 0; i--)
            {
                TilePath path = AllTiles[i];
                Destroy(path.gameObject);
            }
            AllTiles = new List<TilePath>();
            PathSetData = new PathSetData();
            PathSetData.Paths = new List<PathData>();
        }
    }
    #endif
}