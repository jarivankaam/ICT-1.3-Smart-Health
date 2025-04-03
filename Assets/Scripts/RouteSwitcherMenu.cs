using TMPro;
using UnityEngine;

public class RouteSwitcherMenu : MonoBehaviour
{
    string currentRoute;

    public void Start()
    {
        currentRoute = APIClient.Instance.User.TimeLineRoute ? "B" : "A";
    }
 
    public void Update()
    {
        currentRoute = APIClient.Instance.User.TimeLineRoute ? "B" : "A";
        GetComponentInChildren<TMP_Text>().text = $"Smarthealth-app\r\nHuidige route: {currentRoute}";
    }
}
