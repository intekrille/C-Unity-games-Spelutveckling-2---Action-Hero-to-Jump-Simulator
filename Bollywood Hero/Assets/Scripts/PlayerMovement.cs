using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float movementSpeed = 2, jumpForce = 10;
    float distToGround;
    bool jumped = false;

    Rigidbody rb;

    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector3 rotation = (new Vector3(0, Input.GetAxis("Mouse X"), 0) * Settings.GetMouseSensitivity() * Time.deltaTime);
        transform.Rotate(rotation);
    }

    void FixedUpdate()
    {
        //Forward and Backward
        if (Input.GetAxis("Vertical") != 0)
            transform.Translate(0, 0, Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime);

        //Left and Right
        if (Input.GetAxis("Horizontal") != 0)
            transform.Translate(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, 0, 0);

        //Fungerar som KeyDown/ButtonDown fast med axis.
        if (Input.GetAxis("Jump") > 0 && !jumped && IsGrounded())
            Jump();
        else if (Input.GetAxis("Jump") == 0)
            jumped = false;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumped = true;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + .01f);
    }
}
