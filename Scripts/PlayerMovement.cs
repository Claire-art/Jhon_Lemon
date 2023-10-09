using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 'PlayerMovement'라는 MonoBehaviour를 상속받는 클래스를 선언한다.
public class PlayerMovement : MonoBehaviour
{
    // 플레이어의 회전 속도를 설정하는 public 변수 turnSpeed를 선언한다. 
    public float turnSpeed = 20f;


    // Animator, Rigidbody, AudioSource 타입의 private 변수들을 선언한다. 
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;

    // 이동 벡터와 회전을 저장하는 private 변수들을 선언한다.
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    // 총알 프리팹, 발사 위치, 총알 힘에 대한 public 변수들을 선언한다.
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public float bulletForce = 20f;

    // 발사 속도와 마지막 발사 시간에 대한 private/public 변수들을 선언한다.
    public float fireRate = 0.1f; 
    private float lastFireTime;

    // 총소리에 대한 AudioClip 타입의 public 변수 GunSound를 선언한다.
    public AudioClip GunSound;


    void Start()
    {

        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        lastFireTime = -fireRate;


        // 시작할 때 컴포넌트를 가져오고 초기화하며 에러 메시지를 출력하는 로직이 포함된다.

        if (firePoint == null)
        {
            Debug.LogError("firePoint 값이 null입니다.");
        }

        if (bulletPrefab == null)
        {
            Debug.LogError("bulletPrefab 값이 null입니다.");
        }


    }

    void Update()
    {

        // 매 프레임마다 마우스 버튼 클릭 여부와 발사 시간 간격을 확인하여 총알 발사 함수 Shoot()를 호출하는 로직이 포함된다.

        if (Input.GetMouseButtonDown(0) && Time.time >= lastFireTime + fireRate)
        {
            Shoot();
            lastFireTime = Time.time;
        }

    }


    void FixedUpdate()
    {
        // 픽스드 업데이트에서는 사용자 입력에 따라 이동 및 회전 벡터가 계산되고 애니메이션 상태가 업데이트되며 오디오 재생 여부가 결정된다.

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
        // 애니메이션 이동 중일 때 물리적인 위치와 회전 값을 업데이트하는 로직이 포함된다.

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    void Shoot()
    {
        // 총알 발사 함수에서는 총알의 인스턴스를 생성하고 물리적인 힘을 추가하며, 필요한 경우 오디오 클립을 재생한다.

        Vector3 firePosition = firePoint.position + transform.forward * 0.6f  + transform.up * 0.5f ;

        GameObject bulletInstance = Instantiate(bulletPrefab, firePosition, firePoint.rotation);
        Rigidbody rbBulletInstance = bulletInstance.GetComponent<Rigidbody>();

        if (rbBulletInstance != null)
        {
            rbBulletInstance.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Rigidbody 체크 필요");
        }

        if (GunSound != null) {
            AudioSource.PlayClipAtPoint(GunSound, transform.position);
        }


    }

}