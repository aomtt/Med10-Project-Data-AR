using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BillboardScript : MonoBehaviour
{
    public GameObject camera;
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        Billboard();
    }

    // Update is called once per frame
    void Update()
    {
        Billboard();
    }

    void Billboard()
    {
        var position = camera.transform.position;
        var v = position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(position - v*-1);
    }
}
