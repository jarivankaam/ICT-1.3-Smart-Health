using TMPro;
using UnityEngine;

public class RouteSwitcher : MonoBehaviour
{
    public TMP_Text chosenRoute;
    public GameObject RouteA;
    public GameObject RouteB;
    private UserData User = APIClient.Instance.User;

    public void Start()
    {
        if (User.TimeLineRoute)
        {
            chosenRoute.text = "Route B";
            RouteB.SetActive(true);
            RouteA.SetActive(false);
        }
        else
        {
            chosenRoute.text = "Route A";
            RouteA.SetActive(true);
            RouteB.SetActive(false);
        }
    }

    public async void ChangeRoute()
    {
        if (!User.TimeLineRoute)
        {
            chosenRoute.text = "Route B";
            RouteB.SetActive(true);
            RouteA.SetActive(false);
            User.TimeLineRoute = true;
            await APIClient.Instance.PutUpdateTimeLine();
        }
        else
        {
            chosenRoute.text = "Route A";
            RouteA.SetActive(true);
            RouteB.SetActive(false);
            User.TimeLineRoute = false;
            await APIClient.Instance.PutUpdateTimeLine();
        }
    }
}
