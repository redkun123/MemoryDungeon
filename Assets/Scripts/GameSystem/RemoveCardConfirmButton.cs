using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveCardConfirmButton : MonoBehaviour
{
    [SerializeField] RemoveCardConfirmPopup popup;
    [SerializeField] Button _removeButton;
    private void Awake()
    {
        _removeButton.onClick.AddListener(ConfirmRemove);
    }
    void ConfirmRemove()
    {
        popup.ConfirmRemoveCard();
    }
}
