using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EffectContext
{
    public Character source;
    public Character target;
    public int value;
    public EffectType type;
    public EffectContext(Character source, Character target, int value, EffectType type)
    {
        this.source = source;
        this.target = target;
        this.value = value;
        this.type = type;
    }
}
public enum EffectType
{
    Damage,
    Block,
    LoseHP,
    ApplyStatus,
    Heal,
    Discard
}