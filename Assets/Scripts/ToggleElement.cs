using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ToggleElement : MonoBehaviour
{

    private bool state = false;
    public TextMeshProUGUI textElement;
    public Sprite spriteElement;
    public bool text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
