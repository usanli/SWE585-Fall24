using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Public variables for customization in the Inspector
    public float moveSpeed = 5f;      // Speed for movement
    public float jumpForce = 300f;    // Force for jumping
    public float delayForceAmount = 10f; // Force applied after a delay
    public float delayedForceDuration = 2f; // Delay time before applying force

    // Private variables
    private Rigidbody rb;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the GameObject
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Jump action
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false; // Prevents double jumping
            Debug.Log("Player has jumped.");
        }

        // Start a coroutine to apply force after a delay
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(DelayedForce(delayedForceDuration));
        }
    }

    // FixedUpdate is used for physics-related updates
    void FixedUpdate()
    {
        // Get movement input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Apply force for movement
        rb.AddForce(movement * moveSpeed);
    }

    // Coroutine to apply force after a delay
    IEnumerator DelayedForce(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.AddForce(Vector3.forward * delayForceAmount);
        Debug.Log("Force applied after delay.");
    }

    // Detect collisions with other objects
    void OnCollisionEnter(Collision other)
    {
        // Check if the object collided with is tagged as "Ground"
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Allow jumping again
            Debug.Log("Player has landed.");
        }
    }
}
