using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MarketplaceUI : Singleton<MarketplaceUI>
{
    public ItemUI itemUIPrefab;
    public Transform itemContainer;
    [SerializeField] private Canvas canvas;
    private List<ItemUI> itemUIs = new List<ItemUI>();

    public void ShowShop(bool show)
    {
        canvas.gameObject.SetActive(show);
    }

    public void RenderNewItems()
    {
        int row = 0;
        int column = 0;
        float itemWidth = itemUIPrefab.GetComponent<RectTransform>().rect.width;
        float itemHeight = itemUIPrefab.GetComponent<RectTransform>().rect.height;
        float padding = 10f;

        for (int i = 0; i < MarketplaceManager.s_Instance.AvailableItems.Length; i++)
        {
            for (int j = 0; j < MarketplaceManager.s_Instance.AvailableItems[0].Length; j++)
            {
                TowerItemSO itemData = MarketplaceManager.s_Instance.AvailableItems[i][j];
                if (itemData == null)
                {
                    continue;
                }
                ItemUI itemUI = Instantiate(itemUIPrefab, itemContainer);
                itemUI.gameObject.transform.parent = canvas.transform;
                itemUIs.Add(itemUI);
                RectTransform itemRectTransform = itemUI.GetComponent<RectTransform>();
                itemRectTransform.anchoredPosition = new Vector2((column-1) * (itemWidth + padding), (row-1) * (itemHeight + padding));

                itemUI.PopulateFields(itemData);

                column++;
                if (column >= 3)
                {
                    column = 0;
                    row++;
                }
            }
        }
    }

    public void ResetItemUIs()
    {
        foreach (ItemUI itemUI in itemUIs)
        {
            Destroy(itemUI.gameObject);
        }
        itemUIs.Clear();
    }
}