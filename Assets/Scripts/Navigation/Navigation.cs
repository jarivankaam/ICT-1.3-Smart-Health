using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void ChangeSceneEditProfile()
    {
        SceneManager.LoadScene("EditProfile");
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
        SceneManager.LoadScene("StartScreen");
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
    // Timeline Navigation Methods
    public void Checkin()
    {
        SceneManager.LoadScene("Timelineitem1(Video)");
    }
    public void AtTheDoctor()
    {
        SceneManager.LoadScene("Timelineitem1(Video)");
    }
    public void Cast()
    {
        SceneManager.LoadScene("TimelineitemChoiceTablet");
    }
    public void Operation()
    {
        SceneManager.LoadScene("Timelineitem1(Video)");
    }
    public void Medication()
    {
        SceneManager.LoadScene("TimelineitemMedicine");
    }
    public void AfterCare()
    {
        SceneManager.LoadScene("Timelineitem1(Video)");
    }
    public void CheckinAtTheDoctor()
    {
        SceneManager.LoadScene("Timelineitem1(Video)");
    }
    public void SwitchToVideo()
    {
        SceneManager.LoadScene("Timelineitem1(Video)");
    }
    public void SwitchToText()
    {
        SceneManager.LoadScene("Timelineitem1(Text)");
    }
}
