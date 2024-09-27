using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathSetData : IJSONData<PathSetData>
{
    public List<PathData> Paths;

    public PathSetData CreateNewFile()
    {
        PathSetData data = new PathSetData();
        data.Paths = new List<PathData>();
        return data;
    }
}

[System.Serializable]
public struct PathData
{
    public List<PathPieceData> Path;

    public PathData(List<TilePath> tilePahts)
    {
        Path = new List<PathPieceData>();
        foreach (TilePath tilePath in tilePahts)
        {
            Path.Add(new PathPieceData(tilePath.transform.position));
        }
    }
}

[System.Serializable]
public struct PathPieceData
{
    public Vector3 Position;

    public PathPieceData(Vector3 position)
    {
        Position = position;
    }
}