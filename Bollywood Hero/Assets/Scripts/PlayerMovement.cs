using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2, jumpForce = 10, wiggleForce = 5;
    float distToGround, wiggleRoom;
    bool jumped = false;

    Rigidbody rb;

    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
        wiggleRoom = GetComponent<Collider>().bounds.extents.z;
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

        if (Input.GetAxis("Jump") > 0 && !jumped && IsGrounded())
            Jump(jumpForce);
        else if (Input.GetAxis("Jump") > 0 && jumped && CanWiggle())
            Jump(wiggleForce);
        else if (Input.GetAxis("Jump") == 0 && jumped)
            jumped = false;
    }

    void Jump(float force)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        jumped = true;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + .01f);
    }

    bool CanWiggle()
    {
        return Physics.Raycast(transform.position, transform.forward, wiggleRoom + .01f);
    }
}
