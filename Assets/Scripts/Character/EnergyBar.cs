using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private Slider energyBar;
    [SerializeField] Transform fill;
    public void InitSet(int currentEnergy, int maxEnergy)
    {
        energyBar.maxValue = maxEnergy;
        energyBar.minValue = 0;
        if (maxEnergy <= 0) return;
        energyBar.value = currentEnergy;
    }
    public void Set(int currentEnergy, int maxEnergy)
    {
        if (currentEnergy == 0)
        {
            energyBar.value = 0;
        }
        else
        {
            energyBar.value = maxEnergy;
        }
    }
    //public void Set(int currentEnergy, int maxEnergy)
    //{
    //    energyBar.value = currentEnergy;
    //    energyBar.value = Mathf.Clamp(currentEnergy, 0, maxEnergy);
    //}
}
