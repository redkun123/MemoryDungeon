using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuardCount : MonoBehaviour
{
    public TextMeshProUGUI guardCount;
    public GameObject guardIcon;
    public void ModifyGuard(int currentGuard)
    {
        if (currentGuard > 0)
        {
            guardIcon.SetActive(true);
            guardCount.text = currentGuard.ToString();
        }
        else
        {
            LostGuard();
        }
    }
    public void LostGuard()
    {
        Debug.Log("Make guard icon disappear");
        guardIcon.SetActive(false);
        guardCount.text = "";
    }
}
