using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCubeScript : MonoBehaviour
{
    private Material material;

    private void OnEnable()
    {
        // Get the material from the renderer
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;

            // Get world space position
            Vector3 worldPosition = transform.localPosition;

            // Convert world position to color
            Color color = new Color(worldPosition.x, worldPosition.y, worldPosition.z, 0.75f);

            // Apply color to material
            material.color = color;
        }
        else
        {
            Debug.LogError("Renderer component not found on the prefab.");
            enabled = false; // Disable the script if renderer is not found
        }
    }

    private void Update()
    {

    }
}
