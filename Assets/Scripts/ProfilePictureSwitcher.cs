using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System;

public class ProfilePictureSwitcher : MonoBehaviour
{
    public RawImage profileRawImage;
    private string currentProfilePicturePath;
    private int chosenImageNumber;
    private string imagesFolder;
    private UserData user = APIClient.Instance.User;
    private List<string> fileNames = new List<string>()
        {
        "Black brown girl.png",
        "Black brown man.png",
        "Black brown woman.png",
        "Blonde white kid.png",
        "Blonde white woman.png",
        "Brown child.png",
        "Brown white boy.png",
        "Brown white man.png",
        "child.png",
        "Ginger white man.png"
        };

    public void Start()
    {
        string sourceFolder = Application.dataPath + "/Art/Profile Pictures/";
        string destinationFolder = Application.persistentDataPath + "/ProfilePictures/";
        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }

        foreach (string fileName in fileNames)
        {
            string sourcePath = Path.Combine(sourceFolder, fileName);
            string destinationPath = Path.Combine(destinationFolder, fileName);

            if (File.Exists(sourcePath) && !File.Exists(destinationPath))
            {
                File.Copy(sourcePath, destinationPath);
                Debug.Log("Copied: " + fileName);
            }
        }

        imagesFolder = Application.persistentDataPath + "/ProfilePictures/";
        currentProfilePicturePath = string.IsNullOrEmpty(user.ProfilePhotoPath) ? fileNames[8] : user.ProfilePhotoPath;
        LoadCurrentProfileImage();
    }

    public void BlackBrownGirlProfilePicture() => SetProfilePicture(0);
    public void BlackBrownManProfilePicture() => SetProfilePicture(1);
    public void BlackBrownWomanProfilePicture() => SetProfilePicture(2);
    public void BlondeWhiteKidProfilePicture() => SetProfilePicture(3);
    public void BlondeWhiteWomanProfilePicture() => SetProfilePicture(4);
    public void BrownChildProfilePicture() => SetProfilePicture(5);
    public void BrownWhiteBoyProfilePicture() => SetProfilePicture(6);
    public void BrownWhiteManProfilePicture() => SetProfilePicture(7);
    public void ChildProfilePicture() => SetProfilePicture(8);
    public void GingerWhiteManProfilePicture() => SetProfilePicture(9);

    private async void SetProfilePicture(int index)
    {
        if (index < 0 || index >= fileNames.Count)
        {
            Debug.LogError("Ongeldige index voor profielfoto!");
            return;
        }

        chosenImageNumber = index;
        currentProfilePicturePath = fileNames[index];
        await APIClient.Instance.PutChangeUserData(currentProfilePicturePath);
        user.ProfilePhotoPath = currentProfilePicturePath;
        LoadImage();
    }

    private void LoadImage()
    {
        if (!Directory.Exists(imagesFolder))
        {
            Debug.LogError("Profile pictures folder not found: " + imagesFolder);
            return;
        }

        string[] files = Directory.GetFiles(imagesFolder, "*.png");

        if (files.Length == 0)
        {
            Debug.LogError("No images found in: " + imagesFolder);
            return;
        }

        Array.Sort(files, StringComparer.OrdinalIgnoreCase);

        if (chosenImageNumber < 0 || chosenImageNumber >= files.Length)
        {
            Debug.LogWarning("ChosenImageNumber out of range, defaulting to first image.");
            chosenImageNumber = 0;
        }

        string chosenFilePath = files[chosenImageNumber];

        try
        {
            byte[] imageBytes = File.ReadAllBytes(chosenFilePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);
            profileRawImage.texture = texture;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error loading image: " + ex.Message);
        }
    }

    private void LoadCurrentProfileImage()
    {
        if (string.IsNullOrEmpty(currentProfilePicturePath))
        {
            currentProfilePicturePath = fileNames[8];
        }

        string imagePath = Path.Combine(imagesFolder, currentProfilePicturePath);

        if (!File.Exists(imagePath))
        {
            Debug.LogError("Profielafbeelding niet gevonden: " + imagePath);
            return;
        }

        try
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);
            profileRawImage.texture = texture;
        }
        catch (Exception ex)
        {
            Debug.LogError("Fout bij laden afbeelding: " + ex.Message);
        }
    }
}
