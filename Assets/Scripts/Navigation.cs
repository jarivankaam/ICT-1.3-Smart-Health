using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void ChangeSceneEditProfile()
    {
        SceneManager.LoadScene("");
    }
    public void ChangeSceneManual()
    {
        SceneManager.LoadScene("ManualScreen");
    }
    public void ChangeSceneDiary()
    {
        SceneManager.LoadScene("");
    }
    public void LogOut()
    {
        //code voor Logout
    }
    public void ChangeSceneAgenda()
    {
        SceneManager.LoadScene("Agenda Scene");
    }
    public void ChangeSceneBigTimeLine()
    {
        SceneManager.LoadScene("BigTimeline");
    }
}
