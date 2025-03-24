using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPageFunctions : MonoBehaviour
{
    public void RedirectToRegisterScreen() => SceneManager.LoadScene("RegisterScreen");
    public void RedirectToManualScreen() => SceneManager.LoadScene("ManualScreen");
}
