using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverTooltipHandler : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] private Image tooltip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering");
        tooltip.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Stop hovering");
        tooltip.gameObject.SetActive(false);
    }
}
