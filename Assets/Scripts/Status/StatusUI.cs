using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI statusName;
    [SerializeField] public TextMeshProUGUI statusDescription;
    [SerializeField] public TextMeshProUGUI statusStack;
    private StatusData statusData;
    [SerializeField] Image statusImage;

    public void SetupStatus(StatusData status, string stack)
    {
        statusData = status;
        statusName.text = statusData.statusName;
        statusDescription.text = statusData.description;
        statusImage.sprite = status.icon;
        statusStack.text = stack;
    }
    public void ShowTooltip()
    {
        //show tooltip khi click/hover
    }
}
