using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapGenerator : Singleton<MapGenerator>
{
    public TilePath StartTilePath { get; private set; }
    public TilePath EndTilePath { get; private set; }
    [SerializeField] private TilePath _tilePathPrefab;
    [SerializeField] private TilePlot _tilePlotPrefab;
    [SerializeField] private Sprite _pathEndSprite;
    [SerializeField] private Sprite _pathStraightSprite;
    [SerializeField] private Sprite _pathTurnSprite;
    /*
        0- Empty Space
        S- Start
        P- Path
        E- End
        T- Tile Plot
    */
    private Tile[,] _tiles; // (0,0) refers to the bottom left corner.

    void Start()
    {
        // string mapData = @"
        // 00000
        // SPP00
        // 00P00
        // 00PPP
        // 0000E";

        string mapData = @"
        00000E0000
        0PPPTPPPP0
        TPTP00TTPT
        SP0P00PPPT
        000P0TPTTT
        00TPT0PPP0
        0PPPTT00P0
        0PTT000TP0
        0PPPPPPPP0
        000T0T0000";
        mapData = CleanMapData(mapData);
        GenerateMap(mapData);
    }

    public void GenerateMap(string mapData)
    {
        string[] mapDataLines = mapData.Split('\n');
        int r = mapDataLines.Length;
        int c = mapDataLines[0].Length;
        _tiles = new Tile[r, c];
        SpawnTiles(mapData);
        StitchTiles(_tiles);
        RenderTiles(_tiles);
    }

    private void SpawnTiles(string mapData)
    {
        string[] mapDataLines = mapData.Split('\n');

        for (int y = 0; y < mapDataLines.Length; y++)
        {
            string line = mapDataLines[mapDataLines.Length - 1 - y]; // Read lines in reverse order
            for (int x = 0; x < line.Length; x++)
            {
                Tile newTile;
                char tileType = line[x];
                Vector3 tilePosition = new Vector3(x, y, 0);

                if (tileType == 'S')
                {
                    TilePath tilePath = Instantiate(_tilePathPrefab, tilePosition, Quaternion.identity, transform);
                    tilePath.Initialize(x, y);
                    tilePath.PathType = PathType.Start;
                    StartTilePath = tilePath;
                    newTile = tilePath;
                }
                else if (tileType == 'E')
                {
                    TilePath tilePath = Instantiate(_tilePathPrefab, tilePosition, Quaternion.identity, transform);
                    tilePath.Initialize(x, y);
                    tilePath.PathType = PathType.End;
                    EndTilePath = tilePath;
                    newTile = tilePath;
                }
                else if (tileType == 'P')
                {
                    TilePath tilePath = Instantiate(_tilePathPrefab, tilePosition, Quaternion.identity, transform);
                    tilePath.Initialize(x, y);
                    tilePath.PathType = PathType.Path;
                    newTile = tilePath;
                }
                else if (tileType == 'T')
                {
                    TilePlot tilePlot = Instantiate(_tilePlotPrefab, tilePosition, Quaternion.identity, transform);
                    tilePlot.Initialize(x, y);
                    tilePlot.Initialize(true, true);
                    newTile = tilePlot;
                }
                else
                {
                    newTile = null;
                }
                if (newTile != null)
                {
                    newTile.gameObject.name = $"TilePath ({x}, {y})";
                }
                _tiles[x, y] = newTile;
            }
        }
    }

    private void StitchTiles(Tile[,] tiles)
    {
        // Find the starting tile
        TilePath currentTile = null;
        for (int y = 0; y < tiles.GetLength(1); y++)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                Tile tile = tiles[x, y];
                if (tile is TilePath tilePath && tilePath.PathType == PathType.Start)
                {
                    currentTile = tilePath;
                    break;
                }
            }
        }

        // Stitch path from Start to End
        while (currentTile != null && currentTile.PathType != PathType.End)
        {
            List<Direction> adjacentPathDirections = FindAdjacentPaths(currentTile, tiles);
            foreach (Direction direction in adjacentPathDirections)
            {
                TilePath adjacentTilePath = GetTilePathInDirection(direction, currentTile);
                if (adjacentTilePath != null && adjacentTilePath != currentTile.PreviousTilePath)
                {
                    currentTile.NextTilePath = adjacentTilePath;
                    currentTile.NextTilePath.PreviousTilePath = currentTile;
                }
            }
            currentTile = currentTile.NextTilePath;
        }
    }

    private TilePath GetTilePathInDirection(Direction direction, TilePath currentPath)
    {
        TilePath adjacentTilePath = null;
        switch (direction)
        {
            case Direction.Down:
                if (currentPath.Position.Y > 0)
                {
                    adjacentTilePath = (TilePath)_tiles[currentPath.Position.X, currentPath.Position.Y - 1];
                }
                break;
            case Direction.Left:
                if (currentPath.Position.X > 0)
                {
                    adjacentTilePath = (TilePath)_tiles[currentPath.Position.X - 1, currentPath.Position.Y];
                }
                break;
            case Direction.Up:
                if (currentPath.Position.Y < _tiles.GetLength(1) - 1)
                {
                    adjacentTilePath = (TilePath)_tiles[currentPath.Position.X, currentPath.Position.Y + 1];
                }
                break;
            case Direction.Right:
                if (currentPath.Position.X < _tiles.GetLength(0) - 1)
                {
                    adjacentTilePath = (TilePath)_tiles[currentPath.Position.X + 1, currentPath.Position.Y];
                }
                break;
        }
        return adjacentTilePath;
    }

    private List<Direction> FindAdjacentPaths(TilePath tilePath, Tile[,] tiles)
    {
        List<Direction> adjacentPathDirections = new List<Direction>();
        int x = tilePath.Position.X;
        int y = tilePath.Position.Y;
        if (x > 0 && tiles[x - 1, y] != null && tiles[x - 1, y] is TilePath)
        {
            adjacentPathDirections.Add(Direction.Left);
        }
        if (x < tiles.GetLength(0) - 1 && tiles[x + 1, y] != null && tiles[x + 1, y] is TilePath)
        {
            adjacentPathDirections.Add(Direction.Right);
        }
        if (y > 0 && tiles[x, y + 1] != null && tiles[x, y + 1] is TilePath)
        {
            adjacentPathDirections.Add(Direction.Up);
        }
        if (y < tiles.GetLength(1) - 1 && tiles[x, y - 1] != null && tiles[x, y - 1] is TilePath)
        {
            adjacentPathDirections.Add(Direction.Down);
        }

        return adjacentPathDirections;
    }

    private void RenderTiles(Tile[,] tiles)
    {
        for (int y = 0; y < tiles.GetLength(1); y++)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                Tile tile = tiles[x, y];
                if (tile != null)
                {
                    tile.Position = new MapPosition(x, y);
                    if (tile is TilePath)
                    {
                        TilePath tilePath = (TilePath)tile;
                        if (tilePath.PathType == PathType.Start)
                        {
                            tilePath.SetSprite(_pathEndSprite);
                            Direction dir = tilePath.GetNextPathDirection();
                            switch (dir)
                            {
                                case Direction.Down:
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 0);
                                    break;
                                case Direction.Up:
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 180);
                                    break;
                                case Direction.Left:
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 270);
                                    break;
                                case Direction.Right:
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 90);
                                    break;
                            }
                        }
                        else if (tilePath.PathType == PathType.End)
                        {
                            tilePath.SetSprite(_pathEndSprite);
                            Direction dir = tilePath.GetPreviousPathDirection();
                            switch (dir)
                            {
                                case Direction.Down:
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 0);
                                    break;
                                case Direction.Up:
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 180);
                                    break;
                                case Direction.Left:
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 270);
                                    break;
                                case Direction.Right:
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 90);
                                    break;   
                            }
                        }
                        else
                        {
                            if (tilePath.IsStraight())
                            {
                                tilePath.SetSprite(_pathStraightSprite);
                                Direction dir = tilePath.GetNextPathDirection();
                                if (dir == Direction.Left || dir == Direction.Right)
                                {
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 90);
                                }
                                else
                                {
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 0);
                                }
                            }
                            else if (tilePath.IsCurved())
                            {
                                tilePath.SetSprite(_pathTurnSprite);
                                Direction prevDir = tilePath.GetPreviousPathDirection();
                                Direction nextDir = tilePath.GetNextPathDirection();
                                if ((prevDir == Direction.Up || nextDir == Direction.Up) && (prevDir == Direction.Right || nextDir == Direction.Right))
                                {
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 0);
                                }
                                else if ((prevDir == Direction.Right || nextDir == Direction.Right) && (prevDir == Direction.Down || nextDir == Direction.Down))
                                {
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 270);
                                }
                                else if ((prevDir == Direction.Down || nextDir == Direction.Down) && (prevDir == Direction.Left || nextDir == Direction.Left))
                                {
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 180);
                                }
                                else if ((prevDir == Direction.Left || nextDir == Direction.Left) && (prevDir == Direction.Up || nextDir == Direction.Up))
                                {
                                    tilePath.transform.rotation = Quaternion.Euler(0, 0, 90);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private string CleanMapData(string mapData)
    {
        mapData = mapData[1..];
        return mapData.Replace(" ", "");
    }
}