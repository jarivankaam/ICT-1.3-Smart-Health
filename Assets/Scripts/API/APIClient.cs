using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class APIClient : MonoBehaviour
{
    [SerializeField] private string _baseUrl = "https://localhost:7109";
    private UserData _user = new UserData();
    public string GetAccessToken() => _user.AccessToken;
    public string GetRefreshToken() => _user.RefreshToken;
    public string GetEmail() => _user.Email;
    public static APIClient Instance { get; private set; }

    // Prevents destroying the ApiClient instance
    public void Awake()
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

    // Registering a new (auth) and (dbo) user
    public async Task Register(string email, string password)
    {
        var request = new PostRegisterRequestDto()
        {
            email = email,
            password = password
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall($"{_baseUrl}/custom/Auth/register", "POST", jsondata);

        if (response == null)
        {
            Debug.Log("Failed to register a new user");
            return;
        }

        await Login(email, password);
    }

    // Login
    public async Task Login(string email, string password)
    {
        var request = new PostLoginRequestDto()
        {
            email = email,
            password = password
        };

        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall($"{_baseUrl}/auth/login", "POST", jsondata);
        var responseDto = JsonUtility.FromJson<PostLoginResponseDto>(response);

        // Filling all user data
        if (responseDto != null)
        {
            _user.AccessToken = responseDto.accessToken;
            _user.RefreshToken = responseDto.refreshToken;
            _user.Email = email;
            await GetIdentityUserID();
            await GetUserData();
        }
    }

    // Get Identity User ID
    public async Task GetIdentityUserID()
    {
        var response = await PerformApiCall($"{_baseUrl}/api/User/CurrentUser", "GET", null);
        
        if (response == null)
        {
            Debug.Log("Failed to get Identity User ID");
        }

        _user.IdentityUserID = Guid.Parse(response);
    }

    // Get User ID
    public async Task GetUserData()
    {
        var request = new PostIdentityUserIDRequestDto()
        {
            IdentityUserID = _user.IdentityUserID,
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall($"{_baseUrl}/api/User", "GET", jsondata);
        var responseDto = JsonUtility.FromJson<PostIdentityUserIDResponseDto>(response);

        if (responseDto != null)
        {
            Debug.Log("Failed to get all user data");
            _user.UserID = Guid.Parse(responseDto.ID);
            _user.DisplayName = responseDto.DisplayName;
            _user.ProfilePhotoPath = responseDto.ProfilePhotoPath;
        }
    }

    // Logout the user
    public async Task Logout()
    {
        var request = new PostLogoutRequestDto()
        {
            Email = _user.Email
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall($"{_baseUrl}/auth/logout", "POST", jsondata, _user.AccessToken);

        _user.AccessToken = null;
        _user.RefreshToken = null;
        _user.Email = null;

        SceneManager.LoadScene("StartScreen");
    }

    // API call
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
