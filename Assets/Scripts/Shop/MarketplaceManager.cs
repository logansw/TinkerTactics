using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class MarketplaceManager : Singleton<MarketplaceManager>
{
    // Declare any variables or references here
    public Dictionary<int, List<TowerItemSO>> TowerItemsByTier;
    public TowerItemSO[][] AvailableItems; // Changed to jagged array

    private void Start()
    {
        // Initialization code goes here
        AvailableItems = new TowerItemSO[3][];
        LoadTowerItems();
        PopulateAvailableItems();
    }

    void OnEnable()
    {
        EnemyManager.e_OnWaveCleared += PopulateAvailableItems;
    }

    void OnDisable()
    {
        EnemyManager.e_OnWaveCleared -= PopulateAvailableItems;
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
        MarketplaceUI.s_Instance.ResetItemUIs();
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
            AvailableItems[tier - 1] = selectedItems.ToArray();
        }
        MarketplaceUI.s_Instance.RenderNewItems();
    }
}
