using DG.Tweening;
using UnityEngine;
public class OnHoverCoinAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 0.15f;
    public GameObject ToolTip;

    public void OpenToolTipAnimation()
    {
        ToolTip.SetActive(true);
        ToolTip.GetComponent<CanvasGroup>().alpha = 0; 
        ToolTip.GetComponent<CanvasGroup>().DOFade(1f, duration); 
    }

    public void CloseToolTipAnimation()
    {
        ToolTip.GetComponent<CanvasGroup>().DOFade(0f, duration);
        ToolTip.SetActive(false);
    }
}
