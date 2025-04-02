using UnityEngine;

public class RouteSwitchTimeline : MonoBehaviour
{
    public bool isRouteB;
    public GameObject RouteA;
    public GameObject RouteB;

    public void Start()
    {
        isRouteB = APIClient.Instance.User.TimeLineRoute;
        if (isRouteB)
        {
            RouteB.SetActive(true);
            RouteA.SetActive(false);
        }
        else
        {
            RouteA.SetActive(true);
            RouteB.SetActive(false);
        }
    }
}
