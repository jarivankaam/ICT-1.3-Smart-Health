using UnityEngine;
using UnityEngine.SceneManagement;

public class Manual : MonoBehaviour
{
    private bool _accessTokenNotNull;

    public void Start()
    {
        _accessTokenNotNull = string.IsNullOrEmpty(APIClient.Instance.GetAccessToken()) ? false : true;
    }
    public void CloseManual()
    {
        if (!_accessTokenNotNull)
        {
            SceneManager.LoadScene("StartScreen");
            Debug.Log("loading main scene");
        }
        else
        {
            SceneManager.LoadScene("BigTimeline");
            Debug.Log("loading Timeline");
        }
    }
}
