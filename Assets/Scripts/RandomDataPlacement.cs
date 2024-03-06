using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDataPlacement : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localPosition = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
