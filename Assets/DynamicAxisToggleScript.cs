using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAxisToggleScript : MonoBehaviour
{
    [SerializeField] public GameObject[] xAxes;
    [SerializeField] public GameObject[] yAxes;
    [SerializeField] public GameObject[] zAxes;
    



 
    void Update()
    {
        // Calculate distances for each axis separately
        var position = transform.position;
        float xdist1 = Vector3.Distance(xAxes[0].transform.position, position);
        float xdist2 = Vector3.Distance(xAxes[1].transform.position, position);
        float ydist1 = Vector3.Distance(yAxes[0].transform.position, position);
        float ydist2 = Vector3.Distance(yAxes[1].transform.position, position);
        float ydist3 = Vector3.Distance(yAxes[2].transform.position, position);
        float ydist4 = Vector3.Distance(yAxes[3].transform.position, position);
        float zdist1 = Vector3.Distance(zAxes[0].transform.position, position);
        float zdist2 = Vector3.Distance(zAxes[1].transform.position, position);

        // Find the minimum distance for each axis
        float minDistX = Mathf.Min(xdist1, xdist2);
        float minDistY = Mathf.Min(ydist1, ydist2, ydist3, ydist4);
        float minDistZ = Mathf.Min(zdist1, zdist2);

        // Activate the closest object for each axis and deactivate the others
        ActivateClosest(xAxes, minDistX);
        ActivateAdjacent(yAxes, minDistY);
        ActivateClosest(zAxes, minDistZ);
    }

    // Helper method to activate the closest object for an axis and deactivate the others
    void ActivateClosest(GameObject[] axisObjects, float minDistance)
    {
        foreach (GameObject axisObject in axisObjects)
        {
            bool isActive = Math.Abs(Vector3.Distance(axisObject.transform.position, transform.position) - minDistance) < 0.01f;
            axisObject.SetActive(isActive);
        }
    }
    void ActivateAdjacent(GameObject[] axisObjects, float minDistance)
    {
        int minIndex = -1;
        float minDifference = float.MaxValue;

        // Find the index of the object closest to minDistance
        for (int i = 0; i < axisObjects.Length; i++)
        {
            float distance = Vector3.Distance(axisObjects[i].transform.position, transform.position);
            float difference = Mathf.Abs(distance - minDistance);
            if (difference < minDifference)
            {
                minDifference = difference;
                minIndex = i;
            }
        }

        // Activate the adjacent object
        int adjacentIndex = (minIndex + 1) % axisObjects.Length; // Wrap around if at the end
        foreach (GameObject go in axisObjects)
        {
            go.SetActive(false);
        }
        axisObjects[adjacentIndex].SetActive(true);
    }
    
}
