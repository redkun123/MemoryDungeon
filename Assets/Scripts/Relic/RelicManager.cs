using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RelicManager
{
    public RelicLibrary relicLibrary;
    public List<Relic> currentRelic;
    public List<RelicData> currentRelicData;
    private BattleManager battleManager; //can nhac unbind sau moi battle
    public RelicContext ctx;
    public event Action OnReceiveRelic;

    public RelicManager(RelicLibrary relicLibrary)
    {
        this.relicLibrary = relicLibrary;
        currentRelic = new List<Relic>();
        currentRelicData = new List<RelicData>();
        ctx = new RelicContext();
    }
    public void Setup()
    {
        battleManager = RunManager.Instance.battleManager;
        ctx.Init(battleManager);
    }
    public void AddRelicByID(string id)
    {
        var data = relicLibrary.relics.Find(r => r.id == id);
        Relic relic = id switch
        {
            "Air Blade" => new RelicAirBlade(),
            "Patriot Shield" => new RelicPatriotShield(),
            "Glass Shoe" => new RelicGlassShoe(),
            _ => throw new NotImplementedException()
        };
        relic.Init(data);
        currentRelic.Add(relic);
        currentRelicData.Add(data);
        Debug.Log($"Received {relic.relicName}");
        OnEquip(relic);
        OnReceiveRelic?.Invoke();
    }
    public void OnEquip(Relic relic)
    {
        var player = RunManager.Instance.player;
        relic.OnEquip(player);
    }
    public void OnDestroy()
    {
        currentRelic.Clear();
        battleManager = null;
    }
    public IEnumerator TriggerPhase(BattlePhase phase)
    {
        foreach (var relic in currentRelic)
        {
            if (!ShouldTrigger(relic, phase))
                continue;

            yield return TriggerRelic(relic);
        }
    }
    private bool ShouldTrigger(Relic relic, BattlePhase phase)
    {
        return relic.data.trigger switch
        {
            TriggerType.BattleStart =>
                phase == BattlePhase.BattleStart,

            TriggerType.PlayerTurnStart =>
                phase == BattlePhase.TurnStart,

            TriggerType.PlayerTurnEnd =>
                phase == BattlePhase.TurnEnd,

            _ => false
        };
    }
    private IEnumerator TriggerRelic(Relic relic)
    {
        relic.OnTrigger(ctx);

        yield return null;
    }
    //Them animation relic
    //private IEnumerator TriggerRelic(Relic relic)
    //{
    //    yield return relic.PlayEffect();

    //    relic.OnTrigger(ctx);

    //    yield return new WaitForSeconds(0.3f);
    //}
    public List<string> GetRelicData()
    {
        List<string> listRelic = new List<string>();
        for (int i = 0; i < currentRelicData.Count; i++)
        {
            listRelic.Add(currentRelicData[i].id);
        }
        return listRelic;
    }
}
