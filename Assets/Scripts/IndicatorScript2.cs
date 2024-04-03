using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class IndicatorScript2 : MonoBehaviour
{
    [SerializeField, Tooltip("The Camera Game Object. Named 'Main Camera'")]
    private GameObject cameraObject;
    [SerializeField, Tooltip("The play Button Game Object. Named 'StartButton'")]
    private GameObject playButton;
    [SerializeField, Tooltip("The find floor text Game Object. Named 'FindFloorPrompt'")]
    private GameObject findFloorPrompt;
    
    private ARRaycastManager raycastManager;
    
    [SerializeField, Tooltip("The Indicator Object. Named 'IndicatorObject'")]
    private GameObject indicator;
    [SerializeField, Tooltip("The first point object of the rectangle")]
    public GameObject point1;
    [SerializeField, Tooltip("The second point object of the rectangle")]
    public GameObject point2;
    [SerializeField, Tooltip("The third point object of the rectangle")]
    public GameObject point3;
    public GameObject secondPointer;
    [FormerlySerializedAs("CubeObject")] [FormerlySerializedAs("planeObject")] public GameObject cubeObject;
    [FormerlySerializedAs("planeIndicator")] public GameObject cubeIndicator;
    private bool hasEnabled = false;
    private List<ARRaycastHit> hitList =new List<ARRaycastHit>();
    private float angle;
    

    
    private Vector2 ray;
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        if(cameraObject == null) cameraObject = GameObject.Find("Main Camera");
        if(playButton == null) playButton = GameObject.Find("StartAreaButton");
        if(findFloorPrompt == null) findFloorPrompt = GameObject.Find("FindFloorPrompt");
        if(indicator == null) indicator = GameObject.Find("IndicatorObject");
        raycastManager = FindObjectOfType<ARRaycastManager>();
        //indicator = transform.GetChild(0).gameObject;
        //indicator.SetActive(false);
        playButton.SetActive(false);
        findFloorPrompt.SetActive(true);
    }
    
    void Update()
    {
        angle = Vector3.Angle(cameraObject.transform.forward, Vector3.up);
        ray = new Vector2(Screen.width/2, Screen.height/2);
        
        if (raycastManager.Raycast(ray, hitList, TrackableType.PlaneWithinBounds))
        {
            findFloorPrompt.SetActive(false);
                //find median plane rounded down
            //Pose hitPose = hitList[^((hitList.Count/2)+1)].pose;
                //find bottom plane
            //Pose hitPose = hitList[^1].pose;
                //find top plane
            Pose hitPose = hitList[0].pose;
            transform.position = new Vector3(hitPose.position.x, hitPose.position.y, hitPose.position.z);
            Vector3 targetPos = new Vector3(cameraObject.transform.position.x, transform.position.y,
                cameraObject.transform.position.z);
            Vector3 targetDirection = targetPos - transform.position;
            transform.rotation = Quaternion.Euler(transform.rotation.x, -cameraObject.transform.rotation.y*180, transform.rotation.z);

            if (!hasEnabled) { hasEnabled = true; playButton.SetActive(true); }
            if (!point1.activeInHierarchy) return;
            DrawCube();
        } else if (point1.activeInHierarchy) {
            DrawCube();
        }
    }
    
    
    public void PlacePoint1()
    {
        point1.SetActive(true);
        point1.transform.position = transform.position;
        //point1.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y+45, transform.rotation.z);
        point1.transform.rotation = Quaternion.Euler(transform.rotation.x, -cameraObject.transform.rotation.y*180, transform.rotation.z);
        Debug.Log("point1 pos: "+point1.transform.position);
        Debug.Log("point1 localpos: "+point1.transform.localPosition);
    }
    
    public void PlacePoint2()
    {
        point2.SetActive(true);
        //place at same height as point 1
        point2.transform.position = new Vector3(transform.position.x, point1.transform.position.y, transform.position.z);
        //place individual height (plane will then be placed at the average between the two)
        //point2.transform.position = transform.position;
        point2.transform.rotation = point1.transform.rotation;
        //Debug.Log("point2 pos: "+point2.transform.position);
        //Debug.Log("point2 localpos: "+point2.transform.localPosition);
        
        //Debug.Log("running place plane method");
        //PlacePlane();
        point3.SetActive(true);
    }
    
    public void PlacePoint3()
    {
        
        //place at same height as point 1
        
        //point3.transform.position = new Vector3(point2.transform.position.x, (angle/90), point2.transform.position.z);
        //place individual height (plane will then be placed at the average between the two)
        //point2.transform.position = transform.position;
        //point3.transform.rotation = point1.transform.rotation;
        //Debug.Log("point2 pos: "+point2.transform.position);
        //Debug.Log("point2 localpos: "+point2.transform.localPosition);
        
        //Debug.Log("running place plane method");
        PlaceCube();
    }
    
    public void PlaceCube()
    {
        //enables the plot object and disables the indicator object
        cubeObject.SetActive(true);
        cubeIndicator.SetActive(false);
        //set the correct position, rotation, and scale for the plot
        cubeObject.transform.localPosition = new Vector3(point2.transform.localPosition.x/2, 0, point2.transform.localPosition.z/2);
        cubeObject.transform.rotation = point1.transform.rotation;
        cubeObject.transform.localScale = new Vector3(Mathf.Abs(point2.transform.localPosition.x/10), Mathf.Abs(point3.transform.localPosition.y/10), Mathf.Abs(point2.transform.localPosition.z/10));
        //disables the indicator(itself)
        gameObject.SetActive(false);
    }
    
    public void DrawCube()
    {
        //Debug.Log(angle);
        secondPointer.transform.position = transform.position;
        //Debug.Log("setting plane active...");
        cubeIndicator.SetActive(true);
        
        cubeIndicator.transform.rotation = point1.transform.rotation;
        
        //Debug.Log("setting plane position...    variables: "+secondPointer.transform.localPosition+"  and: "+secondPointer.transform.localPosition/2);
        cubeIndicator.transform.localPosition = secondPointer.transform.localPosition/2;
        //Debug.Log("setting rotation.,.");
        if (point3.activeInHierarchy)
        {
            
            //float angle = Vector3.Angle(cameraObject.transform.forward, Vector3.up);     "Mathf.Max(3, (180-angle)/10-5)"            
            point3.transform.localPosition = new Vector3(point2.transform.position.x, Mathf.Max(3, (10*(cameraObject.transform.position.y-point2.transform.position.y))+(105-angle)/5), point2.transform.position.z);
            //Debug.Log("point3 height: "+point3.transform.localPosition.y+"    angle value: "+(90-angle)/90+"cam height value: "+cameraObject.transform.position.y);
            cubeIndicator.transform.localPosition = new Vector3(point2.transform.localPosition.x/2, point3.transform.localPosition.y/2, point2.transform.localPosition.z/2);
            cubeIndicator.transform.localScale = new Vector3(Mathf.Abs(point2.transform.localPosition.x), Mathf.Abs(point3.transform.localPosition.y), Mathf.Abs(point2.transform.localPosition.z));
        }
        else
        {
            cubeIndicator.transform.localScale = new Vector3(Mathf.Abs(secondPointer.transform.localPosition.x), 0.1f, Mathf.Abs(secondPointer.transform.localPosition.z));
        }
        
    }
    
}
