using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class RouteSwitcher : MonoBehaviour
{
    public TMP_Text chosenRoute;
    public GameObject RouteA;
    public GameObject RouteB;
    public bool isRouteA = true;
    public void Start()
    {
        RouteA.SetActive(true);
        RouteB.SetActive(false);
    }
    public void ChangeRoute()
    {
        if (isRouteA)
        {
            chosenRoute.text = "Route B";
            isRouteA = false;
            RouteB.SetActive(true);
            RouteA.SetActive(false);
        }
        else
        {
            chosenRoute.text = "Route A";
            isRouteA = true;
            RouteA.SetActive(true);
            RouteB.SetActive(false);
        }
    }
}
