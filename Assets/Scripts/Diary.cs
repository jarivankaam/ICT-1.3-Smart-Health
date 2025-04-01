using TMPro;
using UnityEngine;
using System.Threading.Tasks;

public class Diary : MonoBehaviour
{
    public TMP_InputField diaryInput;
    private static string SavedDiaryText = "";

    void Start()
    {
        diaryInput.text = SavedDiaryText;
        //MakeDiary();
    }

    public void DeleteDiary()
    {
        SavedDiaryText = "";
        diaryInput.text = SavedDiaryText;
    }

    public void SaveDiary()
    {
        SavedDiaryText = diaryInput.text;
    }


    //public async task SaveDiaryToDatabase(string savedDiaryText)
    //{
        //savedDiaryText = SavedDiaryText;
        //var request = new PostRegisterRequestDto()
        //{
        //    userId = userId,
        //    savedDiaryText = savedDiaryText,
        //};
        //var jsondata = JsonUtility.ToJson(request);
        //var response = await PerformApiCall("https://localhost:7032/api/Dairy/:id", "PUT", jsondata);
    //}

    //public async Task MakeDiary() //maakt diary 
    //{
    //    //string MakeDiaryText = "";
    //    //var request = new PostRegisterRequestDto()
    //    //{
    //    //    userId = userId,
    //    //    MakeDiaryText = MakeDiaryText,
    //    //};
    //    //var jsondata = JsonUtility.ToJson(request);
    //    //var response = await PerformApiCall("https://localhost:7032/api/Dairy", "POST", jsondata);
    //}
}
