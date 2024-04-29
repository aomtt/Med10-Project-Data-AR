using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using System.Data;
using LSL;



public class LSL_Marker_Unity : MonoBehaviour
{
    StreamInfo inf;
    StreamOutlet outl;
    public int k = 0;
    void Start()
    {
        // create stream info and outlet
        inf = new StreamInfo("Test2", "Markers", 1, 0, channel_format_t.cf_string, "giu4569");
        outl = new StreamOutlet(inf);
    }
    

    void Update()
    {
        string[] strings = new string[] { "Test", "ABC", "123" };
        string[] sample = new string[1];

        if(Input.GetKeyDown(KeyCode.Space)) {

            sample[0] = strings[k % 3];
            outl.push_sample(sample);
            Debug.Log(sample[0]);

        }
    }

    public void SendEventMarker(string eventName)  {
        string[] eventArray = new string[] { eventName };
        outl.push_sample(eventArray);
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