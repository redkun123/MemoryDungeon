using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyCount : MonoBehaviour
{
    public TextMeshProUGUI energyCount;
    // Start is called before the first frame update
    void Start()
    {
        energyCount = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void Set(int currentEnergy, int maxEnergy)
    {
        energyCount.text = $"{currentEnergy}/{maxEnergy}";
    }
}
