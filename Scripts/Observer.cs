using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{

    // 플레이어의 Transform과 게임 종료 관리자(GameEnding)에 대한 참조를 선언한다.
    public Transform player;
    public GameEnding gameEnding;

    // 플레이어가 범위 내에 있는지 여부를 판단하는 private bool 변수 m_IsPlayerInRange을 선언한다.
    bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            // 만약 다른 객체가 플레이어라면, m_IsPlayerInRange을 true로 설정한다. 
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            // 만약 다른 객체가 플레이어라면, m_IsPlayerInRange을 false로 설정한다. 
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

            // Physics.Raycast 함수를 사용하여 레이캐스트를 발사하고, 그 결과를 raycastHit에 저장한다. 
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                    // 만약 레이캐스트가 플레이어와 충돌했다면, CaughtPlayer 메서드를 호출하여 게임 종료 처리를 한다. 
                }
            }
        }
    }
}