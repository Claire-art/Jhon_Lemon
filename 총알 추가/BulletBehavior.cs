using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public static int enemyCount = 0; // Add this

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy")) // Check if the collided object has the tag 'enemy'
        {
            Debug.Log("적과 충돌");
            Destroy(collision.gameObject); // If so, destroy that object
            enemyCount += 1; // Add this
        }

        Destroy(gameObject); // Then destroy the bullet itself
    }
}