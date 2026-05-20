using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class BattleLogic
{
    public Player player;
    public Enemy enemy;
    public HandManager handManager;
    public int maxHandSize = 6;
    private BattleManager battleManager;
    public BattleExecutor battleExecutor;
    public CardManager cardManager;
    public void Register(BattleManager bm)
    {
        battleManager = bm;
        Debug.Log("Battle Manager registered to battle logic");
        battleExecutor = bm.battleExecutor;
    }
    public IEnumerator RefillHand(HandManager handManager, Player player)
    {
        Debug.Log("Refill hand started");
        this.player = player;
        Debug.Log($"player.hand = {player.hand}");
        int need = maxHandSize - player.hand.Count;
        Debug.Log("Get number of card to draw");
        for (int i = 0; i < need; i++)
        {
            player.DrawOne();
            yield return new WaitForSeconds(0.25f);
            Debug.Log($"Draw {i}");
        }
        Debug.Log($"Need draw: {need}");
    }
    public bool CanPlayCard(Card card)
    {
        if (!player.hand.Contains(card)) return false;
        if (player.currentEnergy < card.energyCost)
        {
            Debug.Log("Not enough Energy!");
            return false;
        }
        return true;
    }

    public void PlayCard(Card card, Enemy enemy)
    {
        player.SpendEnergy(card.energyCost);
        Debug.Log("Energy spent");
        cardManager = new CardManager(player, enemy, card);
        cardManager.battleExecutor = battleExecutor;
        cardManager.UseCard(card);
        player.Discard(card);
        //if (battleManager.battleEnded)
        //{
        //    battleManager.CheckGameResult();
        //}
        cardManager = null;
    }


    public void EnemyActionPerTurn(Enemy enemy, Player player)
    {
        Card card = enemy.moveSet[enemy.turnCount];
        Debug.Log($"Enemy used {enemy.moveSet[enemy.turnCount]}");
        cardManager = new CardManager(enemy, player, card);
        cardManager.battleExecutor = battleExecutor;
        cardManager.UseCard(card);
        cardManager = null;
        enemy.turnCount = EnemyConfigTurnCount(enemy);
    }

    public int EnemyConfigTurnCount(Enemy enemy)
    {
        enemy.turnCount++;
        if (enemy.turnCount >= enemy.moveSet.Count) enemy.turnCount = 0;
        return enemy.turnCount;
    }
}

