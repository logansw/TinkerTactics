using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Renders the map model to the screen.
/// </summary>
public class MapView : MonoBehaviour
{
    [SerializeField] private TilePath _tilePathPrefab;
    [SerializeField] private TilePlot _tilePlotPrefab;
    [SerializeField] private Sprite _pathEndSprite;
    [SerializeField] private Sprite _pathStraightSprite;
    [SerializeField] private Sprite _pathTurnSprite;

    void OnEnable()
    {
        MapManager.e_OnMapUpdated += RenderMap;
    }

    void OnDisable()
    {
        MapManager.e_OnMapUpdated -= RenderMap;
    }
    
    public Tile[,] SpawnTiles(string mapData)
    {
        string[] mapDataLines = mapData.Split('\n');
        Tile[,] tiles = new Tile[mapDataLines[0].Length, mapDataLines.Length];

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
                    newTile = tilePath;
                }
                else if (tileType == 'E')
                {
                    TilePath tilePath = Instantiate(_tilePathPrefab, tilePosition, Quaternion.identity, transform);
                    tilePath.Initialize(x, y);
                    tilePath.PathType = PathType.End;
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
                tiles[x, y] = newTile;
            }
        }

        return tiles;
    }

    private void RenderMap(Tile[,] tiles)
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
}