using TMPro;
using UnityEngine;

public class ToggleElement : MonoBehaviour
{
    private bool state = false;
    public TextMeshProUGUI textElement;
    public Sprite spriteElement;
    public bool text;

    public void Toggle(bool choice)
    {
        Object element = null;

        if (choice == true)
        {
            textElement.gameObject.SetActive(state);
        }        

        if (state == false)
        {
            state = true;
        }
        else
        {
            state = false;
        }
    }

}
