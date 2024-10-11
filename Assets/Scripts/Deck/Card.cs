using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Image _background;
    private CardEffect CardEffect;
    public RectTransform RectTransform;
    private bool _isDragging;
    private TargetPreview _targetPreview;

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        CardEffect = GetComponent<CardEffect>();
    }

    protected virtual void Start()
    {
        Render(true);
        CardEffect.Initialize(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _background.color = new Color(0.9f, 0.9f, 0.9f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _background.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!CardEffect.CanPrepare())
        {
            Debug.Log("Cannot play card right now.");
            return;
        }

        _isDragging = true;

        _targetPreview = CardEffect.GetTargetPreview();
        Render(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isDragging)
        {
            TryCast(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            _isDragging = false;
            Destroy(_targetPreview.gameObject);
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
    }

    public void Discard()
    {
        Destroy(gameObject);
    }
}