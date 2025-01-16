using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private Block _blockPrefab;
    private Block[,] _blocks = new Block[5,5];

    void Start()
    {
       InstantiateBlocks();
       CalculateShadows();
    }

    private void InstantiateBlocks()
    {
        for (int i = 0; i < _blocks.GetLength(0); i++)
        {
            for (int j = 0; j < _blocks.GetLength(1); j++)
            {
                Block block = Instantiate(_blockPrefab, new Vector2(i, j), Quaternion.identity, transform);
                _blocks[i,j] = block;
                block.Initialize(UnityEngine.Random.Range(0, 3));
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
