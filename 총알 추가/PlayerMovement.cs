using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;


    public GameObject bulletPrefab; // Prefab for the bullet
    public Transform firePoint; // The point from where the bullet will be fired
    public float bulletForce = 20f; // The force with which the bullet will be fired

    public float fireRate = 0.1f; // The rate at which the gun fires
    private float lastFireTime; // The last time the gun was fired


    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        lastFireTime = -fireRate;

        if (firePoint == null)
        {
            Debug.LogError("Don't forget to set up the fire point.");
        }

        if (bulletPrefab == null)
        {
            Debug.LogError("Don't forget to set up the bullet prefab.");
        }

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && Time.time >= lastFireTime + fireRate)
        {
            Shoot();
            lastFireTime = Time.time; // Update the last fire time
        }

    }


    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    void Shoot()
    {
        Vector3 firePosition = firePoint.position + new Vector3(0, 1.5f, 0); // This is the new firing position
        GameObject bulletInstance = Instantiate(bulletPrefab, firePosition, firePoint.rotation);
        Rigidbody rbBulletInstance = bulletInstance.GetComponent<Rigidbody>();

        if (rbBulletInstance != null)
        {
            rbBulletInstance.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("The Bullet Prefab needs a Rigidbody component attached.");
        }


    }

}