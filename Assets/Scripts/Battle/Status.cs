using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status
{
    StatusType type;
    bool isDebuff;
    bool removeAtEndOfTurn;
    public Character owner {  get; private set; }
    public int stack {  get; private set; }
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
        OnApply();
    }
    public virtual void OnStack(int addStack)
    {
        stack += addStack;
    }
    protected virtual void OnApply() { }
    protected virtual void OnRemove() { }
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
