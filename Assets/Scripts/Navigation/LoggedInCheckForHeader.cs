using UnityEngine;

public class LoggedInCheckForHeader : MonoBehaviour
{
    private bool _accessTokenNotNull;
    public void Start()
    {
        _accessTokenNotNull = string.IsNullOrEmpty(APIClient.Instance.GetAccessToken()) ? false : true;
        gameObject.SetActive(_accessTokenNotNull);
    }
}