using UnityEngine;
using UnityEngine.UI;

public class MarketplaceUI : Singleton<MarketplaceUI>
{
    public ItemUI itemUIPrefab;
    public Transform itemContainer;
    [SerializeField] private Canvas canvas;

    public void ShowShop(bool show)
    {
        canvas.gameObject.SetActive(show);
    }

    public void UpdateAvailableItems()
    {
        int row = 0;
        int column = 0;
        float itemWidth = itemUIPrefab.GetComponent<RectTransform>().rect.width;
        float itemHeight = itemUIPrefab.GetComponent<RectTransform>().rect.height;
        float padding = 10f;

        foreach (TowerItemSO itemData in MarketplaceManager.s_Instance.AvailableItems)
        {
            if (itemData == null)
            {
                continue;
            }
            ItemUI itemUI = Instantiate(itemUIPrefab, itemContainer);
            itemUI.gameObject.transform.parent = canvas.transform;
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