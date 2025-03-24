using UnityEngine;

public class ExplanationText : MonoBehaviour
{
    [SerializeField]
    private GameObject infoBox;

    public void ToggleInfoBox()
    {
        infoBox.SetActive(!infoBox.activeSelf);
    }
}