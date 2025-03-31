using TMPro;
using UnityEngine;

public class Diary : MonoBehaviour
{
    public TMP_InputField diaryInput;
    private static string savedDiaryText = "";

    void Start()
    {
        diaryInput.text = savedDiaryText;
    }

    public void DeleteDiary()
    {
        savedDiaryText = "";
        diaryInput.text = savedDiaryText;
    }

    public void SaveDiary()
    {
        savedDiaryText = diaryInput.text;
    }
}
