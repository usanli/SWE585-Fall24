using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;       // Player movement speed
    public float lookSensitivity = 2f; // Mouse look sensitivity
    public float jumpForce = 5f;       // Jump force
    public LayerMask groundLayer;      // Layer to check for ground

    private Rigidbody rb;
    private Transform cameraTransform;
    private float rotationX = 0f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
        CheckGround();
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Rotate player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera vertically (clamped)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    void HandleMovement()
    {
        // Get input for movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = transform.forward * moveVertical + transform.right * moveHorizontal;

        // Check for collision to prevent sticking
        if (!IsCollidingWithWall(moveDirection))
        {
            // Apply velocity for movement
            Vector3 velocity = moveDirection * moveSpeed;
            velocity.y = rb.velocity.y; // Preserve gravity
            rb.velocity = velocity;
        }
    }

    bool IsCollidingWithWall(Vector3 direction)
    {
        // Cast a ray in the desired movement direction to detect walls
        return Physics.Raycast(transform.position, direction, 0.5f);
    }

    void CheckGround()
    {
        // Check if the player is on the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
