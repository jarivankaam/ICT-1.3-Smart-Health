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
    public string currentProfilePicture;
    public int chosenImageNumber;
    private string imagesFolder;
    List<string> fileNames = new List<string>()
        {
        "Black brown man.png",
        "Black brown woman.png",
        "Blonde white kid.png",
        "Blonde white woman.png",
        "Brown white boy.png",
        "Brown white man.png",
        "Ginger white man.png",
        "Black brown girl.png",
        "child.png",
        "Brown child.png"
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
        currentProfilePicture = "child.png";
        LoadCurrentProfileImage();  
    }
    public void BlackBrownManProfilePicture()
    {
        chosenImageNumber = 1;
        LoadImage();
    }
    public void BlackBrownWomanProfilePicture()
    {
        chosenImageNumber = 2;
        LoadImage();
    }
    public void BlondeWhiteKidProfilePicture()
    {
        chosenImageNumber = 3;
        LoadImage();
    }
    public void BlondeWhiteWomanProfilePicture()
    {
        chosenImageNumber = 4;
        LoadImage();
    }
    public void BrownWhiteBoyProfilePicture()
    {
        chosenImageNumber = 6;
        LoadImage();
    }
    public void BrownWhiteManProfilePicture()
    {
        chosenImageNumber = 7;
        LoadImage();
    }
    public void GingerWhiteManProfilePicture()
    {
        chosenImageNumber = 9;
        LoadImage();
    }
    public void BlackBrownGirlProfilePicture()
    {
        chosenImageNumber = 0;
        LoadImage();
    }
    public void ChildProfilePicture()
    {
        chosenImageNumber = 8;
        LoadImage();
    }
    public void BrownChildProfilePicture()
    {
        chosenImageNumber = 5;
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

        string ChosenFilePath = files[chosenImageNumber];    // choose which picture you want to show
        byte[] imageBytes = File.ReadAllBytes(ChosenFilePath); //reads the files and changes into bytes
        Texture2D texture = new Texture2D(2, 2);   //initialize the texture
        texture.LoadImage(imageBytes);   // puts the bytes into the texture
        profileRawImage.texture = texture;  // puts the texture into the image
    }
    public void LoadCurrentProfileImage()
    {
        byte[] imageBytes = File.ReadAllBytes(imagesFolder + currentProfilePicture);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);
        profileRawImage.texture = texture;
    }
}
