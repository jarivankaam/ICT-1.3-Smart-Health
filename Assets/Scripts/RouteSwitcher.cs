using JetBrains.Annotations;
using System.Threading.Tasks;
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

    public void ChangeRoute()
    {
        if (!User.TimeLineRoute)
        {
            chosenRoute.text = "Route B";
            RouteB.SetActive(true);
            RouteA.SetActive(false);
            User.TimeLineRoute = true;
            APIClient.Instance.PutUpdateTimeLine();
        }
        else
        {
            chosenRoute.text = "Route A";
            RouteA.SetActive(true);
            RouteB.SetActive(false);
            User.TimeLineRoute = false;
            APIClient.Instance.PutUpdateTimeLine();
        }
    }
}
