using TMPro;
using UnityEngine;

public class Diary : MonoBehaviour
{
    public TMP_InputField diaryInput;
    private UserData user = APIClient.Instance.User;

    public void Start()
    {
        diaryInput.text = user.DairyContent;
    }

    public async void SaveDiary()
    {
        user.DairyContent = diaryInput.text;
        await APIClient.Instance.PutChangeDairy(user.DairyContent);
    }
}
