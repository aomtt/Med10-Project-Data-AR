using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedScaleScript : MonoBehaviour
{
    public float fixedScale = 1.0f;
    
    
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
    
    public void Rescale()
    {
        if(transform.root != transform)
        {
            Transform parent = transform.parent;
            Transform transform1;
            (transform1 = transform).SetParent(null);
            transform1.localScale = Vector3.one*fixedScale;
            transform.SetParent(parent, true);
        }
    }
    
    public void Rescale(float scale)
    {
        if(transform.root != transform)
        {
            Transform parent = transform.parent;
            Transform transform1;
            (transform1 = transform).SetParent(null);
            transform1.localScale = Vector3.one*scale;
            transform.SetParent(parent, true);
        }
    }
}
