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
        if (IsOwned)
        {
            if (!CardEffect.CanPrepare())
            {
                ToastManager.s_Instance.AddToast("Not enough energy");
                return;
            }

            DeckManager.s_Instance.ShowReturnTray(true);
            CardEffect.OnCardClicked();
            _isDragging = true;

            _targetPreview = CardEffect.GetTargetPreview();
            Render(false);
        }
        else
        {
            // Do Nothing
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        DeckManager.s_Instance.ShowReturnTray(false);
        if (IsOwned)
        {
            if (_isDragging)
            {
                TryCast(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                _isDragging = false;
                Destroy(_targetPreview.gameObject);
            }
            Render(true);
        }
        else
        {
            AddToDeck();
            MarketplaceManager.s_Instance.RemoveFromAvailable(this);
        }
    }

    public bool TryCast(Vector3 targetPosition)
    {
        if (CardEffect.CanCast(targetPosition))
        {
            CardEffect.Cast();
            return true;
        }

        Render(true);
        return false;
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
        DeckManager.s_Instance.Discard(this);
    }

    public void Consume()
    {
        DeckManager.s_Instance.Consume(this);
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
        // Do Nothing
    }

    public bool IsSelectable()
    {
        return true;
    }

    private void AddToDeck()
    {
        DeckManager.s_Instance.AddCardToDeck(this);
        IsOwned = true;
    }
}