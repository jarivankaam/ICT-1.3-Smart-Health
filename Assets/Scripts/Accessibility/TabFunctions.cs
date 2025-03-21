using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeInput : MonoBehaviour
{
    EventSystem system;
    public List<Selectable> tabOrder;
    public Button submitButton;
    private bool hasTabbed = false;

    public void Start()
    {
        system = EventSystem.current;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            if (!hasTabbed && tabOrder.Count > 0)
            {
                tabOrder[0].Select();
                hasTabbed = true;
                return;
            }

            GameObject currentObj = system.currentSelectedGameObject;
            int currentIndex = tabOrder.FindIndex(s => s.gameObject == currentObj);

            if (currentIndex != -1) 
            {
                int nextIndex = shiftPressed ? (currentIndex - 1 + tabOrder.Count) % tabOrder.Count
                                             : (currentIndex + 1) % tabOrder.Count;
                tabOrder[nextIndex].Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) && system.currentSelectedGameObject != null)
        {
            Selectable current = system.currentSelectedGameObject.GetComponent<Selectable>();

            if (current is Button button)
            {
                button.onClick.Invoke();
            }
            else if (submitButton != null)
            {
                submitButton.onClick.Invoke();
            }
        }
    }
}
