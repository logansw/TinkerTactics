using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class MarketplaceManager : Singleton<MarketplaceManager>
{
    public List<Card> AllCards;
    public List<Card> AvailableCards;

    [SerializeField] private Transform cardsContainer;
    [SerializeField] private GameObject canvas;
    public int CardsSelected;

    public void PopulateAvailableItems()
    {
        ResetCards();
        AllCards = AllCards.OrderBy(item => Guid.NewGuid()).ToList();
        RenderNewItems();
        CardsSelected = 0;
    }

    public void ShowShop(bool show)
    {
        canvas.gameObject.SetActive(show);
    }

    public void RenderNewItems()
    {
        float padding = 10f;
        List<Card> selectedCards = AllCards.Take(5).ToList();

        for (int i = 0; i < selectedCards.Count; i++)
        {
            Card instance = Instantiate(selectedCards[i], cardsContainer);
            instance.gameObject.SetActive(true);
            instance.transform.localScale = Vector3.one;
            AvailableCards.Add(instance);
            instance.IsOwned = false;
            instance.RectTransform.anchoredPosition = new Vector2((i-2) * (Card.CARD_WIDTH + padding), 0);
            instance.Render(true);
        }
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
        CardsSelected++;
        if (CardsSelected == 2)
        {
            card.Render(true);
            ShowShop(false);
            DeckRenderer.s_Instance.QueueUpdate();
            Debug.Log("Removed from available");
        }
    }
}
