using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public int maxEnergy;
    public int currentEnergy;
    public int gold;
    public List<Card> deck;
    public List<Card> hand;
    public List<Card> discard;
    public event Action<int, int> OnEnergyChanged;
    public event Action<int> OnDeckChanged;
    public event Action<int> OnDiscardChanged;
    public void SpendEnergy(int amount)
    {
        int oldEn = currentEnergy;
        currentEnergy -= amount;
        if (currentEnergy != oldEn)
        {
            OnEnergyChanged?.Invoke(currentEnergy, maxEnergy);
        }
    }

    public void UseCard(Card card, CardContext context)
    {
        foreach (var effect in card.cardEffect)
        {
            effect.Execute(context);
        }
        Debug.Log($"Card {card.name} played");
    }

    public void Discard(Card card)
    {
        hand.Remove(card);
        discard.Add(card);
        OnDiscardChanged?.Invoke(discard.Count);
        Debug.Log("Card discarded");
    }
    public void DiscardAll()
    {
        for (int i = hand.Count-1; i >= 0; i--)
        {
            Card card = hand[i];
            hand.RemoveAt(i);
            discard.Add(card);
            OnDiscardChanged?.Invoke(discard.Count);
            Debug.Log("Card discarded");
        }
    }
    public void RestoreEnergy(int maxEnergy)
    {
        this.currentEnergy = maxEnergy;
        OnEnergyChanged?.Invoke(currentEnergy, maxEnergy);
    }

    public void DrawOne()
    {
        if (deck.Count == 0) RefillDeck();
        if (deck.Count == 0) return;
        Card card = deck[0];
        deck.RemoveAt(0);
        OnDeckChanged?.Invoke(deck.Count);
        hand.Add(card);
    }
    private void RefillDeck()
    {
        deck.AddRange(discard);
        discard.Clear();
        OnDiscardChanged?.Invoke(discard.Count);
        deck.Shuffle();
        OnDeckChanged?.Invoke(deck.Count);
    }
}
