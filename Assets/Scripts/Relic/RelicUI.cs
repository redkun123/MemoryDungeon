using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI relicName;
    [SerializeField] public TextMeshProUGUI relicDescription;
    public RelicData relicData;
    [SerializeField] Image relicImage;

    public void SetupRelic(RelicData relic)
    {
        relicData = relic;
        relicName.text = relicData.relicName;
        relicImage.sprite = relic.icon;
        relicDescription.text = relicData.description;
    }
    public void ShowTooltip()
    {
        //show tooltip khi click/hover
    }
}
