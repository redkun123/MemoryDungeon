using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Card/Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public int energyCost;
    public Sprite cardImage;
    public List<CardEffect> cardEffect;
    public CardType cardType;
    public enum CardType
    {
        Attack,
        Guard,
        Setup,
        Feint
    }
    public string GetFullDescription()
    {
        List<string> listEffect = new List<string>();
        foreach(var effect in cardEffect)
        {
            listEffect.Add(effect.effectDescription);
        }
        string fullDescription = Extensions.Concatenate(listEffect);
        return fullDescription;
    }
}

