using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveCardButton : MonoBehaviour
{
    [SerializeField] Button _removeButton;
    private void Awake()
    {
        _removeButton.onClick.AddListener(ShowPopupRemove);
    }
    void ShowPopupRemove()
    {
        CardInputRouter.Instance.SetMode(CardInputRouter.CardInputMode.Remove);
        DeckUIManager.Instance.ShowTrueDeck();
    }
}
