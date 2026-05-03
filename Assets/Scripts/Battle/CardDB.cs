using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Database")]
public class CardDB : ScriptableObject
{
    [Header("List card")]

    [SerializeField] public List<Card> cardDatabase;
    private Dictionary<string, Card> _lookup;

    public void Init()
    {
        if (_lookup != null)
        {
            _lookup.Clear();
        }
        _lookup = new Dictionary<string, Card>();
        foreach (var card in cardDatabase)
        {
            if (_lookup.ContainsKey(card.cardID))
            {
                Debug.LogError($"Duplicate card id: {card.cardID}");
                continue;
            }
            _lookup.Add(card.cardID, card);
        }
    }
    public Card GetCard(string id)
    {
        if (_lookup == null)
            Init();

        if (_lookup.TryGetValue(id, out var card))
            return card;

        Debug.LogError($"Card not found: {id}");
        return null;
    }
}
