using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private float timer = 0.0f;

    [SerializeField] private GameObject objectToEnable;
    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 0.0f;
        objectToEnable.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5 && !objectToEnable.activeSelf)
        {
            objectToEnable.SetActive(true);
        }
    }
}
