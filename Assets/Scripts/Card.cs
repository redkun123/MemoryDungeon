using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public int energyCost;
    public List<CardEffect> cardEffect;
}

