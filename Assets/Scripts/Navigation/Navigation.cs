using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public UserData user = APIClient.Instance.User;
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
    public async void LogOut()
    {
        await APIClient.Instance.Logout();
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
        user.CurrentStep = 1;
    }
    public void AtTheDoctorVideo()
    {
        SceneManager.LoadScene("BijDeArts(Video)");
        user.CurrentStep = 2;
    }
    public void Cast()
    {
        SceneManager.LoadScene("TimelineitemChoiceTablet"); // gips
        if (user.TimeLineRoute)
        {
            user.CurrentStep = 4;
        }
        else
        {
            user.CurrentStep = 3;
        }
    }
    public void CastText()
    {
        SceneManager.LoadScene("Gips(Text)");
        if (user.TimeLineRoute)
        {
            user.CurrentStep = 4;
        }
        else
        {
            user.CurrentStep = 3;
        }
    }
    public void CastVideo()
    {
        SceneManager.LoadScene("Gips(Video)");
        if (user.TimeLineRoute)
        {
            user.CurrentStep = 4;
        }
        else
        {
            user.CurrentStep = 3;
        }
    }
    public void OperationVideo()
    {
        SceneManager.LoadScene("Operatie(Video)");
        user.CurrentStep = 3;
    }
    public void Medication()
    {
        SceneManager.LoadScene("TimelineItemMedicine");
        if (user.TimeLineRoute)
        {
            user.CurrentStep = 5;
        }
        else
        {
            user.CurrentStep = 4;
        }
    }
    public void AfterCareVideo()
    {
        SceneManager.LoadScene("Nazorg(Video)");
        if (user.TimeLineRoute)
        {
            user.CurrentStep = 6;
        }
        else
        {
            user.CurrentStep = 5;
        }
    }
    public void CheckinAtTheDoctorVideo()
    {
        SceneManager.LoadScene("ControleBijDeArts(Video)");
        if (user.TimeLineRoute)
        {
            user.CurrentStep = 7;
        }
        else
        {
            user.CurrentStep = 6;
        }
    }
    public void CheckinText()
    {
        SceneManager.LoadScene("Controle(Text)");
        user.CurrentStep = 1;
    }
    public void AtTheDoctorText()
    {
        SceneManager.LoadScene("BijDeArts(Text)");
        user.CurrentStep = 2;
    }
    public void OperationText()
    {
        SceneManager.LoadScene("Operatie(Text)");
        user.CurrentStep = 3;
    }
    public void AfterCareText()
    {
        SceneManager.LoadScene("Nazorg(Text)");
        if (user.TimeLineRoute)
        {
            user.CurrentStep = 6;
        }
        else
        {
            user.CurrentStep = 5;
        }
    }
    public void CheckinAtTheDoctorText()
    {
        SceneManager.LoadScene("ControleBijDeArts(Text)");
        if (user.TimeLineRoute)
        {
            user.CurrentStep = 7;
        }
        else
        {
            user.CurrentStep = 6;
        }
    }
    public void EndPage()
    {
        SceneManager.LoadScene("EndScreen");
    }
    public void AtTheDocterToNextPageVideo()
    {
        if (user.TimeLineRoute)
        {
            SceneManager.LoadScene("Operatie(Video)");
            user.CurrentStep = 3;
        }
        else
        {
            SceneManager.LoadScene("Gips(Video)");
            user.CurrentStep = 3;
        }
    }
    public void AtTheDocterToNextPageText()
    {
        if (user.TimeLineRoute)
        {
            SceneManager.LoadScene("Operatie(Text)");
            user.CurrentStep = 3;
        }
        else
        {
            SceneManager.LoadScene("Gips(Text)");
            user.CurrentStep = 3;
        }
    }
    public void CastLastPageText()
    {
        var user = APIClient.Instance.User;
        if (user.TimeLineRoute)
        {
            SceneManager.LoadScene("Operatie(Text)");
            user.CurrentStep = 3;
        }
        else
        {
            SceneManager.LoadScene("BijDeArts(Text)");
            user.CurrentStep = 2;
        }
    }
    public void CastLastPageVideo()
    {
        var user = APIClient.Instance.User;
        if (user.TimeLineRoute)
        {
            SceneManager.LoadScene("Operatie(Video)");
            user.CurrentStep = 3;
        }
        else
        {
            SceneManager.LoadScene("BijDeArts(Video)");
            user.CurrentStep = 2;
        }
    }

}
