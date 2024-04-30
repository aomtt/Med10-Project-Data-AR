using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Globalization;
using LSL;

public class Ping : MonoBehaviour
{
    public int x = 0;
    StreamInfo inf;
    StreamOutlet outl;

    System.Guid guid = System.Guid.NewGuid();
    // URL to Firebase database
    string databaseUrl = "https://med10-106e2-default-rtdb.europe-west1.firebasedatabase.app/";
    // Firebase Web API Key
    string apiKey = "AIzaSyDdOoU54hkYbKTXhRxdMRt9vV58jks58os";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Unity Firebase REST API Test");
    }

    // Method to send data to Firebase using UnityWebRequest


    public void SendAMessage(string info)
    {
        SendData("messages", info);
    }
    public void SendData(string key, string data)
    {
        string json = $"{{\"{key}\":\"{data}\"}}";
        StartCoroutine(PutData($"{databaseUrl}messages.json?auth={apiKey}", json));
    }

    IEnumerator PutData(string url, string json)
    {
        var request = new UnityWebRequest(url, "PUT");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Data sent successfully! Status Code: " + request.responseCode);
        }
    }

    void Update()
    {
        x += 1;
        if (Input.GetKeyDown(KeyCode.Space)) // Example trigger
        {
            SendData("message", "Hello from Unity!");
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("Closing outlet stream....");
        outl.Close();
        if(outl.IsClosed == true) {
            Debug.Log("Successfully Closed!");
        }
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}
