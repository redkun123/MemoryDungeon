using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Relic
{
    public RelicData data;
    public string relicName => data.relicName;
    public string description => data.description;
    public void Init(RelicData data)
    {
        this.data = data;
    }
    public virtual void OnEquip(Player player) { }
    public virtual IEnumerator OnTrigger(RelicContext ctx) 
    {
        yield return null;
    }
}
