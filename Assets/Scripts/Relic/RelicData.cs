using System.Collections;
using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(menuName = "Relic/New Relic")]
public class RelicData : ScriptableObject
{
    public string id;
    public string relicName;
    public string description;
    public Sprite icon;

    public int value;
    public TriggerType trigger;
    public StatusTypeByName statusType;

    public Type GetStatusType()
    {
        return statusType switch
        {
            StatusTypeByName.Strength => typeof(StrengthStatus),
            //StatusTypeByName.Weak => typeof(WeakStatus),
            //StatusTypeByName.Vulnerable => typeof(VulnerableStatus),
            StatusTypeByName.Guard => typeof(GuardEffect),
            //StatusTypeByName.Energy
            _ => null
        };
    }
}
public enum StatusTypeByName
{
    Strength,
    Weak,
    Vulnerable,
    Guard,
    Energy
}
public enum TriggerType
{
    BattleStart,
    BattleEnd,
    PlayerTurnStart,
    PlayerTurnEnd,
    TakeDamage,
    CauseDamage
}