using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class HoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private float scaleMultiplier = 0.97f; 
    [SerializeField] private float duration = 0.15f;
    private Vector3 originalScale;

    private Outline oldOutline = null;
    private Color originalOutlineColor;
    private Vector2 originalOutlineDistance;
    private bool outlineAdded = false;

    private void Awake()
    {
        originalScale = transform.localScale;

        oldOutline = GetComponent<Outline>();
        if (oldOutline != null)
        {
            originalOutlineColor = oldOutline.effectColor;
            originalOutlineDistance = oldOutline.effectDistance;
        }
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
        Outline outline = gameObject.GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
            outlineAdded = true; 
        }
        else if (!outlineAdded) 
        {
            originalOutlineColor = outline.effectColor;
            originalOutlineDistance = outline.effectDistance;
        }

        outline.effectColor = Color.black; 
        outline.effectDistance = new Vector2(5, 5);
        transform.DOScale(originalScale * scaleMultiplier, duration);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Outline outline = gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            if (outlineAdded)
            {
                Destroy(outline);
                outlineAdded = false;
            }
            else
            {
                outline.effectColor = originalOutlineColor;
                outline.effectDistance = originalOutlineDistance;
            }
        }
        transform.DOScale(originalScale, duration);
    }
}