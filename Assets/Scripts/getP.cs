using TMPro;
using UnityEngine;

public class getP : MonoBehaviour
{
    public GameObject cubeObject1;
    public GameObject cubeObject2;
    public GameObject cubeObject3;

    // Public variable to hold the color to be assigned to the cube
    public Color cubeColor = Color.blue;
    public TMP_Text debugTextField;

    // <meta-data android:name="unityplayer.SkipPermissionsDialog" android:value="true" />

    void Start()
    {
        // Get the MeshRenderer component of the cubeObject
        MeshRenderer renderer1 = cubeObject1.GetComponent<MeshRenderer>();
        MeshRenderer renderer2 = cubeObject2.GetComponent<MeshRenderer>();
        MeshRenderer renderer3 = cubeObject3.GetComponent<MeshRenderer>();
        Debug.Log("Detected platform: " + Application.platform);
        bool testBool = true;

        // Assign the specified color to the cube
        renderer1.material.color = cubeColor;
        Debug.Log("Color assigned to cubeObject1.");
        debugTextField.text = "started";

        // Check if the platform is Android
        if (testBool)
        {
            renderer2.material.color = cubeColor;
            Debug.Log("Color assigned to cubeObject2.");
            debugTextField.text = "testbool";

            // Call the Java method to retrieve the last image path
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            debugTextField.text = "unityplayer";

            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            debugTextField.text = "currentactivity";

            string lastImagePath = currentActivity.Call<string>("GetLastImagePathFromCamera", currentActivity);
            debugTextField.text = "lastImagePath";

            Debug.Log("Attempting to retrieve last image path from Android.");

            // Check if the lastImagePath is not null or empty
            if (!string.IsNullOrEmpty(lastImagePath))
            {
                // Do something with the lastImagePath, such as loading the image into a Texture2D
                renderer3.material.color = cubeColor;
                Debug.Log("Color assigned to cubeObject3.");
            }
            else
            {
                Debug.LogWarning("No image found from the camera.");
            }
        }
        else
        {
            Debug.LogWarning("Camera access is only available on Android platform.");
        }
    }
}

