using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class Player : Character
{
    public int maxEnergy;
    public int currentEnergy;
    public int gold;
    public List<Card> trueDeck;
    public List<Card> deck;
    public List<Card> hand;
    public List<Card> discard;
    public event Action<int, int> OnEnergyChanged;
    public event Action<int> OnDeckChange;
    public event Action<int> OnTrueDeckChange;
    public event Action<int> OnDiscardChanged;
    public event Action<int> OnGoldChange;
    public void SpendEnergy(int amount)
    {
        int oldEn = currentEnergy;
        currentEnergy -= amount;
        if (currentEnergy != oldEn)
        {
            OnEnergyChanged?.Invoke(currentEnergy, maxEnergy);
        }
    }
    public void RestoreHP(int amount) //Hồi máu
    {
        currentHP += amount;
        currentHP = Math.Min(currentHP, maxHP);
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
        for (int i = hand.Count - 1; i >= 0; i--)
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
        OnDeckChange?.Invoke(deck.Count);
        hand.Add(card);
    }
    private void RefillDeck()
    {
        deck.AddRange(discard);
        discard.Clear();
        OnDiscardChanged?.Invoke(discard.Count);
        deck.Shuffle();
        OnDeckChange?.Invoke(deck.Count);
    }
}
