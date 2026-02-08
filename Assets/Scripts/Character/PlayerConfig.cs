using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Config")]
public class PlayerConfig : ScriptableObject
{
    public string charName;
    public int maxHP;
    public int startGold;
    public List<Card> startingDeck;
}

