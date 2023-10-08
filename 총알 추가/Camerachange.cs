using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerachange : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;

    void Start()
    {
        // ������ ���� 3��Ī ���� ī�޶� Ȱ��ȭ
        thirdPersonCamera.enabled = true;
        firstPersonCamera.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Tab Ű�� ������ ī�޶� ����
            thirdPersonCamera.enabled = !thirdPersonCamera.enabled;
            firstPersonCamera.enabled = !firstPersonCamera.enabled;
        }
    }
}



