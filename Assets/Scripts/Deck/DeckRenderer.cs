using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckRenderer : MonoBehaviour
{
    [SerializeField] private RectTransform _drawPileTransform;
    [SerializeField] private RectTransform _handTransform;
    [SerializeField] private RectTransform _discardPileTransform;
    [SerializeField] private RectTransform _exhaustPileTransform;
    [SerializeField] private RectTransform _databaseTransform;
    private bool _updateQueued;

    void Update()
    {
        if (_updateQueued)
        {
            _updateQueued = false;
            RenderHand(DeckManager.s_Instance.Hand);
            RenderDrawPile(DeckManager.s_Instance.DrawPile);
            RenderDiscardPile(DeckManager.s_Instance.DiscardPile);
            RenderExhaustPile(DeckManager.s_Instance.ConsumePile);
            RenderDatabase();
        }
    }

    public void RenderHand(List<Card> hand)
    {
        float cardPositionOffset = 0;
        foreach (Card card in hand)
        {
            card.gameObject.SetActive(true);
            card.transform.SetParent(_handTransform);
            card.RectTransform.anchorMin = new Vector2(0, 0.5f);
            card.RectTransform.anchorMax = new Vector2(0, 0.5f);
            card.RectTransform.pivot = new Vector2(0, 0.5f);
            card.RectTransform.anchoredPosition = new Vector2(cardPositionOffset, 0);
            cardPositionOffset += card.RectTransform.rect.width + 10;
        }
    }

    public void RenderDrawPile(List<Card> drawPile)
    {
        foreach (Card card in drawPile)
        {
            card.transform.SetParent(_drawPileTransform);
            card.gameObject.SetActive(false);
            card.transform.localPosition = Vector3.zero;
        }
    }

    public void RenderDiscardPile(List<Card> discardPile)
    {
        foreach (Card card in discardPile)
        {
            card.transform.SetParent(_discardPileTransform);
            card.gameObject.SetActive(false);
            card.transform.localPosition = Vector3.zero;
        }
    }

    public void RenderExhaustPile(List<Card> exhaustPile)
    {
        foreach (Card card in exhaustPile)
        {
            card.transform.SetParent(_exhaustPileTransform);
            card.gameObject.SetActive(false);
            card.transform.localPosition = Vector3.zero;
        }
    }

    public void RenderDatabase()
    {
        foreach (Card card in DeckManager.s_Instance.Database)
        {
            card.transform.SetParent(_databaseTransform);
            card.gameObject.SetActive(false);
            card.transform.localPosition = Vector3.zero;
        }
    }

    public void QueueUpdate()
    {
        _updateQueued = true;
    }
}