using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private float scaleMultiplier = 0.97f; 
    [SerializeField] private float duration = 0.15f;
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(originalScale * scaleMultiplier, duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, duration);
    }

    public void OnSelect(BaseEventData eventData)
    {
        transform.DOScale(originalScale * scaleMultiplier, duration);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.DOScale(originalScale, duration);
    }
}