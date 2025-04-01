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
        SceneManager.LoadScene("Diary");
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
    public void CheckinVideo()
    {
        SceneManager.LoadScene("Controle(Video)");
    }
    public void AtTheDoctorVideo()
    {
        SceneManager.LoadScene("BijDeArts(Video)");
    }
    public void Cast()
    {
        SceneManager.LoadScene("TimelineitemChoiceTablet");
    }
    public void CastText()
    {
        SceneManager.LoadScene("Gips(Text)");
    }
    public void CastVideo()
    {
        SceneManager.LoadScene("Gips(Video)");
    }
    public void OperationVideo()
    {
        SceneManager.LoadScene("Operatie(Video)");
    }
    public void Medication()
    {
        SceneManager.LoadScene("TimelineitemMedicine");
    }
    public void AfterCareVideo()
    {
        SceneManager.LoadScene("Nazorg(Video)");
    }
    public void CheckinAtTheDoctorVideo()
    {
        SceneManager.LoadScene("ControleBijDeArts(Video)");
    }
    public void CheckinText()
    {
        SceneManager.LoadScene("Controle(Text)");
    }
    public void AtTheDoctorText()
    {
        SceneManager.LoadScene("BijDeArts(Text)");
    }
    public void OperationText()
    {
        SceneManager.LoadScene("Operatie(Text)");
    }
    public void AfterCareText()
    {
        SceneManager.LoadScene("Nazorg(Text)");
    }
    public void CheckinAtTheDoctorText()
    {
        SceneManager.LoadScene("ControleBijDeArts(Text)");
    }

}
