using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectable
{
    public const int CARD_WIDTH = 150;
    public const int CARD_HEIGHT = 225;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _cardType;
    private CardEffect _cardEffect;
    public CardEffect CardEffect
    {
        get
        {
            if (_cardEffect == null)
            {
                _cardEffect = GetComponent<CardEffect>();
            }
            return _cardEffect;
        }
        private set
        {
            _cardEffect = value;
        }
    }

    public RectTransform RectTransform;
    private bool _isDragging;
    private TargetPreview _targetPreview;
    public int OriginalSiblingIndex;
    public bool IsOwned;

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        RectTransform.sizeDelta = new Vector2(CARD_WIDTH, CARD_HEIGHT);
    }

    void OnEnable()
    {
        IdleState.e_OnIdleStateEnter += OnDeselect;
    }

    void OnDisable()
    {
        IdleState.e_OnIdleStateEnter -= OnDeselect;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        transform.position = new Vector2(transform.position.x, transform.position.y + (RectTransform.rect.width / 4f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - (RectTransform.rect.width / 4f));
        transform.SetSiblingIndex(OriginalSiblingIndex);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // TODO: Add this item (tower or tinker) to the player's ownership
        AddToDeck();
        MarketplaceManager.s_Instance.RemoveFromAvailable(this);
    }

    public void Render(bool active)
    {
        _background.enabled = active;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(active);
        }
        _name.text = CardEffect.GetName();
        _description.text = CardEffect.GetDescription();
        _cost.text = CardEffect.GetCost().ToString();
        _cardType.text = CardEffect.GetCardType();
        _background.color = CardEffect.GetColor();
    }

    public void Discard()
    {
        Debug.LogError("Not implemented yet");
    }

    public void Consume()
    {
        Debug.LogError("Not implemented yet");
    }

    public void OnDrawn()
    {
        CardEffect.Initialize(this);
        CardEffect.OnDrawn();
        Render(true);
        transform.localScale = Vector3.one;
    }


    // TODO: This is only here so that the camera panning works properly. This should be changed later. Currently, some systems interact with the mouse through the
    // selectable interface, but others have their own ways of working. These should be unified. 
    public void OnSelect()
    {
        // Do Nothing
    }

    public void OnDeselect()
    {

    }

    public bool IsSelectable()
    {
        return true;
    }

    private void AddToDeck()
    {
        // TODO: Actually add it properly.
        Debug.LogError("Not implemented yet");
        IsOwned = true;
    }
}