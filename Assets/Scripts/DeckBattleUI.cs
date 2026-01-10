using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckBattleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deckCount;
    public void Set(int deckCountNew)
    {
        deckCount.text = deckCountNew.ToString();
    }
}
