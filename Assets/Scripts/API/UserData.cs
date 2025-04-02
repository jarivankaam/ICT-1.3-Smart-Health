using System;

public class UserData
{
    // User Data
    public Guid UserID { get; set; }
    public Guid IdentityUserID { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Email { get; set; }

    // User Settings
    public string? ProfilePhotoPath { get; set; }
    public string DisplayName { get; set; }

    // User Dairy
    public Guid DairyId { get; set; }
    public string DairyContent { get; set; }

    // User TimeLine Data
    public Guid TimeLineId { get; set; }
    public string TimeLineName { get; set; }
    public bool TimeLineRoute { get; set; }
} 