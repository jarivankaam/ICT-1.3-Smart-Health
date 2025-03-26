using UnityEngine;
using UnityEngine.SceneManagement;

public class Manual : MonoBehaviour
{
    private bool accesTokenNotNull = false;
    public void CloseManual()
    {
        if (!accesTokenNotNull)
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
