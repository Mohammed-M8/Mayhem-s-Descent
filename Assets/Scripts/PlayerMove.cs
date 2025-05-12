using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody _rb;
    public float _speed = 5;
    public float _turnSpeed = 360;
    private Vector3 _input;
    public bool isPaused = false;

    private void Update()
    {
        if (isPaused) return;
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        if (isPaused) return;
        Move();
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;
        Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 multipliedMatrix = isometricMatrix.MultiplyPoint3x4(_input);
        var rot = Quaternion.LookRotation(multipliedMatrix, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {

        if (_input.magnitude > 0)
        {

            _rb.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);
            GetComponent<PlayerAnimation>().SetMovement(_input.x, _input.z);

        }
        else
        {
            GetComponent<PlayerAnimation>().SetMovement(0,0);
        }
        }
}

    