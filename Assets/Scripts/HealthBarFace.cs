using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFace : MonoBehaviour
{

    private Camera maincamera;
    // Start is called before the first frame update
    void Start()
    {
        maincamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (maincamera != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }
}
