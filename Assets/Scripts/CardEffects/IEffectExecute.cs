using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EffectContext;

public interface IEffectExecute
{
    int GetValue();
    EffectType GetEffectType();
    void Resolve(EffectContext ctx);
}
