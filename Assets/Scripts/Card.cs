using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card")]
public class Card : ScriptableObject
{
    public string CardName;
    public int EnergyCost;
    public List<CardEffect> CardEffect;
}

