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
    //public Player()
    //{
    //    name = "Rocky";
    //    maxHealth = 100;
    //    currentHealth = maxHealth;
    //    maxEnergy = 3;
    //    IsAlive = true;
    //}
    public void SpendEnergy(int amount)
    {
        currentEnergy -= amount;
    }

    public void UseCard(Card card, CardContext context)
    {
        foreach (var effect in card.CardEffect)
        {
            effect.Execute(context);
        }
    }

    public void Discard(Card card)
    {
        hand.Remove(card);
        discard.Add(card);
    }
    public void RestoreEnergy()
    {
        this.currentEnergy = maxEnergy;
    }

    public void DrawOne()
    {
        if (deck.Count == 0) RefillDeck();
        if (deck.Count == 0) return;
        Card card = deck[0];
        deck.RemoveAt(0);
        hand.Add(card);
    }
    private void RefillDeck()
    {
        deck.AddRange(discard);
        discard.Clear();
        deck.Shuffle();
    }
}
