using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverCoinAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private float duration = 0.15f;
    public GameObject ToolTip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OpenToolTipAnimation();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CloseToolTipAnimation();
    }

    public void OnSelect(BaseEventData eventData)
    {
        CloseToolTipAnimation();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        ToolTip.SetActive(false);
    }

    private void OpenToolTipAnimation()
    {
        ToolTip.SetActive(true);
        ToolTip.GetComponent<CanvasGroup>().alpha = 0; 
        ToolTip.GetComponent<CanvasGroup>().DOFade(1f, duration); 
    }

    private void CloseToolTipAnimation()
    {
        ToolTip.GetComponent<CanvasGroup>().DOFade(0f, duration);
        ToolTip.SetActive(false);
    }
}
