using TMPro;
using UnityEngine;

public class RouteSwitcher : MonoBehaviour
{
    public TMP_Text chosenRoute;
    public bool RouteA = true;
    public void ChangeRoute()
    {
        if (RouteA)
        {
            chosenRoute.text = "Route B";
            RouteA = false;
        }
        else
        {
            chosenRoute.text = "Route A";
            RouteA = true;
        }
    }
}
