using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicPatriotShield : Relic
{
    public override IEnumerator OnTrigger(RelicContext ctx)
    {
        //var status = data.Effect.Clone();
        //ctx.player.statusManager.AddStatus(status, data.value);
        yield return null;
    }
}
