using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(DeckRenderer))]
public class DeckManager : Singleton<DeckManager>
{
    public const int CARD_DRAW_COUNT = 7;
    public List<Card> Deck;
    public List<Card> DrawPile;
    public List<Card> Hand;
    public List<Card> DiscardPile;
    public List<Card> ConsumePile;
    public List<Card> Database;
    [SerializeField] private RectTransform _cardPrefab;
    private DeckRenderer _deckRenderer;
    private bool _initialDraw = true;

    public override void Initialize()
    {
        base.Initialize();
        _deckRenderer = GetComponent<DeckRenderer>();
        InitializeDeck();
        DrawPile = new List<Card>();
        Hand = new List<Card>();
        ConsumePile = new List<Card>();
        DiscardPile = new List<Card>();
        InitializeDrawPile();
        InitializeDatabase();
    }

    // Instantiate Card game objects and add them to the Draw pile, shuffled.
    public void InitializeDeck()
    {
        List<Card> tempDeck = new List<Card>();
        foreach (Card card in Deck)
        {
            Card instance = Instantiate(card);
            instance.transform.localScale = Vector3.one;
            instance.IsOwned = true;
            tempDeck.Add(instance);
        }
        Deck.Clear();
        Deck = tempDeck;
    }

    private void InitializeDatabase()
    {
        Database = new List<Card>();
    }

    public void Shuffle(List<Card> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Card value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void Reset()
    {
        _initialDraw = true;
        foreach (Card card in Hand)
        {
            DiscardPile.Add(card);
        }
        Hand.Clear();
        foreach (Card card in DiscardPile)
        {
            DrawPile.Add(card);
        }
        DiscardPile.Clear();
        foreach (Card card in ConsumePile)
        {
            DrawPile.Add(card);
        }
        ConsumePile.Clear();
        Shuffle(DrawPile);
        DrawNewHand();
    }

    public void InitializeDrawPile()
    {
        foreach (Card card in Deck)
        {
            DrawPile.Add(card);
        }
        Deck.Clear();
    }

    // public void AddCardToDeck(Card card)
    // {
        // RectTransform card = Instantiate(_cardPrefab);
        // card = card.gameObject.AddComponent(card.GetType()) as Card;
        // Deck.Add(card);
    // }

    // public void AddCardToDeck<T>(int count) where T : Card
    // {
    //     for (int i = 0; i < count; i++)
    //     {
    //         RectTransform card = Instantiate(_cardPrefab);
    //         T card = card.AddComponent<T>();
    //         Deck.Add(card);
    //     }
    // }

    public void DrawNewHand()
    {
        if (_initialDraw)
        {
            _initialDraw = false;
            Card firstCard = null;
            foreach (Card card in DrawPile)
            {
                if (card.CardEffect is DeployTower)
                {
                    firstCard = card;
                    break;
                }
            }
            firstCard.OnDrawn();
            MoveCard(firstCard, DrawPile, Hand);
            Shuffle(DrawPile);
            DrawCards(CARD_DRAW_COUNT - 1);
        }
        else
        {
            foreach (Card card in Hand)
            {
                DiscardPile.Add(card);
            }
            Hand.Clear();
            DrawCards(CARD_DRAW_COUNT);
        }
    }

    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            DrawCard();
        }
    }

    private void DrawCard()
    {
        if (DrawPile.Count == 0)
        {
            foreach (Card discardedCard in DiscardPile)
            {
                DrawPile.Add(discardedCard);
            }
            DiscardPile.Clear();
            Shuffle(DrawPile);
        }
        Card card = DrawPile[0];
        card.OnDrawn();
        MoveCard(card, DrawPile, Hand);
    }

    public void Discard(Card card)
    {
        if (!Hand.Contains(card))
        {
            Debug.LogError("The card you are trying to discard is not in your hand");
            return;
        }

        MoveCard(card, Hand, DiscardPile);
    }

    public void Consume(Card card)
    {
        if (!Hand.Contains(card))
        {
            Debug.LogError("The card you are trying to discard is not in your hand");
            return;
        }

        MoveCard(card, Hand, ConsumePile);
    }

    public Card GetRandomDatabaseCard()
    {
        return Database[Random.Range(0, Database.Count)];
    }

    private void MoveCard(Card card, List<Card> from, List<Card> to)
    {
        if (!from.Contains(card))
        {
            Debug.LogError("The card you are trying to move is not in the source list");
            return;
        }
        to.Add(card);
        from.Remove(card);
        _deckRenderer.QueueUpdate();
    }

    public void AddCardToDeck(Card card)
    {
        DrawPile.Add(card);
        Reset();
    }

    public void ShowReturnTray(bool show)
    {
        _deckRenderer.ShowReturnTray(show);
    }
}