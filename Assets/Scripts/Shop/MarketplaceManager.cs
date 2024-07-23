using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class MarketplaceManager : Singleton<MarketplaceManager>
{
    public TowerItemSO[] AvailableItems;
    List<TowerItemSO> towerItems;

    private void Start()
    {
        // Initialization code goes here
        AvailableItems = new TowerItemSO[3];
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
        TowerItemSO[] towerItemObjects = Resources.LoadAll<TowerItemSO>("TowerItems/_Prod");

        // Convert the array to a list
        towerItems = new List<TowerItemSO>(towerItemObjects);
    }

    private void PopulateAvailableItems()
    {
        MarketplaceUI.s_Instance.ResetItemUIs();

        towerItems = towerItems.OrderBy(item => Guid.NewGuid()).ToList();

        // Take the first 3 tower items from the shuffled list
        List<TowerItemSO> selectedItems = towerItems.Take(3).ToList();
        for (int i = 0; i < AvailableItems.Length; i++)
        {
            AvailableItems[i] = selectedItems[i];
        }
        MarketplaceUI.s_Instance.RenderNewItems();
    }
}
