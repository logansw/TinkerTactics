using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathSetData : IJSONData<PathSetData>
{
    public List<PathData> Paths;
    public List<TilePlotData> TilePlots;

    public PathSetData CreateNewFile()
    {
        PathSetData data = new PathSetData();
        data.Paths = new List<PathData>();
        data.TilePlots = new List<TilePlotData>();
        return data;
    }
}

[Serializable]
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

[Serializable]
public struct PathPieceData
{
    public Vector3 Position;

    public PathPieceData(Vector3 position)
    {
        Position = position;
    }
}

[Serializable]
public struct TilePlotData
{
    public Vector3 Position;

    public TilePlotData(Vector3 position)
    {
        Position = position;
    }
}