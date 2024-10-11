using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(DeckRenderer))]
public class DeckManager : Singleton<DeckManager>
{
    public List<Card> Deck { get; private set; }
    public List<Card> DrawPile { get; private set; }
    public List<Card> Hand { get; private set; }
    public List<Card> DiscardPile { get; private set; }
    public List<Card> ExhaustPile { get; private set; }
    public List<Card> Database { get; private set; }
    public List<Card> Shop { get; private set; }
    [SerializeField] private RectTransform _cardPrefab;
    private DeckRenderer _deckRenderer;

    protected override void Awake()
    {
        base.Awake();
        _deckRenderer = GetComponent<DeckRenderer>();
    }

    public void Initialize()
    {
        InitializeDeck();
        DrawPile = new List<Card>();
        Hand = new List<Card>();
        ExhaustPile = new List<Card>();
        DiscardPile = new List<Card>();
        Shop = new List<Card>();
        InitializeDrawPile();
        DrawNewHand();
        InitializeDatabase();
    }

    // Instantiate Card game objects and add them to the Draw pile, shuffled.
    public void InitializeDeck()
    {
        Deck = new List<Card>();
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
        foreach (Card card in ExhaustPile)
        {
            DrawPile.Add(card);
        }
        ExhaustPile.Clear();
        Shuffle(DrawPile);
    }

    public void InitializeDrawPile()
    {
        foreach (Card card in Deck)
        {
            DrawPile.Add(card);
        }
        Shuffle(DrawPile);
        Deck.Clear();
    }

    private void AddCardToDatabase<T>(int count) where T : Card
    {
        for (int i = 0; i < count; i++)
        {
            RectTransform card = Instantiate(_cardPrefab);
            // T card = card.AddComponent<T>();
            // Database.Add(card);
        }
    }

    public void AddCardToDeck(Card card)
    {
        // RectTransform card = Instantiate(_cardPrefab);
        // card = card.gameObject.AddComponent(card.GetType()) as Card;
        // Deck.Add(card);
    }

    public void AddCardToDeck<T>(int count) where T : Card
    {
        for (int i = 0; i < count; i++)
        {
            RectTransform card = Instantiate(_cardPrefab);
            // T card = card.AddComponent<T>();
            // Deck.Add(card);
        }
    }

    public void DrawNewHand()
    {
        foreach (Card card in Hand)
        {
            DiscardPile.Add(card);
        }
        Hand.Clear();
        DrawCards(5);
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

    public void Exhaust(Card card)
    {
        if (!Hand.Contains(card))
        {
            Debug.LogError("The card you are trying to discard is not in your hand");
            return;
        }

        MoveCard(card, Hand, ExhaustPile);
    }

    public Card GetRandomDatabaseCard()
    {
        return Database[Random.Range(0, Database.Count)];
    }

    public void DatabaseToShop(Card card)
    {
        MoveCard(card, Database, Shop);
    }

    public void ShopToDatabase(Card card)
    {
        MoveCard(card, Shop, Database);
    }

    public void ShopToDeck(Card card)
    {
        MoveCard(card, Shop, DiscardPile);
        card.gameObject.SetActive(false);
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
}