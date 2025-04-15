using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimIndicator : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Aim aim = player.GetComponent<Aim>();
        Vector3 mouseWorldPos = aim.GetMousePosition();

        Vector3 direction = mouseWorldPos - player.transform.position;
        direction.y = 0f; // Keep rotation flat on Y-axis

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
        }
    }
}
