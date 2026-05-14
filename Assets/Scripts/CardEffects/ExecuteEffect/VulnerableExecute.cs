using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VulnerableExecute
{

}
    /*: IEffectExecute
{
    private float attackModifier;
    private int stack;
    public VulnerableExecute(int stack)
    {
        this.stack = stack;
        attackModifier = 1.5f;
    }

    public int GetValue() => stack;

    public EffectType GetEffectType() => EffectType.ApplyStatus;

    public void Resolve(EffectContext ctx)
    {
        Type statusType = typeof(VulnerableStatus);
        var status = (Status)Activator.CreateInstance(statusType);
        ctx.target.statusManager.AddStatus(status, stack);
        Debug.Log($"Activated Status: {status.name} + {stack}");
    }
    
}*/
