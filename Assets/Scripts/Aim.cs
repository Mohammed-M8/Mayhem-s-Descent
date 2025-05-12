using UnityEngine;

public class Aim : MonoBehaviour
{
    public Camera mainCamera;
    public bool isPaused;
    [Tooltip("Only these layers will be hittable by the aim ray")]
    [SerializeField] LayerMask groundMask;

    public Vector3 GetMousePosition()
    {
        if (isPaused) return Vector3.zero;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        // only collide with groundMask
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
            return hit.point;

        return Vector3.zero;
    }
}
