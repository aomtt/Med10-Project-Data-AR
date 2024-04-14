using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class DataPointScript : MonoBehaviour
{
    
    public TMP_Text textLabel;
    [SerializeField]
    public string dataDescriptionText;

    [SerializeField] public GameObject lineObject;
    [SerializeField] public LineRenderer lineRenderer;
    [SerializeField] public Renderer renderer;

    [SerializeField] public MeshFilter meshFilter;
    [SerializeField] public Mesh[] meshArray;
    
    
    void Start()
    {
        textLabel.text = dataDescriptionText;
    }

    public void SetLine(bool toggle)
    {
        lineObject.SetActive(toggle);
        var material = renderer.material;
        lineRenderer.startColor = lineRenderer.endColor = new Color(material.color.r, material.color.g, material.color.b, 1);
    }

    public void SetMesh(int index)
    {
        meshFilter.mesh = meshArray[index];
    }
}
