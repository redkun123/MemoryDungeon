using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    //public Player player;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerHP;
    [SerializeField] private TextMeshProUGUI playerGold;
    [SerializeField] private TextMeshProUGUI deckCount;
    [SerializeField] private TextMeshProUGUI floor;
    // Start is called before the first frame update
    void Awake()
    {
        RunManager.Instance.RegisterStatusBar(this);
        //this.player = RunManager.Instance.player;
    }

    public void UpdateStatus(Player player, int currentFloor)
    {
        playerName.text = $"{player.name}";
        playerHP.text = $"{player.currentHP}/{player.maxHP}";
        playerGold.text = $"{player.gold}";
        deckCount.text = $"{player.trueDeck.Count}";
        floor.text = $"{currentFloor}/10";
    }
}
