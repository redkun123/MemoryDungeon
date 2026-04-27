using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class CardManager
{
    public Character user;
    public Character opponent;
    public List<Card> targetCard;
    public Character targetChar;
    public Card card;
    public BattleExecutor battleExecutor;

    public CardManager(Character user, Character opponent, Card card)
    {
        this.user = user;
        this.opponent = opponent;
        this.card = card;
    }
    public void UseCard(Card card)
    {
        foreach (var cardEffect in card.cardEffect)
        {
            TargetAcquire(cardEffect);
            Character source = user;
            Character target = targetChar;
            IEffect effect = cardEffect.CreateEffect(source, target);
            battleExecutor.ExecuteEffect(effect, source, target);
            Debug.Log($"Card {card.name} played");
        }
    }
    public void TargetAcquire(CardEffect effect)
    {
        switch (effect.effectTargetType)
        {
            case CardEffect.EffectTargetType.Character:
                targetChar = CharacterTargetAcquire(effect.effectTarget);
                break;
            case CardEffect.EffectTargetType.Card:
                targetCard = CardTargetAcquire(effect.effectTarget);
                break;
            case CardEffect.EffectTargetType.None:
                break;
        }
    }
    public Character CharacterTargetAcquire(CardEffect.EffectTarget target)
    {
        Character targetChar = null;
        switch (target)
        {
            case CardEffect.EffectTarget.Opponent:
                targetChar = opponent;
                break;
            case CardEffect.EffectTarget.Self:
                targetChar = user;
                break;
            default:
                Debug.Log("No target");
                targetChar = null;
                break;
        }
        return targetChar;
    }
    public List<Card> CardTargetAcquire(CardEffect.EffectTarget target)
    {
        List<Card> targetCards = new();
        switch (target)
        {
            case CardEffect.EffectTarget.ThisCard:
                targetCards.Add(card);
                break;
            default:
                Debug.Log("No target");
                break;
        }
        return targetCards;
    }
}
