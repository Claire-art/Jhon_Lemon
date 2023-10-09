using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{

    // ���̵� �ð�, �̹��� ǥ�� �ð�, �÷��̾� ��ü, ĵ���� �׷�, ����� �ҽ� � ���� ������ �������� �����Ѵ�.
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;

    // �÷��̾ �߰ߵǾ��� �� ����� ������
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    // ���� ���� �����ϴ� ���� ���� enemyCount�� �����ϰ� �ʱ�ȭ�Ѵ�.  
    public static int enemyCount = 0; 




    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // ���� �ٸ� ��ü�� �÷��̾���, m_IsPlayerAtExit�� true�� �����Ѵ�.
            m_IsPlayerAtExit = true;
        }
    }

    // CaughtPlayer �޼���� �ܺο��� ȣ��� �� ������, �� �޼��尡 ȣ��Ǹ� �÷��̾ �߰ߵ� ������ �����ϰ� ���� ���� ó���� �����Ѵ�.
    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    void Update()
    {
        // ���� BulletBehavior.enemyCount ���� 2 �̻��� ��� ���� ���� ó���� �Ѵ�.
        if (BulletBehavior.enemyCount >= 2) {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);

        }

        // ���� �÷��̾ �ⱸ�� �����ߴٸ� ���� ���� ó���� �Ѵ�.
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }

        // ���� �÷��̾ �߰ߵǾ��ٸ� ���� ����� ó���� �Ѵ�.
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
                // ������� �ʿ��� ���, ���� �ٽ� �ε��ϰ� ���� ���� �ʱ�ȭ�Ѵ�.
            }
            else
            {
                Application.Quit();
                BulletBehavior.enemyCount = 0;
                // ������� �ʿ����� ���� ���, ������ �����ϰ� ���� ���� �ʱ�ȭ�Ѵ�.

            }
        }

    }
}
