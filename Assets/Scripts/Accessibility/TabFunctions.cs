using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ChangeInput : MonoBehaviour
{
    EventSystem system;
    public List<Selectable> tabOrder;
    public List<Selectable> tabOrderMenu;
    public Button submitButton;
    private bool isMenuActive = false;

    public void Start()
    {
        system = EventSystem.current;
    }

    public void Update()
    {
        List<Selectable> activeTabOrder = isMenuActive ? tabOrderMenu : tabOrder;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            // Only reset selection when pressing Tab, not every frame
            if (system.currentSelectedGameObject == null && activeTabOrder.Count > 0)
            {
                activeTabOrder[0].Select();
                return;
            }

            GameObject currentObj = system.currentSelectedGameObject;
            int currentIndex = activeTabOrder.FindIndex(s => s.gameObject == currentObj);

            if (currentIndex != -1)
            {
                int nextIndex = shiftPressed ? (currentIndex - 1 + activeTabOrder.Count) % activeTabOrder.Count
                                             : (currentIndex + 1) % activeTabOrder.Count;
                activeTabOrder[nextIndex].Select();
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

    // Call this method when toggling the menu
    public void ToggleMenu()
    {
        isMenuActive = !isMenuActive; // Toggle the state first

        Debug.Log("Menu Active: " + isMenuActive); // Debugging: check toggle state

        // Reset current selection before the coroutine runs
        system.SetSelectedGameObject(null);

        // Start the coroutine to delay selection while ensuring the correct list is used
        StartCoroutine(DelayedSelect(isMenuActive));
    }

    private IEnumerator DelayedSelect(bool menuState)
    {
        yield return new WaitForEndOfFrame(); // Small delay to prevent double input

        List<Selectable> newTabOrder = menuState ? tabOrderMenu : tabOrder; // Use correct state
        if (newTabOrder.Count > 0)
        {
            newTabOrder[0].Select(); // Select first element of the correct tab order
        }
    }
}
