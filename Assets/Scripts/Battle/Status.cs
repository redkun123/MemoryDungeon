using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status
{
    StatusType type;
    bool isDebuff;
    bool removeAtEndOfTurn;
    public string name;
    public Character owner { get; private set; }
    public int stack { get; set; }
    private enum StatusType
    {
        None,
        ModifyStat,
        TriggerOnEvent,
        Control
    }
    public void Init(Character owner, int stack)
    {
        this.owner = owner;
        this.stack = stack;
        GetName();
        OnApply();
    }
    public virtual void OnStack(int addStack)
    {
        stack += addStack;
    }
    public virtual void GetName() { }
    protected virtual void OnApply() { }
    protected virtual void OnRemove() { }
    public virtual void OnBattleStart() { }
    public virtual void OnTurnStart() { }
    public virtual void OnTurnEnd() { }
    public virtual void Modify(EffectContext ctx) { }
    public void ReduceStack(int amount)
    {
        stack -= amount;
    }
    public bool IsExpired()
    {
        return stack <= 0;
    }
    public void Remove()
    {
        OnRemove();
    }
}
