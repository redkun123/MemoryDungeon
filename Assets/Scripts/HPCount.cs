using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPCount : MonoBehaviour
{
    public TextMeshProUGUI hpCount;
    // Start is called before the first frame update
    void Start()
    {
        hpCount = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void Set(int currentHP, int maxHP)
    {
        hpCount.text = $"{currentHP}/{maxHP}";
    }
}
