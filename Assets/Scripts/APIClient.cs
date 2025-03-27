using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class APIClient : MonoBehaviour
{
    string token;
    string Email;
    public static APIClient Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    public async void Register(string email, string password)
    {
        Email = email;
        var request = new PostRegisterRequestDto()
        {
            email = email,
            password = password
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall("https://localhost:7032/account/register", "POST", jsondata);
        var login = await Login(email, password);
        if (login == null)
        {
            Debug.Log("Login failed");
        }

    }
    public async Task<string> Login(string email, string password)
    {
        if (Email != email)
        {
            Email = email;
        }

        var request = new PostLoginRequestDto()
        {
            email = email,
            password = password
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall("https://localhost:7032/account/login", "POST", jsondata);
        var responseDto = JsonUtility.FromJson<PostLoginResponseDto>(response);
        if (responseDto != null)
        {
            token = responseDto.accessToken;
            SceneManager.LoadScene("WorldSelection");
            return "Succes";
        }
        else
        {
            return null;
        }
    }
    public async Task Logout()
    {
        var request = new PostLogoutRequestDto()
        {
            Email = Email
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall("https://localhost:7032/account/logout", "POST", jsondata, token);
        if (response != null)
        {
            token = "";
            SceneManager.LoadScene("StartScreen");
        }
    }
    private async Task<string> PerformApiCall(string url, string method, string jsonData = null, string token = null)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, method))
        {
            if (!string.IsNullOrEmpty(jsonData))
            {
                byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            if (!string.IsNullOrEmpty(token))
            {
                request.SetRequestHeader("Authorization", "Bearer " + token);
            }

            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("API-aanroep is successvol: ");

                return request.downloadHandler.text;

            }
            else
            {
                if (request.error == "HTTP/1.1 400 Bad Request")
                {

                    Debug.Log("Request Not good");
                }
                else if (request.error == "HTTP/1.1 401 Unauthorized")
                {
                    Debug.Log("Not Authorized");
                    Debug.Log(token);

                }
                else
                {
                    Debug.Log("API-aanroep Failed: " + request.error);
                }
                return null;
            }
        }
    }
}
