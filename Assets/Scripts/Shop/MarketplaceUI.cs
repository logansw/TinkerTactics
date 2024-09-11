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
        float itemWidth = itemUIPrefab.GetComponent<RectTransform>().rect.width;
        float padding = 10f;

        for (int i = 0; i < MarketplaceManager.s_Instance.AvailableItems.Length; i++)
        {
            TowerItemSO itemData = MarketplaceManager.s_Instance.AvailableItems[i];
            if (itemData == null)
            {
                continue;
            }
            ItemUI itemUI = Instantiate(itemUIPrefab, itemContainer);
            itemUI.gameObject.transform.SetParent(canvas.transform);
            itemUIs.Add(itemUI);
            RectTransform itemRectTransform = itemUI.GetComponent<RectTransform>();
            itemRectTransform.anchoredPosition = new Vector2((i-1) * (itemWidth + padding), 0);

            itemUI.PopulateFields(itemData);
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