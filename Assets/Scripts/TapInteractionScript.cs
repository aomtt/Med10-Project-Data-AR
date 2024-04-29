using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapInteractionScript : MonoBehaviour
{
    
    public ARRaycastManager raycastManager;

    public GameObject temp;

    public Camera camera;

    public GameObject highlightedObject = null;
    
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) TapRayCast();
        //HighlightObject();
    }

    public void TapRayCast()
    {
        //Debug.Log("Debug: 1");
        if(highlightedObject!=null) highlightedObject.transform.GetChild(0).gameObject.SetActive(false);
        //Debug.Log("Debug: 2");
        highlightedObject = null;
        //Debug.Log("Debug: 3");
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        //Debug.Log("Debug: 4");
        //Debug.DrawRay(ray.origin, ray.direction, Color.green);
        
        //if (Physics.Raycast (ray.origin, ray.direction) && hit.transform.tag == "Dynamic")
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit) && hit.transform.CompareTag("DataPoint")){
            //Debug.Log("Debug: 5");
            highlightedObject = hit.transform.gameObject;
            //Debug.Log("Debug: 6");
            HighlightObject();
            //Debug.Log("hit!");
        }
    }

    void HighlightObject()
    {
        //Debug.Log("Debug: 7");
        if (highlightedObject == null) return;
        //Debug.Log("Debug: 8");
        highlightedObject.transform.GetChild(0).gameObject.SetActive(true);
        //Debug.Log("Debug: 9");
    }
}
