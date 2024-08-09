using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AugmentSelector : MonoBehaviour
{
    [SerializeField] private int MAX_AUGMENTS;
    private int _padding = 10;
    private List<AugmentSO> _augmentList;
    [SerializeField] private AugmentCard _augmentCardPrefab;
    [SerializeField] private Canvas _canvas;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _augmentList = new List<AugmentSO>(Resources.LoadAll<AugmentSO>("Augments"));
        for (int i = 0; i < MAX_AUGMENTS; i++)
        {
            AugmentCard augmentCard = Instantiate(_augmentCardPrefab, _canvas.transform);
            augmentCard.RectTransform.anchoredPosition = new Vector2((i-1) * (augmentCard.RectTransform.rect.width + _padding), 0);
            augmentCard.Initialize(_augmentList[i], this);
        }
    }

    public AugmentSO GetRandomAugment()
    {
        return _augmentList[Random.Range(0, _augmentList.Count)];
    }

    public void ShowAugments(bool show)
    {
        _canvas.gameObject.SetActive(show);
    }

    public void ToggleAugments()
    {
        _canvas.gameObject.SetActive(!_canvas.gameObject.activeSelf);
    }
}