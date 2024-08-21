using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AugmentCard : MonoBehaviour
{
    [HideInInspector] public RectTransform RectTransform;
    private TMP_Text _text;
    private AugmentSelector _augmentSelector;
    
    public void Initialize(AugmentSO augment, AugmentSelector augmentSelector)
    {
        _text = GetComponentInChildren<TMP_Text>();
        _text.text = augment.Description;
        _augmentSelector = augmentSelector;
    }

    public void Reroll()
    {
        Initialize(_augmentSelector.GetRandomAugment(), _augmentSelector);
    }
}