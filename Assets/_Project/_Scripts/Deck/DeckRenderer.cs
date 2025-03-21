using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckRenderer : Singleton<DeckRenderer>
{
    [SerializeField] private RectTransform _drawPileTransform;
    [SerializeField] private RectTransform _handTransform;
    [SerializeField] private RectTransform _discardPileTransform;
    // [SerializeField] private RectTransform _exhaustPileTransform;
    // [SerializeField] private RectTransform _databaseTransform;
    [SerializeField] private ReturnTray _returnTray;
    private bool _updateQueued;

    void Update()
    {
        if (_updateQueued)
        {
            _updateQueued = false;
            RenderHand(DeckManager.s_Instance.Hand);
            RenderDiscardPile(DeckManager.s_Instance.DiscardPile);
            // RenderDatabase();
        }
    }

    public void RenderHand(List<Card> hand)
    {
        float cardPositionOffset = 0;
        float offset = _handTransform.rect.width / hand.Count;
        foreach (Card card in hand)
        {
            card.gameObject.SetActive(true);
            card.transform.SetParent(_handTransform);
            card.RectTransform.anchorMin = new Vector2(0, 0.5f);
            card.RectTransform.anchorMax = new Vector2(0, 0.5f);
            card.RectTransform.pivot = new Vector2(0, 0.5f);
            card.RectTransform.anchoredPosition = new Vector2(cardPositionOffset, 0);
            card.RectTransform.localScale = Vector3.one;
            cardPositionOffset += offset;
            card.OriginalSiblingIndex = card.transform.GetSiblingIndex();
            card.Render(true);
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

    // public void RenderDatabase()
    // {
    //     foreach (Card card in DeckManager.s_Instance.Database)
    //     {
    //         card.transform.SetParent(_databaseTransform);
    //         card.gameObject.SetActive(false);
    //         card.transform.localPosition = Vector3.zero;
    //     }
    // }

    public void QueueUpdate()
    {
        _updateQueued = true;
    }

    public void ShowReturnTray(bool show)
    {
        _returnTray.gameObject.SetActive(show);
        _returnTray.transform.SetAsLastSibling();
    }
}