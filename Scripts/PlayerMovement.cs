using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 'PlayerMovement'��� MonoBehaviour�� ��ӹ޴� Ŭ������ �����Ѵ�.
public class PlayerMovement : MonoBehaviour
{
    // �÷��̾��� ȸ�� �ӵ��� �����ϴ� public ���� turnSpeed�� �����Ѵ�. 
    public float turnSpeed = 20f;


    // Animator, Rigidbody, AudioSource Ÿ���� private �������� �����Ѵ�. 
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;

    // �̵� ���Ϳ� ȸ���� �����ϴ� private �������� �����Ѵ�.
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    // �Ѿ� ������, �߻� ��ġ, �Ѿ� ���� ���� public �������� �����Ѵ�.
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public float bulletForce = 20f;

    // �߻� �ӵ��� ������ �߻� �ð��� ���� private/public �������� �����Ѵ�.
    public float fireRate = 0.1f; 
    private float lastFireTime;

    // �ѼҸ��� ���� AudioClip Ÿ���� public ���� GunSound�� �����Ѵ�.
    public AudioClip GunSound;


    void Start()
    {

        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        lastFireTime = -fireRate;


        // ������ �� ������Ʈ�� �������� �ʱ�ȭ�ϸ� ���� �޽����� ����ϴ� ������ ���Եȴ�.

        if (firePoint == null)
        {
            Debug.LogError("firePoint ���� null�Դϴ�.");
        }

        if (bulletPrefab == null)
        {
            Debug.LogError("bulletPrefab ���� null�Դϴ�.");
        }


    }

    void Update()
    {

        // �� �����Ӹ��� ���콺 ��ư Ŭ�� ���ο� �߻� �ð� ������ Ȯ���Ͽ� �Ѿ� �߻� �Լ� Shoot()�� ȣ���ϴ� ������ ���Եȴ�.

        if (Input.GetMouseButtonDown(0) && Time.time >= lastFireTime + fireRate)
        {
            Shoot();
            lastFireTime = Time.time;
        }

    }


    void FixedUpdate()
    {
        // �Ƚ��� ������Ʈ������ ����� �Է¿� ���� �̵� �� ȸ�� ���Ͱ� ���ǰ� �ִϸ��̼� ���°� ������Ʈ�Ǹ� ����� ��� ���ΰ� �����ȴ�.

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
        // �ִϸ��̼� �̵� ���� �� �������� ��ġ�� ȸ�� ���� ������Ʈ�ϴ� ������ ���Եȴ�.

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    void Shoot()
    {
        // �Ѿ� �߻� �Լ������� �Ѿ��� �ν��Ͻ��� �����ϰ� �������� ���� �߰��ϸ�, �ʿ��� ��� ����� Ŭ���� ����Ѵ�.

        Vector3 firePosition = firePoint.position + transform.forward * 0.6f  + transform.up * 0.5f ;

        GameObject bulletInstance = Instantiate(bulletPrefab, firePosition, firePoint.rotation);
        Rigidbody rbBulletInstance = bulletInstance.GetComponent<Rigidbody>();

        if (rbBulletInstance != null)
        {
            rbBulletInstance.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Rigidbody üũ �ʿ�");
        }

        if (GunSound != null) {
            AudioSource.PlayClipAtPoint(GunSound, transform.position);
        }


    }

}