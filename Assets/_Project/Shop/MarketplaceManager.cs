using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class MarketplaceManager : Singleton<MarketplaceManager>
{
    public List<Card> AllCards;
    public List<Card> AvailableCards;

    [SerializeField] private Transform cardsContainer;
    private int _cardsSelected;
    [SerializeField] private int _cardsToSelect;
    [SerializeField] private int _cardOptionsCount;

    public override void Initialize()
    {
        base.Initialize();
        PopulateAvailableItems();
    }

    public void CloseShop()
    {
        SceneLoader.s_Instance.LoadScene(SceneType.Map);
    }

    public void PopulateAvailableItems()
    {
        ResetItems();
        AllCards = AllCards.OrderBy(item => Guid.NewGuid()).ToList();
        RenderNewItems(AllCards);
    }

    public void RenderNewItems(List<Card> chooseFromList)
    {
        float padding = 10f;
        // Randomize order in list
        chooseFromList = chooseFromList.OrderBy(item => Guid.NewGuid()).ToList();
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

    public void ResetItems()
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
        if (_cardsSelected < _cardsToSelect)
        {
            PopulateAvailableItems();
        }
        else
        {
            DeckRenderer.s_Instance.QueueUpdate();
            _cardsSelected = 0;
            CloseShop();
        }
    }
}