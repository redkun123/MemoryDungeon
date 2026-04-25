using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckCloseButton : MonoBehaviour
{
    [SerializeField] Button _close;
    [SerializeField] GameObject popup;
    private void Awake()
    {
        _close.onClick.AddListener(OnClickClose);
    }
    private void OnClickClose()
    {
        CardInputRouter.Instance.SetMode(CardInputRouter.Instance.oldMode);
        popup.SetActive(false);
    }
}
