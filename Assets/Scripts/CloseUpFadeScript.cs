using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
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
    
    [SerializeField]
    private Collider hitbox;
    
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
        //return;
        //Material material;
        Material material;
        
        dist = Vector3.Distance(camera.transform.position, transform.position);
        if (dist > max)
        {
            (material = renderer.material).color = new Color(mainColor.r, mainColor.g, mainColor.b, 1);
            return;
        }
        if (dist < min) dist = min;

        
        (material = renderer.material).color = new Color(mainColor.r,mainColor.g,mainColor.b,Mathf.InverseLerp(min, max, dist));
        hitbox.enabled = material.color.a >= 0.25f;
    }
}
