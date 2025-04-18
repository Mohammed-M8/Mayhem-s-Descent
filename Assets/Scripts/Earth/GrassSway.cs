using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSway : MonoBehaviour
{
    public float swaySpeed = 1f;
    public float swayAmount = 5f;

    private float baseRotation;

    void Start()
    {
        baseRotation = transform.localEulerAngles.y;
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.localRotation = Quaternion.Euler(0, baseRotation + sway, 0);
    }
}

