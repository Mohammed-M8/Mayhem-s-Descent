using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseFade : MonoBehaviour
{
    private float pulseSpeed = 2f;
    private float minScale = 2.8f;
    private float maxScale = 3.2f;

    void Update()
    {
        float scale = minScale + Mathf.PingPong(Time.time * pulseSpeed, maxScale - minScale);
        transform.localScale = new Vector3(scale, 1, scale);
    }
}

