using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class TabFunctionTimelineScene : MonoBehaviour
{
    private EventSystem system;

    public List<Selectable> tabOrderA;
    public List<Selectable> tabOrderB;
    public List<Selectable> tabOrderMenu;
    public Button submitButton;

    public enum PageState { 
        RouteA, 
        RouteB, 
        Menu 
    }

    private PageState currentState;
    private PageState previousState = PageState.RouteA; 

    private bool hasTabbed = false;

    public void Start()
    {
        system = EventSystem.current;
        currentState = APIClient.Instance.User.TimeLineRoute ? PageState.RouteB : PageState.RouteA;
    }

    public void Update()
    {
        List<Selectable> activeTabOrder = GetActiveTabOrder();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            if (system.currentSelectedGameObject == null && activeTabOrder.Count > 0)
            {
                activeTabOrder[0].Select();
                hasTabbed = true;
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

    //Determine which tab order is active
    private List<Selectable> GetActiveTabOrder()
    {
        switch (currentState)
        {
            case PageState.RouteA:
                return tabOrderA;
            case PageState.RouteB:
                return tabOrderB;
            case PageState.Menu:
                return tabOrderMenu;
            default:
                return tabOrderA;
        }
    }

    //Switch between Route A & Route B
    public void ToggleRoute()
    {
        if (currentState == PageState.RouteA)
        {
            currentState = PageState.RouteB;
        }
        else if (currentState == PageState.RouteB)
        {
            currentState = PageState.RouteA;
        }

        hasTabbed = false;
        Debug.Log("Switched to: " + currentState);

        //Clear current selection to force refresh
        system.SetSelectedGameObject(null);

        //Ensure tabbing works immediately after switching
        StartCoroutine(DelayedSelect());
    }

    //Toggle Menu On/Off
    public void ToggleMenu()
    {
        if (currentState == PageState.Menu)
        {
            //Close Menu and Restore Previous Route
            currentState = previousState;
        }
        else
        {
            //Save Current State Before Opening Menu
            previousState = currentState;
            currentState = PageState.Menu;
        }

        hasTabbed = false;
        Debug.Log("Menu Active: " + (currentState == PageState.Menu));

        system.SetSelectedGameObject(null); //Clear focus first
        StartCoroutine(DelayedSelect());    //Restore focus with a delay
    }

    //Ensure focus is properly restored after closing the menu
    private IEnumerator DelayedSelect()
    {
        yield return new WaitForEndOfFrame(); // Small delay to prevent double input issues

        List<Selectable> newTabOrder = GetActiveTabOrder();

        if (newTabOrder.Count > 0)
        {
            //Ensure the first element of the new tab order is selected
            newTabOrder[0].Select();
        }
    }
}