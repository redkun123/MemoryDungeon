using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectContext;

public interface IEffect
{
    int GetValue();
    EffectType GetEffectType();
    void Resolve(EffectContext ctx);
}
