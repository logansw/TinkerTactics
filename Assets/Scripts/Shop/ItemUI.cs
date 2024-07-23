using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ItemUI : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text Description;
    public TMP_Text YellowCost;
    public GameObject Border;
    private TowerItemSO item;

    public void PopulateFields(TowerItemSO item)
    {
        this.item = item;
        Name.text = item.Name;
        Description.text = item.Description;
        YellowCost.text = item.YellowCost.ToString();
    }

    public void Purchase()
    {
        if (item.CanAfford())
        {
            item.Purchase();
            gameObject.SetActive(false);
        }
    }
}