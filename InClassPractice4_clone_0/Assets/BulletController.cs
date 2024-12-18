using UnityEngine;
using Mirror;

public class BulletController : NetworkBehaviour
{
    [SerializeField]
    private float lifeTime = 5f; // Time before the bullet destroys itself

    private void Start()
    {
        // Destroy the bullet after a set amount of time
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is a player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destroy the player on the server
            if (isServer)
            {
                NetworkServer.Destroy(collision.gameObject);
            }
        }

        // Destroy the bullet on collision
        if (isServer)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
