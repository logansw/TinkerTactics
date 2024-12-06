using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(DeckRenderer))]
public class DeckManager : Singleton<DeckManager>
{
    public List<Card> Deck;
    public List<Card> Hand;
    public List<Card> DiscardPile;
    public List<Card> Database;
    private DeckRenderer _deckRenderer;

    public override void Initialize()
    {
        base.Initialize();
        _deckRenderer = GetComponent<DeckRenderer>();
        Hand = new List<Card>();
        DiscardPile = new List<Card>();
        InitializeDeck();
        InitializeDatabase();
    }

    // Instantiate Card game objects and add them to the Draw pile, shuffled.
    public void InitializeDeck()
    {
        foreach (Card card in Deck)
        {
            Card instance = Instantiate(card);
            instance.transform.localScale = Vector3.one;
            instance.IsOwned = true;
            instance.OnDrawn();
            Hand.Add(instance);
        }
        Deck.Clear();
        Deck = Hand;
        _deckRenderer.QueueUpdate();
    }

    private void InitializeDatabase()
    {
        Database = new List<Card>();
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

    public void RestoreCard(Card card)
    {
        if (!DiscardPile.Contains(card))
        {
            Debug.LogError("The card you are trying to return has not been discarded");
            return;
        }

        MoveCard(card, DiscardPile, Hand);
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
        Hand.Add(card);
        card.OnDrawn();
    }

    public void ShowReturnTray(bool show)
    {
        _deckRenderer.ShowReturnTray(show);
    }
}