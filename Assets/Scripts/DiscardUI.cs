using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiscardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI discardCount;
    public void Set(int discardCountNew)
    {
        discardCount.text = discardCountNew.ToString();
    }
}
