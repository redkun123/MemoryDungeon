using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContext
{
    public Player source;
    public Character target;
    public Card card;

    public CardContext(Player source, Character target, Card card)
    {
        this.source = source;
        this.target = target;
        this.card = card;
    }
}



