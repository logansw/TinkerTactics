using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePath : Tile
{
    public PathType PathType { get; set; }
    public TilePath NextTilePath { get; set; }
    public TilePath PreviousTilePath { get; set; }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.GetComponent<Enemy>() != null)
    //     {
    //         Enemy enemy = other.GetComponent<Enemy>();
    //         if (enemy.TileTarget != null && enemy.TileTarget.PathType != PathType.End)
    //         {
    //             enemy.TileTarget = enemy.TileTarget.NextTilePath;
    //         }
    //     }
    // }

    public Direction GetNextPathDirection()
    {
        if (NextTilePath.Position.X > Position.X)
        {
            return Direction.Right;
        }
        else if (NextTilePath.Position.X < Position.X)
        {
            return Direction.Left;
        }
        else if (NextTilePath.Position.Y > Position.Y)
        {
            return Direction.Up;
        }
        else if (NextTilePath.Position.Y < Position.Y)
        {
            return Direction.Down;
        }
        else
        {
            return Direction.None;
        }
    }

    public Direction GetPreviousPathDirection()
    {
        if (PreviousTilePath.Position.X > Position.X)
        {
            return Direction.Right;
        }
        else if (PreviousTilePath.Position.X < Position.X)
        {
            return Direction.Left;
        }
        else if (PreviousTilePath.Position.Y > Position.Y)
        {
            return Direction.Up;
        }
        else if (PreviousTilePath.Position.Y < Position.Y)
        {
            return Direction.Down;
        }
        else
        {
            return Direction.None;
        }
    }

    public bool IsStraight()
    {
        Direction prevDir = GetPreviousPathDirection();
        Direction nextDir = GetNextPathDirection();
        return (prevDir == Direction.Up && nextDir == Direction.Down) || (prevDir == Direction.Down && nextDir == Direction.Up) || (prevDir == Direction.Left && nextDir == Direction.Right) || (prevDir == Direction.Right && nextDir == Direction.Left);
    }

    public bool IsCurved()
    {
        Direction prevDir = GetPreviousPathDirection();
        Direction nextDir = GetNextPathDirection();
        return (prevDir == Direction.Up && nextDir == Direction.Right) || (prevDir == Direction.Right && nextDir == Direction.Up) ||
                (prevDir == Direction.Right && nextDir == Direction.Down) || (prevDir == Direction.Down && nextDir == Direction.Right) ||
                (prevDir == Direction.Down && nextDir == Direction.Left) || (prevDir == Direction.Left && nextDir == Direction.Down) ||
                (prevDir == Direction.Left && nextDir == Direction.Up) || (prevDir == Direction.Up && nextDir == Direction.Left);
    }
}

public enum PathType
{
    Start,
    End,
    Path
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    None
}