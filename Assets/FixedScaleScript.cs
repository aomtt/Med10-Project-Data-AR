using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedScaleScript : MonoBehaviour
{
    public float fixedScale = 1.0f;


    // Update is called once per frame
    void Update()
    {
        Rescale(transform, Vector3.one*fixedScale);
    }
    
    void Rescale(Transform obj, Vector3 newScale)
    {
        if(obj.root != obj)
        {
            Transform parent = obj.parent;
            obj.SetParent(null);
            obj.localScale = newScale;
            obj.SetParent(parent, true);
        }
    }
}
