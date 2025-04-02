using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System;
using Unity.Burst.Intrinsics;
using UnityEngine.Profiling;

public class ProfilePictureSwitcher : MonoBehaviour
{

    public RawImage profileRawImage;
    public string currentProfilePicturePath;
    public int chosenImageNumber;
    private string imagesFolder;
    private UserData user = APIClient.Instance.User;
    List<string> fileNames = new List<string>()
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

    void Start()
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
        if (string.IsNullOrEmpty(user.ProfilePhotoPath))
        {
            currentProfilePicturePath = fileNames[8];
            Debug.Log($"{fileNames[8]}");
            Debug.Log($"{currentProfilePicturePath}");
        }
        APIClient.Instance.PutChangeUserData(currentProfilePicturePath);
        LoadCurrentProfileImage();  
    }
    public void BlackBrownManProfilePicture()
    {
        chosenImageNumber = 1;
        currentProfilePicturePath = fileNames[1];
        APIClient.Instance.PutChangeUserData(currentProfilePicturePath);
        LoadImage();
    }
    public void BlackBrownWomanProfilePicture()
    {
        chosenImageNumber = 2;
        currentProfilePicturePath = fileNames[2];
        APIClient.Instance.PutChangeUserData(currentProfilePicturePath);
        LoadImage();
    }
    public void BlondeWhiteKidProfilePicture()
    {
        chosenImageNumber = 3;
        currentProfilePicturePath = fileNames[3];
        APIClient.Instance.PutChangeUserData(currentProfilePicturePath);
        LoadImage();
    }
    public void BlondeWhiteWomanProfilePicture()
    {
        chosenImageNumber = 4;
        currentProfilePicturePath = fileNames[4];
        APIClient.Instance.PutChangeUserData(currentProfilePicturePath);
        LoadImage();
    }
    public void BrownWhiteBoyProfilePicture()
    {
        chosenImageNumber = 6;
        currentProfilePicturePath = fileNames[6];
        APIClient.Instance.PutChangeUserData(currentProfilePicturePath);
        LoadImage();
    }
    public void BrownWhiteManProfilePicture()
    {
        chosenImageNumber = 7;
        currentProfilePicturePath = fileNames[7];
        APIClient.Instance.PutChangeUserData(currentProfilePicturePath);
        LoadImage();
    }
    public void GingerWhiteManProfilePicture()
    {
        chosenImageNumber = 9;
        currentProfilePicturePath = fileNames[9];
        APIClient.Instance.PutChangeUserData(currentProfilePicturePath);
        LoadImage();
    }
    public void BlackBrownGirlProfilePicture()
    {
        chosenImageNumber = 0;
        currentProfilePicturePath = fileNames[0];
        APIClient.Instance.PutChangeUserData(currentProfilePicturePath);
        LoadImage();
    }
    public void ChildProfilePicture()
    {
        chosenImageNumber = 8;
        currentProfilePicturePath = fileNames[8];
        APIClient.Instance.PutChangeUserData(currentProfilePicturePath);
        LoadImage();
    }
    public void BrownChildProfilePicture()
    {
        chosenImageNumber = 5;
        currentProfilePicturePath = fileNames[5];
        APIClient.Instance.PutChangeUserData(currentProfilePicturePath);
        LoadImage();
    }
    public void LoadImage()
    {
        if (!Directory.Exists(imagesFolder))
        {
            Debug.LogError("Profile pictures folder not found!");
            return;
        }

        string[] files = Directory.GetFiles(imagesFolder, "*.png"); //gets all files and puts them in a string
        Array.Sort(files, StringComparer.OrdinalIgnoreCase); // orders the files in alfabetical order

        string ChosenFilePath = files[chosenImageNumber];    // choose which picture you want to show
        byte[] imageBytes = File.ReadAllBytes(ChosenFilePath); //reads the files and changes into bytes
        Texture2D texture = new Texture2D(2, 2);   //initialize the texture
        texture.LoadImage(imageBytes);   // puts the bytes into the texture
        profileRawImage.texture = texture;  // puts the texture into the image
    }
    public void LoadCurrentProfileImage()
    {
        Debug.Log($"{currentProfilePicturePath}");
        byte[] imageBytes = File.ReadAllBytes(imagesFolder + currentProfilePicturePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);
        profileRawImage.texture = texture;
    }
}
