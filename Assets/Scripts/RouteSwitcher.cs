using JetBrains.Annotations;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class RouteSwitcher : MonoBehaviour
{
    public TMP_Text chosenRoute;
    public GameObject RouteA;
    public GameObject RouteB;
    public bool isRouteB = false;
    public void Start()
    {
        RouteA.SetActive(true);
        RouteB.SetActive(false);
    }
    public void ChangeRoute()
    {
        if (!isRouteB)
        {
            chosenRoute.text = "Route B";
            isRouteB = true;
            RouteB.SetActive(true);
            RouteA.SetActive(false);
            APIClient.Instance.User.TimeLineRoute = isRouteB;
            APIClient.Instance.PutUpdateTimeLine();
        }
        else
        {
            chosenRoute.text = "Route A";
            isRouteB = false;
            RouteA.SetActive(true);
            RouteB.SetActive(false);
            APIClient.Instance.User.TimeLineRoute = isRouteB;
            APIClient.Instance.PutUpdateTimeLine();
        }
    }
}
