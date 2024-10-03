using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;


public class test : MonoBehaviour
{
    public TMP_Text debugTextField;
    // Start is called before the first frame update
    void Start()
    {
        if (Input.mousePosition.x < Screen.width * 2 / 3)
        {
            // Pick a PNG image from Gallery/Photos
            // If the selected image's width and/or height is greater than 512px, down-scale the image
            PickImage(512);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PickImage(int maxSize)
    {
        Texture2D texture = NativeGallery.LoadImageAtPath("/storage/emulated/0/DCIM/Camera/20240203_231739.jpg", maxSize);


        if (texture == null)
        {
            Debug.Log("Couldn't load texture from ");
            return;
        }

                // Assign texture to a temporary quad and destroy it after 5 seconds
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
        quad.transform.forward = Camera.main.transform.forward;
        quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

        Material material = quad.GetComponent<Renderer>().material;
        if (!material.shader.isSupported) // happens when Standard shader is not included in the build
            material.shader = Shader.Find("Legacy Shaders/Diffuse");

            material.mainTexture = texture;

            Destroy(quad, 5f);

                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
            Destroy(texture, 5f);
           
    }

    private void PickImage2(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            debugTextField.text = path;
            if (path != null)
            {
                // Create Texture from selected image
                //Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                Texture2D texture = NativeGallery.LoadImageAtPath("/storage/emulated/0/DCIM/Camera/20240203_231739.jpg", maxSize);




                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                // Assign texture to a temporary quad and destroy it after 5 seconds
                GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                quad.transform.forward = Camera.main.transform.forward;
                quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                Material material = quad.GetComponent<Renderer>().material;
                if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                    material.shader = Shader.Find("Legacy Shaders/Diffuse");

                material.mainTexture = texture;

                Destroy(quad, 5f);

                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
                Destroy(texture, 5f);
            }
        });

        Debug.Log("Permission result: " + permission);
    }
}
