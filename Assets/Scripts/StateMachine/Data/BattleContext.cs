using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleContext
{
    public Player Player;
    public List<Enemy> Enemies;

    public DeckRuntime Deck;
    public HandRuntime Hand;
    public DiscardRuntime Discard;
}
