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
        // Iterate through each tier
        foreach (int tier in TowerItemsByTier.Keys)
        {
            // Get the tower items for the current tier
            List<TowerItemSO> towerItems = TowerItemsByTier[tier];

            // Shuffle the tower items randomly
            towerItems = towerItems.OrderBy(item => Guid.NewGuid()).ToList();

            // Take the first 3 tower items from the shuffled list
            List<TowerItemSO> selectedItems = towerItems.Take(3).ToList();

            // Populate the AvailableItems array with the selected items
            for (int i = 0; i < selectedItems.Count; i++)
            {
                AvailableItems[tier - 1, i] = selectedItems[i];
            }
        }
    }

    public void RemoveItemFromAvailableItems(TowerItemSO item)
    {
        // Find the item in the AvailableItems array
        for (int i = 0; i < AvailableItems.GetLength(0); i++)
        {
            for (int j = 0; j < AvailableItems.GetLength(1); j++)
            {
                if (AvailableItems[i, j] == item)
                {
                    // Remove the item from the AvailableItems array
                    AvailableItems[i, j] = null;
                    return;
                }
            }
        }
        MarketplaceUI.s_Instance.UpdateAvailableItems();
    }
}