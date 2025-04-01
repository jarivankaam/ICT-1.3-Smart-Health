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
            await InitializeUserData();
        }
    }

    // Initialize User Data
    public async Task InitializeUserData()
    {
        // Getting Identity User ID
        _user.IdentityUserID = await GetIdentityUserID();

        Debug.Log("=== User Data ===");
        Debug.Log($"IdentityUserID: {_user.IdentityUserID}");
        Debug.Log($"AccessToken: {_user.AccessToken}");
        Debug.Log($"RefreshToken: {_user.RefreshToken}");
        Debug.Log($"Email: {_user.Email}");

        // Getting User data [HIER GEBLEVEN]
        var userData = await GetUserData();
        _user.UserID = Guid.Parse(userData.ID);
        _user.DisplayName = userData.DisplayName;
        _user.ProfilePhotoPath = userData.ProfilePhotoPath;

        // Getting Dairy Data
        var dairyData = await GetDairyData();
        _user.DairyId = Guid.Parse(dairyData.ID);
        _user.DairyContent = dairyData.Content;

        // Getting TimeLine Data
        var timeLineData = await GetTimeLineData();
        _user.TimeLineId = Guid.Parse(timeLineData.ID);
        _user.TimeLineName = timeLineData.Name;
        _user.TimeLineRoute = timeLineData.routeType;

        LogUserData();
    }

    // Method to log all properties of UserData to the console
    public void LogUserData()
    {
        Debug.Log("=== User Data ===");
        Debug.Log($"UserID: {_user.UserID}");
        Debug.Log($"IdentityUserID: {_user.IdentityUserID}");
        Debug.Log($"AccessToken: {_user.AccessToken}");
        Debug.Log($"RefreshToken: {_user.RefreshToken}");
        Debug.Log($"Email: {_user.Email}");

        Debug.Log("\n=== User Settings ===");
        Debug.Log($"ProfilePhotoPath: {_user.ProfilePhotoPath}");
        Debug.Log($"DisplayName: {_user.DisplayName}");

        Debug.Log("\n=== User Dairy ===");
        Debug.Log($"DairyId: {_user.DairyId}");
        Debug.Log($"DairyContent: {_user.DairyContent}");

        Debug.Log("\n=== User TimeLine ===");
        Debug.Log($"TimeLineId: {_user.TimeLineId}");
        Debug.Log($"TimeLineName: {_user.TimeLineName}");
        Debug.Log($"TimeLineRoute: {_user.TimeLineRoute}");
    }

    // Get Identity User ID
    public async Task<Guid> GetIdentityUserID()
    {
        var response = await PerformApiCall($"{_baseUrl}/api/User/CurrentUser", "GET", null, _user.AccessToken);

        if (response == null)
        {
            Debug.Log("Failed to get Identity User ID");
            return Guid.Empty;
        }

        var cleanedResponse = response.Replace("\"", "");
        return Guid.Parse(cleanedResponse);
    }

    // Get User ID
    public async Task<GetUserDataResponseDto> GetUserData()
    {
        var request = new GetUserDataRequestDto()
        {
            IdentityUserID = _user.IdentityUserID
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall($"{_baseUrl}/api/User/{_user.IdentityUserID}", "GET", jsondata, _user.AccessToken);
        var responseDto = JsonUtility.FromJson<GetUserDataResponseDto>(response);

        if (responseDto == null)
        {
            Debug.Log("Failed to get all user data");
        }

        return responseDto;
    }

    // Get Dairy data by the user ID
    public async Task<GetDairyDataResponseDto> GetDairyData()
    {
        var response = await PerformApiCall($"{_baseUrl}/api/Dairy/{_user.UserID}", "GET", null, _user.AccessToken);
        var responseDto = JsonUtility.FromJson<GetDairyDataResponseDto>(response);

        if (responseDto == null)
        {
            Debug.Log("Failed to get dairy data");
        }

        return responseDto;
    }

    // Get TimeLine data by the user ID
    public async Task<GetTimeLineDataResponseDto> GetTimeLineData()
    {
        var response = await PerformApiCall($"{_baseUrl}/api/Timeline/{_user.UserID}", "GET", null, _user.AccessToken);
        var responseDto = JsonUtility.FromJson<GetTimeLineDataResponseDto>(response);

        if (responseDto == null)
        {
            Debug.Log("Failed to get time line data");
        }

        return responseDto;
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
                Debug.Log("API-aanroep is successvol: " + request.downloadHandler.text);

                return request.downloadHandler.text;
            }
            else
            {
                Debug.Log("Fout bij API-aanroep: " + request.error);
                return null;
            }
        }
    }
}
