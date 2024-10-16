using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBooster : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction = new Vector3(-5, 0, 0);
    private Vector3 startPos;

    private Rigidbody rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Get the Animator component
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the ball collides with the wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Trigger the size change animation
            animator.SetTrigger("ChangeSize");
        }
    }
}