using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class CardEffect : ScriptableObject
{   
    public abstract void Execute(Character targetChar);
    public abstract void Execute(List<Card> targetCard);
    public string effectName;
    public string effectDescription;
    //public Character user;
    //public Character opponent;
    //public List<Card> targetCard;
    //public Character targetChar;
    [SerializeField] public EffectTarget effectTarget;
    [SerializeField] public EffectTargetType effectTargetType;
    public enum EffectTarget
    {
        Opponent,
        Self,
        ThisCard,
        ChooseCard
    }
    public enum EffectTargetType
    {
        Character,
        Card
    }
}
