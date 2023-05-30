using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.Controls.AxisControl;

// TPS�� Third Person Shooting�̶�� �ǹ̷�, �� 3��Ī ���� ����� ���� ���� �帣�̴�

public class TPSCameraController : MonoBehaviour
{
    // ī�޶� ��Ʈ�� ī�޶� �ΰ���, �Ÿ��� ����
    [SerializeField] Transform cameraRoot;
    [SerializeField] float cameraSensitivity;
    [SerializeField] float lookDistance;

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

    private void Update()
    {
        // ȸ�� �Լ� Ȱ��ȭ
        Rotate();
    }

    private void LateUpdate()
    {
        // ī�޶�� ������Ʈ�� ���󰡹Ƿ�, ���� �������� ȣ��Ǵ� ������Ʈ �Լ��� ���
        Look();
    }

    private void Rotate()
    {
        // ī�޶� �̵��� �� ĳ���͵� �Բ� ȸ��
        // ī�޶� ȸ���� �÷��̾� ȸ���� �Բ� �Ͼ�ٸ� ��û���� ������ �ȴ�
        // �׷��Ƿ� ī�޶� ī�޶� ��Ʈ�� ���� ȸ����Ű��, �׸� �÷��̾ �ٶ󺸰� �Ѵ�

        // lookPoint = ���� ī�޶��� ��ġ���� ���� �������� lookDistance��ŭ �̵��� ����
        // y�� 0���� �����Ͽ� ���� ����
        // ���������� �÷��̾ lookPoint�� ���ϵ��� ȸ����Ŵ
        Vector3 lookPoint = Camera.main.transform.position + Camera.main.transform.forward * lookDistance;
        lookPoint.y = 0;
        transform.LookAt(lookPoint);
    }

    private void OnLook(InputValue value)
    {
        // �Է� ���� value���� Vector2�� ������, lookDelta�� ��´�
        lookDelta = value.Get<Vector2>();
    }

    private void Look()
    {
        // ���� * ���콺 �ΰ��� * ��ŸŸ�Ӹ�ŭ ȸ����Ŵ
        yRotation += lookDelta.x * cameraSensitivity * Time.deltaTime;
        xRotation -= lookDelta.y * cameraSensitivity * Time.deltaTime;

        // Clamp�� �ּ�, �ִ��� �����Ͽ� ȸ�������� ������ �Ѿ�� �ʰ� �Ѵ�
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // cameraRoot�� ���� ȸ���� ����
        // ī�޶��� �����¿� �̵��� ǥ���ϴ� �� ����
        cameraRoot.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
