using TMPro;
using UnityEngine;
using System.Threading.Tasks;

public class Diary : MonoBehaviour
{
    public TMP_InputField diaryInput;
    private UserData user = APIClient.Instance.User;

    void Start()
    {
        diaryInput.text = user.DairyContent;
    }

    public void DeleteDiary()
    {
        user.DairyContent = "";
        diaryInput.text = user.DairyContent;
        APIClient.Instance.PutChangeDairy(user.DairyContent);
    }

    public void SaveDiary()
    {
        user.DairyContent = diaryInput.text;
        APIClient.Instance.PutChangeDairy(user.DairyContent);
    }
}
