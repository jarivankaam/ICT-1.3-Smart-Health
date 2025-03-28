using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExplanationAnimation : MonoBehaviour, IPointerClickHandler
{
    public GameObject FrontSide;
    public GameObject BackSide;
    public TMP_Text ToolTipText;
    [SerializeField] private float AnimationDuration = 0.9f;
    private bool _flipToBack = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_flipToBack)
        {
            ToolTipText.text = "Klik voor minder uitleg! De munt draait terug.";
            FlipCircleToBack();
        }
        else
        {
            ToolTipText.text = "Klik voor meer uitleg! De munt draait terug.";
            FlipCircleToFront();
        }

        GetComponentInChildren<TMP_Text>().text = _flipToBack ? "<" : "?";
        _flipToBack = !_flipToBack;
    }

    public void FlipCircleToFront()
    {
        Sequence flipSequence = DOTween.Sequence();

        // Flip animation sequence
        flipSequence
            .Append(FrontSide.transform.DORotate(new Vector3(0, 90, 0), AnimationDuration / 2))
            .Join(BackSide.transform.DORotate(new Vector3(0, -90, 0), AnimationDuration / 2))
            .AppendCallback(() =>
            {
                FrontSide.SetActive(true);
                BackSide.SetActive(false);
            })
            .Append(FrontSide.transform.DORotate(new Vector3(0, 0, 0), AnimationDuration / 2));

        flipSequence.Play();
    }

    public void FlipCircleToBack()
    {
        Sequence flipSequence = DOTween.Sequence();

        // Flip animation sequence
        flipSequence
            .Append(FrontSide.transform.DORotate(new Vector3(0, -90, 0), AnimationDuration / 2))
            .Join(BackSide.transform.DORotate(new Vector3(0, 90, 0), AnimationDuration / 2))
            .AppendCallback(() =>
            {
                FrontSide.SetActive(false);
                BackSide.SetActive(true);
            })
            .Append(BackSide.transform.DORotate(new Vector3(0, 0, 0), AnimationDuration / 2));

        flipSequence.Play();
    }
}