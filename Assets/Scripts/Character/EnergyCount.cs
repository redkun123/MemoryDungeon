using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyCount : MonoBehaviour
{
    public TextMeshProUGUI energyCount;
    public void Set(int currentEnergy, int maxEnergy)
    {
        energyCount.text = $"{currentEnergy}/{maxEnergy}";
    }
}
