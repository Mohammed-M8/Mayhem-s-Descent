using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float ballSpeed;
    public float jumpSpeed;
    float horizontalInput;
    float verticalInput;
    Rigidbody rb;
    bool isGround = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBall();
        jump();
    }

    void MoveBall()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (horizontalInput, 0, verticalInput);
        rb.AddForce (movement * ballSpeed);
    }

    void jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            rb.AddForce(new Vector3(0, jumpSpeed, 0));
            isGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}
