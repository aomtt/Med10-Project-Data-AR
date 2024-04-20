using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoScript : MonoBehaviour
{
    [SerializeField] private GameObject objectToMirror;

    private Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
    }
    void Update()
    {
        if (!objectToMirror.activeSelf) return;
        renderer.enabled = true;
        transform.rotation = objectToMirror.transform.rotation;
    }
}
