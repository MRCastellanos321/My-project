using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("pointer entered");

        tooltip.SetActive(true);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("pointer exit");

        tooltip.SetActive(false);

    }
}
