using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{

    // 페이드 시간, 이미지 표시 시간, 플레이어 객체, 캔버스 그룹, 오디오 소스 등에 대한 참조와 변수들을 선언한다.
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;

    // 플레이어가 발견되었을 때 사용할 변수들
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    // 적의 수를 저장하는 정적 변수 enemyCount를 선언하고 초기화한다.  
    public static int enemyCount = 0; 




    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // 만약 다른 객체가 플레이어라면, m_IsPlayerAtExit을 true로 설정한다.
            m_IsPlayerAtExit = true;
        }
    }

    // CaughtPlayer 메서드는 외부에서 호출될 수 있으며, 이 메서드가 호출되면 플레이어가 발견된 것으로 간주하고 게임 종료 처리를 시작한다.
    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    void Update()
    {
        // 만약 BulletBehavior.enemyCount 값이 2 이상일 경우 게임 종료 처리를 한다.
        if (BulletBehavior.enemyCount >= 2) {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);

        }

        // 만약 플레이어가 출구에 도달했다면 게임 종료 처리를 한다.
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }

        // 만약 플레이어가 발견되었다면 게임 재시작 처리를 한다.
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }

    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
                BulletBehavior.enemyCount = 0;
                // 재시작이 필요한 경우, 씬을 다시 로드하고 적의 수를 초기화한다.
            }
            else
            {
                Application.Quit();
                BulletBehavior.enemyCount = 0;
                // 재시작이 필요하지 않은 경우, 게임을 종료하고 적의 수를 초기화한다.

            }
        }

    }
}
