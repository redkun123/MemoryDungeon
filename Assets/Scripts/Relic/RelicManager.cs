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
        foreach (var relic in currentRelic)
        {
            var trigger = relic.data.trigger;
            switch (trigger)
            {
                case TriggerType.BattleStart:
                    battleManager.OnBattleStart += () => OnTriggerRelic(relic);
                    break;
                case TriggerType.BattleEnd:
                    battleManager.OnBattleEnd += () => OnTriggerRelic(relic);
                    break;
                case TriggerType.PlayerTurnStart:
                    battleManager.OnPlayerTurnStart += () => OnTriggerRelic(relic);
                    break;
                case TriggerType.PlayerTurnEnd:
                    battleManager.OnPlayerTurnEnd += () => OnTriggerRelic(relic);
                    break;
                default:
                    Debug.Log("Trigger type unknown");
                    break;
            }
        }
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
        if (battleManager != null)
        {
            foreach (var relic in currentRelic)
            {
                var trigger = relic.data.trigger;
                switch (trigger)
                {
                    case TriggerType.BattleStart:
                        battleManager.OnBattleStart -= () => OnTriggerRelic(relic);
                        break;
                    case TriggerType.BattleEnd:
                        battleManager.OnBattleEnd -= () => OnTriggerRelic(relic);
                        break;
                    case TriggerType.PlayerTurnStart:
                        battleManager.OnPlayerTurnStart -= () => OnTriggerRelic(relic);
                        break;
                    case TriggerType.PlayerTurnEnd:
                        battleManager.OnPlayerTurnEnd -= () => OnTriggerRelic(relic);
                        break;
                    default:
                        Debug.Log("Trigger type unknown");
                        break;
                }
            }
        }
    }
    public void OnTriggerRelic(Relic relic)
    {
        relic.OnTrigger(ctx);
    }
}
