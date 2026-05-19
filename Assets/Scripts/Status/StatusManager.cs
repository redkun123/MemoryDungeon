using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusManager
{
    private Character owner;
    private List<Status> statuses;
    private Dictionary<string,string> statusByName;
    public event Action OnStatusListChange;
    public StatusManager(Character owner)
    {
        this.owner = owner;
        statuses = new();
    }
    public IEnumerator TriggerPhase(BattlePhase phase)
    {
        foreach (var status in statuses)
        {
            yield return TriggerStatus(status, phase);
        }

        CleanUp();
    }
    private IEnumerator TriggerStatus(Status status, BattlePhase phase)
    {
        switch (phase)
        {
            case BattlePhase.BattleStart:
                status.OnBattleStart();
                break;

            case BattlePhase.TurnStart:
                status.OnTurnStart();
                break;

            case BattlePhase.TurnEnd:
                status.OnTurnEnd();
                break;
        }

        yield return null;
    }
    public void AddStatus(Status newStatus, int stack)
    {
        var existing = statuses.FirstOrDefault(s => s.GetType() == newStatus.GetType());
        if (existing == null)
        {
            newStatus.Init(owner, stack);
            statuses.Add(newStatus);
        }
        else
        {
            existing.OnStack(stack);
        }
        Debug.Log($"{owner} get {stack} {newStatus.name}");
        CleanUp();
    }
    public void OnBattleEnd()
    {
        //for (int i = statuses.Count - 1; i >= 0; i--)
        //{
        //    RemoveStatus(statuses[i]);
        //}
    }
    public void RemoveStatus(Status status)
    {
        status.Remove();
        statuses.Remove(status);
    }
    public void OnTurnStart()
    {
        //foreach (var s in statuses)
        //    s.OnTurnStart();

        //CleanUp();
    }

    public void OnTurnEnd()
    {
        //foreach (var s in statuses)
        //    s.OnTurnEnd();

        //CleanUp();
    }
    public void OnAttack(EffectContext ctx)
    {

    }
    public void OnAttacked(EffectContext ctx)
    {

    }
    public void ApplyModifiers(EffectContext ctx)
    {
        foreach (var s in statuses)
            s.Modify(ctx);
    }
    private void CleanUp()
    {
        for (int i = statuses.Count - 1; i >= 0; i--)
        {
            if (statuses[i].IsExpired())
            {
                statuses[i].Remove();
                statuses.RemoveAt(i);
            }
        }
        OnStatusListChange?.Invoke();
    }
    public Dictionary<string,string> GetAll()
    {
        if (statusByName == null)
        {
            statusByName = new Dictionary<string,string>();
        }
        else
        {
            statusByName.Clear();
        }
        for(int i = 0; i < statuses.Count; i++)
        {
            if (statuses[i].name == null)
            {
                statuses[i].GetName();
            }
            statusByName.Add(statuses[i].name, statuses[i].stack.ToString());
        }
        return statusByName;
    }
}
