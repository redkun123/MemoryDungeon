using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLosePopup : MonoBehaviour
{
    public TextMeshProUGUI result;
    public void ShowResult(string message)
    {
        result.text = message;
    }
}
