using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class APIClient : MonoBehaviour
{
    [SerializeField] private string _baseUrl = "https://avansict20.azurewebsites.net";
    public UserData User = new UserData();
    public string GetAccessToken() => User.AccessToken;
    public string GetRefreshToken() => User.RefreshToken;
    public string GetEmail() => User.Email;
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
            User.AccessToken = responseDto.accessToken;
            User.RefreshToken = responseDto.refreshToken;
            User.Email = email;
            await InitializeUserData();
        }
    }

    // Initialize User Data
    public async Task InitializeUserData()
    {
        // Getting Identity User ID
        User.IdentityUserID = await GetIdentityUserID();

        // Getting User data
        var userData = await GetUserData();
        User.UserID = Guid.Parse(userData.id);
        User.DisplayName = userData.displayName;
        User.ProfilePhotoPath = userData.profilePhotoPath;

        // Getting Dairy Data
        var dairyData = await GetDairyData();

        // If dairy data is found, fill the properties, otherwise create a new dairy
        if (dairyData != null)
        {
            User.DairyId = Guid.Parse(dairyData.id);
            User.DairyContent = dairyData.content;
        }
        else
        {
            var newDairyData = await PostNewDairy();
            User.DairyId = Guid.Parse(newDairyData.id);
            User.DairyContent = newDairyData.content;
        }

        // Getting TimeLine Data
        var timeLineData = await GetTimeLineData();

        // If TimeLine data is found, fill the properties, otherwise create a new TimeLine
        if (timeLineData != null)
        {
            User.TimeLineId = Guid.Parse(timeLineData.id);
            User.TimeLineName = timeLineData.name;
            User.TimeLineRoute = timeLineData.routeType;
        }
        else
        {
            var newTimeLineData = await PostNewTimeLine();
            User.TimeLineId = Guid.Parse(newTimeLineData.id);
            User.TimeLineName = newTimeLineData.name;
            User.TimeLineRoute = newTimeLineData.routeType;
        }

        LogUserData();
    }

    // Method to log all properties of UserData to the console
    public void LogUserData()
    {
        Debug.Log("=== User Data ===");
        Debug.Log($"UserID: {User.UserID}");
        Debug.Log($"IdentityUserID: {User.IdentityUserID}");
        Debug.Log($"AccessToken: {User.AccessToken}");
        Debug.Log($"RefreshToken: {User.RefreshToken}");
        Debug.Log($"Email: {User.Email}");

        Debug.Log("\n=== User Settings ===");
        Debug.Log($"ProfilePhotoPath: {User.ProfilePhotoPath}");
        Debug.Log($"DisplayName: {User.DisplayName}");

        Debug.Log("\n=== User Dairy ===");
        Debug.Log($"DairyId: {User.DairyId}");
        Debug.Log($"DairyContent: {User.DairyContent}");

        Debug.Log("\n=== User TimeLine ===");
        Debug.Log($"TimeLineId: {User.TimeLineId}");
        Debug.Log($"TimeLineName: {User.TimeLineName}");
        Debug.Log($"TimeLineRoute: {User.TimeLineRoute}");
    }

    // Get Identity User ID
    public async Task<Guid> GetIdentityUserID()
    {
        var response = await PerformApiCall($"{_baseUrl}/api/User/CurrentUser", "GET", null, User.AccessToken);

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
        var jsondata = $"\"{User.IdentityUserID}\"";

        var response = await PerformApiCall($"{_baseUrl}/api/User/{User.IdentityUserID}", "GET", jsondata, User.AccessToken);
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
        var response = await PerformApiCall($"{_baseUrl}/api/Dairy/{User.UserID}", "GET", null, User.AccessToken);
        var responseDto = JsonUtility.FromJson<GetDairyDataResponseDto>(response);

        if (responseDto == null)
        {
            Debug.Log("No dairy data found");
        }

        return responseDto;
    }

    // Create new dairy
    public async Task<PostDairyDataResponseDto> PostNewDairy()
    {
        var request = new PostNewDairyDataRequestDto()
        {
            userId = User.UserID.ToString(),
            content = $"{User.DisplayName}'s dagboek"
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall($"{_baseUrl}/api/Dairy", "POST", jsondata, User.AccessToken);
        var responseDto = JsonUtility.FromJson<PostDairyDataResponseDto>(response);

        if (responseDto == null)
        {
            Debug.Log("Failed to create new dairy");
        }

        return responseDto;
    }

    // Change new dairy
    public async Task SaveDairyToDatabase(string DairyContent)
    {
        var request = new PutChangeDairyRequestDto()
        {
            id = User.DairyId.ToString(),
            userId = User.UserID.ToString(),
            content = DairyContent
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall($"{_baseUrl}/api/Dairy/{User.DairyId}", "PUT", jsondata, User.AccessToken);

        if (response == null)
        {
            Debug.Log("Failed to change dairy");
        }
    }

    // Get TimeLine data by the user ID
    public async Task<GetTimeLineDataResponseDto> GetTimeLineData()
    {
        var response = await PerformApiCall($"{_baseUrl}/api/Timeline/{User.UserID}", "GET", null, User.AccessToken);
        var responseDto = JsonUtility.FromJson<GetTimeLineDataResponseDto>(response);

        if (responseDto == null)
        {
            Debug.Log("Failed to get timeline data");
        }
        
        return responseDto;
    }

    // Create new TimeLine
    public async Task<PostNewTimeLineResponseDto> PostNewTimeLine()
    {
        var request = new PostNewTimeLineRequestDto()
        {
            name = $"{User.DisplayName}'s tijdlijn",
            routeType = false,
            userId = User.UserID.ToString()
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall($"{_baseUrl}/api/Timeline", "POST", jsondata, User.AccessToken);
        var responseDto = JsonUtility.FromJson<PostNewTimeLineResponseDto>(response);

        if (responseDto == null)
        {
            Debug.Log("Failed to create new time line");
        }

        return responseDto;
    }
    public async Task PutUpdateTimeLine()
    {
        var request = new PostChangeTimelineRequestDto()
        {
            Id = User.TimeLineId.ToString(),
            name = $"{User.DisplayName}'s tijdlijn",
            routeType = User.TimeLineRoute,
            userId = User.UserID.ToString()
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall($"{_baseUrl}/api/Timeline/{User.TimeLineId}", "POST", jsondata, User.AccessToken);
        var responseDto = JsonUtility.FromJson<PostNewTimeLineResponseDto>(response);

        if (responseDto == null)
        {
            Debug.Log("Failed to create new time line");
        }

    }

    // Logout the user
    public async Task Logout()
    {
        var request = new PostLogoutRequestDto()
        {
            Email = User.Email
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall($"{_baseUrl}/auth/logout", "POST", jsondata, User.AccessToken);

        User.AccessToken = null;
        User.RefreshToken = null;
        User.Email = null;

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
