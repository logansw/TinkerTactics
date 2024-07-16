using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class MarketplaceManager : Singleton<MarketplaceManager>
{
    // Declare any variables or references here
    public Dictionary<int, List<TowerItemSO>> TowerItemsByTier;
    public TowerItemSO[,] AvailableItems;

    private void Start()
    {
        // Initialization code goes here
        LoadTowerItems();
        AvailableItems = new TowerItemSO[3, 3];
        PopulateAvailableItems();
        MarketplaceUI.s_Instance.UpdateAvailableItems();
    }

    private void LoadTowerItems()
    {
        // Get all TowerItemSO objects from a folder
        TowerItemSO[] towerItemObjects = Resources.LoadAll<TowerItemSO>("TowerItems");

        // Convert the array to a list
        List<TowerItemSO> towerItems = new List<TowerItemSO>(towerItemObjects);

        TowerItemsByTier = towerItems.GroupBy(item => item.Tier)
                                     .ToDictionary(group => group.Key, group => group.ToList());
    }

    private void PopulateAvailableItems()
    {
        // Test run to fill up the first element of the matrix
        AvailableItems[0, 0] = TowerItemsByTier[1][0];
    }
}