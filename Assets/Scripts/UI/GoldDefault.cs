using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldDefault : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI goldAmount;

    public void Init(int gold)
    {
        var amount = gold.ToString();
        goldAmount.text = amount;
    }    
}
