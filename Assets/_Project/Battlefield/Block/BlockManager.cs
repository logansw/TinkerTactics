using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class BlockManager : Singleton<BlockManager>
{
    public const float TILE_PLOT_PERCENTAGE = 0.25f;
    [SerializeField] private Block _blockPrefab;
    private Block[,] _blocks = new Block[9,9];

    public override void Initialize()
    {
        base.Initialize();
        InstantiateBlocks();
    }

    public void ClearBlocks()
    {
        for (int i = 0; i < _blocks.GetLength(0); i++)
        {
            for (int j = 0; j < _blocks.GetLength(1); j++)
            {
                Destroy(_blocks[i,j].gameObject);
            }
        }
    }

    public void InstantiateBlocks()
    {
        int m = _blocks.GetLength(0);
        int n = _blocks.GetLength(1);
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Block block = Instantiate(_blockPrefab, transform);
                block.transform.localPosition = new Vector2(i, j) - new Vector2((m-1)/2f, (n-1)/2f);
                _blocks[i,j] = block;
            }
        }

        transform.localRotation = Quaternion.AngleAxis(45f, Vector3.back);

        CalculateHeights();
        CalculateShadows();
    }

    private void CalculateHeights()
    {
        int m = _blocks.GetLength(0);
        int n = _blocks.GetLength(1);
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Block block = _blocks[i,j];
                TilePath closestPath = TilePath.GetClosest(block.gameObject);
                if (closestPath == null)
                {
                    float randomHeight = UnityEngine.Random.Range(0f, 1f);
                    if (randomHeight < TILE_PLOT_PERCENTAGE)
                    {
                        block.Initialize(2);
                        TilePlot tilePlot = block.AddComponent<TilePlot>();
                        tilePlot.IsActivated = true;
                    }
                    else
                    {
                        block.Initialize(0);
                    }
                }
                else
                {
                    block.Initialize(1);
                }
            }
        }
    }

    private void CalculateShadows()
    {
        for (int i = 0; i < _blocks.GetLength(0); i++)
        {
            for (int j = 0; j < _blocks.GetLength(1); j++)
            {
                int westHeight = i > 0 ? _blocks[i-1,j].Height : -1;
                int northHeight = j < _blocks.GetLength(1) - 1 ? _blocks[i,j+1].Height : -1;
                int northwestHeight = (i > 0 && j < _blocks.GetLength(1) - 1) ? _blocks[i-1,j+1].Height : - 1;
                _blocks[i,j].CalculateShadows(westHeight, northHeight, northwestHeight);
            }
        }
    }
}
