using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapModel : MonoBehaviour
{
    private Tile[,] _tiles; // (0,0) refers to the bottom left corner.
    public TilePath StartTile { get; private set; }
    public TilePath EndTile { get; private set; }

    public void LoadMap(Tile[,] tiles)
    {
        _tiles = tiles;
        StitchTiles(_tiles);
    }

    public void DeleteMap()
    {
        foreach (Tile tile in _tiles)
        {
            if (tile != null)
            {
                Destroy(tile.gameObject);
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
                    StartTile = currentTile;
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

        EndTile = currentTile;
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
}