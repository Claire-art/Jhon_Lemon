using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public GameObject explosionEffect;
    public static int enemyCount = 0;

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy") {
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(other.gameObject);
            enemyCount += 1;
        }

        Destroy(gameObject);
    }
}