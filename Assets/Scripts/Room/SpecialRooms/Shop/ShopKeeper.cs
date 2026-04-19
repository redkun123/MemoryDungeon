using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private Button shopKeeper;
    [SerializeField] private ShopManager shopManager;
    public void Awake()
    {
        shopKeeper.onClick.AddListener(OnClickShop);
    }

    public void OnClickShop()
    {
        shopManager.SetPopupActive();
        Debug.Log("Shopkeeper pressed.");
    }
}
