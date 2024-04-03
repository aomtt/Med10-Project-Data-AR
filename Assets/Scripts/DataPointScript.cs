using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class DataPointScript : MonoBehaviour
{
    
    public TMP_Text textLabel;
    [SerializeField]
    public string dataDescriptionText;
    // Start is called before the first frame update
    void Start()
    {
        textLabel.text = dataDescriptionText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
