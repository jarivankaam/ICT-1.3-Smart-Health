using UnityEngine;
using UnityEngine.SceneManagement;

public class Manual : MonoBehaviour
{
    public void CloseManual()
    {
        SceneManager.LoadScene(0);
        Debug.Log("loading main scene");
    }
}
