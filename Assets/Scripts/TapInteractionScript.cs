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

    void TapRayCast()
    {
        if(highlightedObject!=null) highlightedObject.transform.GetChild(0).gameObject.SetActive(false);
        highlightedObject = null;
        
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        //Debug.DrawRay(ray.origin, ray.direction, Color.green);
        
        //if (Physics.Raycast (ray.origin, ray.direction) && hit.transform.tag == "Dynamic")
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit) && hit.transform.CompareTag("DataPoint")){
            highlightedObject = hit.transform.gameObject;
            HighlightObject();
            //Debug.Log("hit!");
        }
    }

    void HighlightObject()
    {
        if (highlightedObject == null) return;

        highlightedObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
