using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{

    // �÷��̾��� Transform�� ���� ���� ������(GameEnding)�� ���� ������ �����Ѵ�.
    public Transform player;
    public GameEnding gameEnding;

    // �÷��̾ ���� ���� �ִ��� ���θ� �Ǵ��ϴ� private bool ���� m_IsPlayerInRange�� �����Ѵ�.
    bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            // ���� �ٸ� ��ü�� �÷��̾���, m_IsPlayerInRange�� true�� �����Ѵ�. 
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            // ���� �ٸ� ��ü�� �÷��̾���, m_IsPlayerInRange�� false�� �����Ѵ�. 
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            // Physics.Raycast �Լ��� ����Ͽ� ����ĳ��Ʈ�� �߻��ϰ�, �� ����� raycastHit�� �����Ѵ�. 
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                    // ���� ����ĳ��Ʈ�� �÷��̾�� �浹�ߴٸ�, CaughtPlayer �޼��带 ȣ���Ͽ� ���� ���� ó���� �Ѵ�. 
                }
            }
        }
    }
}