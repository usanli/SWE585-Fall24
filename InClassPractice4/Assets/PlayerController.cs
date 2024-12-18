using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private GameObject spawnablePrefab; // Prefab to spawn (assign in the Inspector)

    [SerializeField]
    private float throwForce = 10f; // Force applied to the spawned object

    private void Update()
    {
        // Ensure the script is executed only for the local player
        if (!isLocalPlayer)
            return;

        HandleMovement();

        // Check for input to spawn the object
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdSpawn();
        }
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Smooth movement
        transform.Translate(movement * Time.deltaTime * 5f, Space.World);
    }

    [Command] // Runs this method on the server
    private void CmdSpawn()
    {
        if (spawnablePrefab == null)
        {
            Debug.LogError("Spawnable prefab is not assigned.");
            return;
        }

        // Instantiate the bullet
        GameObject spawnedObject = Instantiate(spawnablePrefab, transform.position + transform.forward, Quaternion.identity);

        // Assign the bullet to the server
        NetworkServer.Spawn(spawnedObject);

        // Ignore collisions between the bullet and the spawning player
        Collider playerCollider = GetComponent<Collider>();
        Collider bulletCollider = spawnedObject.GetComponent<Collider>();
        if (playerCollider != null && bulletCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, bulletCollider);
        }

        // Apply force to the spawned object
        Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        }
    }



}
