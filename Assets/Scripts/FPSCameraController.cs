using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// FPS�� First Person Shooter�� ����, 1��Ī �������� �÷����ϴ� ���� ���� �帣�̴�

public class FPSCameraController : MonoBehaviour
{
    // ī�޶� ��Ʈ ��ġ ����
    [SerializeField] Transform cameraRoot;
    // ���콺 �ΰ��� ����
    [SerializeField] float mouseSensitivity;

    private Vector2 lookDelta;
    private float xRotation;
    private float yRotation;


    private void OnEnable()
    {
        // ���콺�� ���� �߾� ��ǥ�� ������Ű��, Ŀ���� ������ �ʰ� ��
        Cursor.lockState = CursorLockMode.Locked;   
    }

    private void OnDisable()
    {
        // ���콺 Ŀ�� ����ȭ
        Cursor.lockState = CursorLockMode.None;
    }

    // ī�޶��� ���� ������Ʈ�� �������� �ʴ´�
    // ��ó���� ���� ��찡 �����Ƿ� LateUpdate ���
    private void LateUpdate()
    {
        Look();
    }

    private void Look()
    {
        // ī�޶� �̵��� �� ĳ���͵� �Բ� �̵�
        // ���� * ���콺 �ΰ��� * ��ŸŸ�Ӹ�ŭ ȸ����Ŵ
        yRotation += lookDelta.x * mouseSensitivity * Time.deltaTime;
        xRotation -= lookDelta.y * mouseSensitivity * Time.deltaTime;

        // Clamp�� �ּ�, �ִ��� �����Ͽ� ȸ�������� ������ �Ѿ�� �ʰ� �Ѵ�
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        
        // cameraRoot�� ���� ȸ���� ����
        // ī�޶��� ���� �̵��� ǥ���ϴ� �� ����
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // �÷��̾��� ���� ȸ���� ����
        // �÷��̾��� �¿� ȸ���� ǥ���ϴ� �� ����
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void OnLook(InputValue value)
    {
        // �Է¹��� value ���� Vector2�� ������, lookDelta ������ �Ҵ���
        lookDelta = value.Get<Vector2>();
    }
}
