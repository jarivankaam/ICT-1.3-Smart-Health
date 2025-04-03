using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ExplanationAnimation : MonoBehaviour
{
    public GameObject FrontSide;
    public GameObject BackSide;
    public TMP_Text ToolTipText;
    [SerializeField] private float AnimationDuration = 0.9f;
    private bool _flipToBack = true;

    // Toggle Cooldown
    private bool _isCooldownActive = false;
    private float _cooldownTime = 0.1f; // 100ms cooldown

    public void ToggleCircle()
    {
        // Cooldown check
        if (_isCooldownActive)
        {
            return;
        }

        _isCooldownActive = true; 
        StartCoroutine(ResetCooldown()); 

        // Coin Flip Handler
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

    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(_cooldownTime);
        _isCooldownActive = false;
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