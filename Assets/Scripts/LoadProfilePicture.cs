using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class LoadProfilePicture : MonoBehaviour
{
    private string currentProfilePicturePath = APIClient.Instance.User.ProfilePhotoPath;
    private string imagesFolder;
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
            }
        }

        if (string.IsNullOrEmpty(currentProfilePicturePath))
        {
            currentProfilePicturePath = fileNames[8];
        }

        imagesFolder = Application.persistentDataPath + "/ProfilePictures/";
        LoadCurrentProfileImage();
    }
    public void Update()
    {
        currentProfilePicturePath = APIClient.Instance.User.ProfilePhotoPath;
        LoadCurrentProfileImage();

    }
    public void LoadCurrentProfileImage()
    {
        byte[] imageBytes = File.ReadAllBytes(imagesFolder + currentProfilePicturePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);
        GetComponentInChildren<RawImage>().texture = texture;
    }
}
