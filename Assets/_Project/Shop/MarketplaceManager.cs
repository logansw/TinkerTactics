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
    private int _cardsSelected;
    private int _cardsToSelect;
    private int _cardOptionsCount;

    public override void Initialize()
    {
        base.Initialize();
        PopulateAvailableItems();
    }

    public void CloseShop()
    {
        GameManager.s_Instance.NextLevel();
        SceneLoader.s_Instance.UnloadScene(SceneType.Shop);        
    }

    public void PopulateAvailableItems()
    {
        ResetCards();
        if (GameManager.s_Instance.CurrentLevelIndex % 2 == 0)
        {
            PrepareTowerShop();
        }
        else
        {
            PrepareTinkerShop();
        }
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

    private void PrepareTowerShop()
    {
        AllTowers = AllTowers.OrderBy(item => Guid.NewGuid()).ToList();
        _cardOptionsCount = 3;
        _cardsToSelect = 1;
        RenderNewItems(AllTowers);
    }

    private void PrepareTinkerShop()
    {
        AllTinkers = AllTinkers.OrderBy(item => Guid.NewGuid()).ToList();
        _cardOptionsCount = 3;
        _cardsToSelect = 3;
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