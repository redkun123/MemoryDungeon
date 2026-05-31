using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicAirBlade : Relic
{
    public override IEnumerator OnTrigger(RelicContext ctx)
    {
        var statusType = data.GetStatusType();
        var status = (Status)Activator.CreateInstance(statusType);
        yield return ctx.player.statusManager.AddStatus(status, data.value);
        Debug.Log($"Activated Status: {status.name} + {data.value}");
    }
}
