using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerachange : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;

    void Start()
    {
        // 시작할 때는 3인칭 시점 카메라 활성화
        thirdPersonCamera.enabled = true;
        firstPersonCamera.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Tab 키를 누르면 카메라 변경
            thirdPersonCamera.enabled = !thirdPersonCamera.enabled;
            firstPersonCamera.enabled = !firstPersonCamera.enabled;
        }
    }
}



