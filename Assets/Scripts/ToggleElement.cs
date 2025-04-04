using TMPro;
using UnityEngine;

public class ToggleElement : MonoBehaviour
{
    private bool state = false;
    public TextMeshProUGUI textElement;
    public Sprite spriteElement;
    public bool text;
    public GameObject endScreemPopup;

    public void Toggle(bool choice)
    {
        if (choice == true)
        {
            textElement.gameObject.SetActive(state);
        }

        state = !state;
    }
    public void ToggleEndScreenPopup()
    {
        endScreemPopup.SetActive(false);
    }

}
