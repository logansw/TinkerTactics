using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    public static PathDrawer s_Instance;
    public string FileName;
    [SerializeField] private TilePath _tilePathPrefab;

    public PathSetData PathSetData;
    public List<TilePath> CurrentPath = new List<TilePath>();
    public List<TilePath> AllTiles = new List<TilePath>();
    public List<TilePath> StartTiles = new List<TilePath>();
    public int StartTilesIndex = 0;

    void Awake()
    {
        s_Instance = this;
    }

    void Start()
    {
        PathSetData = new PathSetData();
        PathSetData.Paths = new List<PathData>();
        LoadPathData();
        foreach (WaveSpawner waveSpawner in WaveSpawner.s_WaveSpawners)
        {
            waveSpawner.SpawnPoint = StartTiles[StartTilesIndex];
            StartTilesIndex++;
        }
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
                    path.PathType = PathType.Start;
                    StartTiles.Add(path);
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
            AllTiles.Add(path);
            path.transform.position = new Vector3(path.transform.position.x, path.transform.position.y, 0);
            if (CurrentPath.Count == 0)
            {
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