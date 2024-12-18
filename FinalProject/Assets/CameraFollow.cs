using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float moveSpeed = 10f;        // Speed for camera movement
    public float lookSensitivity = 2f;   // Sensitivity for mouse movement
    public float maxLookAngle = 80f;     // Limit for up/down camera movement

    private float rotationX = 0f;        // X-axis rotation (up/down)

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        // Get input for movement
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D
        float moveVertical = Input.GetAxis("Vertical");     // W/S

        // Calculate direction relative to the camera's rotation
        Vector3 moveDirection = transform.forward * moveVertical + transform.right * moveHorizontal;
        moveDirection.y = 0; // Keep movement flat (no ascending/descending)

        // Move the camera
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }

    void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Rotate the camera horizontally (left/right)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically (up/down)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxLookAngle, maxLookAngle); // Limit up/down angle
        transform.localRotation = Quaternion.Euler(rotationX, transform.eulerAngles.y, 0f);
    }
}
