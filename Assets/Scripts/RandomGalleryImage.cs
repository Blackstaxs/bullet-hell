using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using TMPro;
using System.Linq;
using UnityEngine.Android;


public class RandomGalleryImage : MonoBehaviour
{
    public RawImage imageDisplay;
    public TMP_Text debugTextField;
    string[] files = null;
    public GameObject transparentOverlay;
    private List<string> galleryImages = new List<string>();

    void Start()
    {
        /*
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
            Permission.RequestUserPermission(Permission.Camera);
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        */
        //PickImageFromGallery();
        //debugTextField.text = "started";
        LoadGalleryImages();
        //SelectRandomImage();
    }

    public void PickImageFromGallery()
    {
        transparentOverlay.SetActive(true);
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent", intentClass.GetStatic<string>("ACTION_PICK"));

        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "content://media/internal/images/media");

        intentObject.Call<AndroidJavaObject>("setDataAndType", uriObject, "image/*");

        currentActivity.Call("startActivityForResult", intentObject, 0);
    }

    void LoadGalleryImages()
    {
        //string galleryPath = Path.Combine(Application.persistentDataPath, "DCIM/Camera");
        string galleryPath = GetAndroidExternalStoragePath();

        debugTextField.text = "load 1";
        if (!string.IsNullOrEmpty(galleryPath))
        {
            string[] imagePaths = Directory.GetFiles(galleryPath, "*.jpg");
            if (imagePaths.Length > 0)
            {
                debugTextField.text = "not zero";
            }
            else
            {
                debugTextField.text = "zer1o";
            }
            galleryImages.AddRange(imagePaths);
            //debugTextField.text = galleryPath;
        }
        else
        {
            Debug.LogError("Gallery directory not found.");
        }
        
    }

    private string GetAndroidExternalStoragePath()
    {
        
        if (Application.platform != RuntimePlatform.Android)
            return Application.persistentDataPath;

        var jc = new AndroidJavaClass("android.os.Environment");
        var path = jc.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory",
            jc.GetStatic<string>("DIRECTORY_DCIM"))
            .Call<string>("getAbsolutePath");
        return path;
        
    }


    void SelectRandomImage()
    {
        //debugTextField.text = "select 1";
        if (galleryImages.Count > 0)
        {
            debugTextField.text = "select 2";
            // Choose a random image from the list
            string randomImagePath = galleryImages[Random.Range(0, galleryImages.Count)];

            // Load the image and display it
            byte[] imageData = File.ReadAllBytes(randomImagePath);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(imageData);
            imageDisplay.texture = texture;
        }
        else
        {
            Debug.LogError("No camera-captured images found.");
        }
    }
}

