using System.Collections;
using UnityEngine;

public class PlayCardAction : IBattleAction
{
    private CardRuntime card;

    public PlayCardAction(CardRuntime card)
    {
        this.card = card;
    }

    public IEnumerator Execute()
    {
        //Debug.Log($"Playing card: {card.Data.cardName}");

        // Disable interaction
        card.View.GetComponent<CanvasGroup>().blocksRaycasts = false;

        // Play animation
        yield return card.View.PlayMoveToCenter();
    }
}