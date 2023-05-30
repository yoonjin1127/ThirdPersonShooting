using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// FPS는 First Person Shooter의 약어로, 1인칭 시점으로 플레이하는 슈팅 게임 장르이다

public class FPSCameraController : MonoBehaviour
{
    // 카메라 루트 위치 설정
    [SerializeField] Transform cameraRoot;
    // 마우스 민감도 설정
    [SerializeField] float mouseSensitivity;

    private Vector2 lookDelta;
    private float xRotation;
    private float yRotation;


    private void OnEnable()
    {
        // 마우스를 게임 중앙 좌표에 고정시키고, 커서를 보이지 않게 함
        Cursor.lockState = CursorLockMode.Locked;   
    }

    private void OnDisable()
    {
        // 마우스 커서 정상화
        Cursor.lockState = CursorLockMode.None;
    }

    // 카메라같은 경우는 업데이트에 구현하지 않는다
    // 후처리가 나은 경우가 많으므로 LateUpdate 사용
    private void LateUpdate()
    {
        Look();
    }

    private void Look()
    {
        // 카메라가 이동할 때 캐릭터도 함께 이동
        // 방향 * 마우스 민감도 * 델타타임만큼 회전시킴
        yRotation += lookDelta.x * mouseSensitivity * Time.deltaTime;
        xRotation -= lookDelta.y * mouseSensitivity * Time.deltaTime;

        // Clamp로 최소, 최댓값을 설정하여 회전각도가 범위를 넘어가지 않게 한다
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        
        // cameraRoot의 로컬 회전을 설정
        // 카메라의 상하 이동을 표현하는 데 사용됨
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // 플레이어의 로컬 회전을 설정
        // 플레이어의 좌우 회전을 표현하는 데 사용됨
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void OnLook(InputValue value)
    {
        // 입력받은 value 값을 Vector2로 추출해, lookDelta 변수에 할당함
        lookDelta = value.Get<Vector2>();
    }
}
