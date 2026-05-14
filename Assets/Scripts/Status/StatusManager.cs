using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusManager
{
    private Character owner;
    private List<Status> statuses;
    public StatusManager(Character owner)
    {
        this.owner = owner;
        statuses = new();
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
        foreach (var status in statuses)
        {
            RemoveStatus(status);
        }
    }
    public void RemoveStatus(Status status)
    {
        status.Remove();
        statuses.Remove(status);
    }
    public void OnTurnStart()
    {
        foreach (var s in statuses)
            s.OnTurnStart();

        CleanUp();
    }

    public void OnTurnEnd()
    {
        foreach (var s in statuses)
            s.OnTurnEnd();

        CleanUp();
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
    }
    public List<Status> GetAll() => statuses;
}
