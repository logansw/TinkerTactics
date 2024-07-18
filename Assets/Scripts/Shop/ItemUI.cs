using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ItemUI : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text Description;
    public TMP_Text RedCost;
    public TMP_Text BlueCost;
    public TMP_Text YellowCost;
    public GameObject Border;
    private TowerItemSO item;

    public void PopulateFields(TowerItemSO item)
    {
        this.item = item;
        Name.text = item.Name;
        Description.text = item.Description;
        RedCost.text = item.RedCost.ToString();
        BlueCost.text = item.BlueCost.ToString();
        YellowCost.text = item.YellowCost.ToString();

        Color borderColor;
        if (item.RedProduction > 0)
        {
            borderColor = Color.red;
        } else if (item.YellowProduction > 0)
        {
            borderColor = Color.yellow;
        } else if (item.BlueProduction > 0)
        {
            borderColor = Color.blue;
        } else
        {
            borderColor = Color.white;
        }
        Image[] childImages = Border.GetComponentsInChildren<Image>();
        foreach (Image childImage in childImages)
        {
            childImage.color = borderColor;
        }
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