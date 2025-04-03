using TMPro;
using UnityEngine;

public class RouteSwitcherMenu : MonoBehaviour
{
    string currentRoute;
    private UserData user = APIClient.Instance.User;
    

    void Start()
    {
        
    }

 
    void Update()
    {
        if (user.TimeLineRoute)
        {
            currentRoute = "Route B";
        }
        else
        {
            currentRoute = "Route A";
        }
        GetComponentInChildren<TextMeshPro>().text = currentRoute;
    }
}
