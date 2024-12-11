using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class MarketplaceManager : Singleton<MarketplaceManager>
{
    public List<Card> AllTowers;
    public List<Card> AllTinkers;
    public List<Card> AvailableCards;

    [SerializeField] private Transform cardsContainer;
    [SerializeField] private GameObject canvas;
    private int _cardsSelected;
    private int _cardsToSelect;
    private int _cardOptionsCount;

    public void PopulateAvailableItems()
    {
        ResetCards();
        AllTowers = AllTowers.OrderBy(item => Guid.NewGuid()).ToList();
        _cardsSelected = 0;
        if (GameManager.s_Instance.CurrentLevelIndex % 2 == 0)
        {
            PrepareTowerShop();
        }
        else
        {
            PrepareTinkerShop();
        }
    }

    public void ShowShop(bool show)
    {
        canvas.gameObject.SetActive(show);
    }

    public void RenderNewItems(List<Card> chooseFromList)
    {
        float padding = 10f;
        List<Card> selectedCards = chooseFromList.Take(_cardOptionsCount).ToList();

        for (int i = 0; i < selectedCards.Count; i++)
        {
            Card instance = Instantiate(selectedCards[i], cardsContainer);
            instance.gameObject.SetActive(true);
            instance.transform.localScale = Vector3.one;
            AvailableCards.Add(instance);
            instance.IsOwned = false;
            instance.RectTransform.anchoredPosition = new Vector2((i-2) * (Card.CARD_WIDTH + padding), 0);
            instance.OnDrawn();
        }
    }

    private void PrepareTowerShop()
    {
        _cardOptionsCount = 3;
        _cardsToSelect = 1;
        RenderNewItems(AllTowers);
    }

    private void PrepareTinkerShop()
    {
        _cardOptionsCount = 5;
        _cardsToSelect = 2;
        RenderNewItems(AllTinkers);
    }

    public void ResetCards()
    {
        for (int i = AvailableCards.Count - 1; i >= 0; i--)
        {
            Destroy(AvailableCards[i].gameObject);
        }
        AvailableCards.Clear();
    }

    public void RemoveFromAvailable(Card card)
    {
        AvailableCards.Remove(card);
        card.Render(false);
        _cardsSelected++;
        if (_cardsSelected == _cardsToSelect)
        {
            card.Render(true);
            ShowShop(false);
            DeckRenderer.s_Instance.QueueUpdate();
        }
    }
}