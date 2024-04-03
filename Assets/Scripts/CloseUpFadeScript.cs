using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
public class CloseUpFadeScript : MonoBehaviour
{
    public GameObject camera;
    [SerializeField]
    private float max = 0.15f;
    [SerializeField]
    private float min = 0.05f;
    private Renderer renderer;
    private Color mainColor;
    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        renderer = GetComponent<Renderer>();
        mainColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(camera.transform.position, transform.position);
        if (dist > max) return;
        if (dist < min) dist = min;
        renderer.material.color = new Color(mainColor.r,mainColor.g,mainColor.b,Mathf.InverseLerp(min, max, dist));
    }
}
