using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;       // Ball's forward movement speed
    public float rotationSpeed = 150f;  // Ball's rotation speed (left/right)

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input axes
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Rotate the ball left/right
        float rotation = moveHorizontal * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotation, 0));

        // Move the ball forward/backward, locking Y-axis
        Vector3 flatForward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        Vector3 movement = flatForward * moveVertical * moveSpeed;
        rb.AddForce(movement, ForceMode.Force);
    }
}
