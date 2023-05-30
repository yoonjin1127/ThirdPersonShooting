using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    private CharacterController controller;
    private Vector3 moveDir;
    private float ySpeed = 0;

    private void Awake()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ�� controller ������ �Ҵ�
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        // ���ϴ� ����,�ӷ����� �̵�
        // forward�� z�� ����ϰ�, right�� x�� ����� �̵��Ѵ�
        // �̿����� back�̳� left �������� �̵��Ϸ��� forward�� right�� �ݴ���� ���͸� ����ϸ� �ȴ�
        controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
        controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        // ���ӳ������� WASD �̵��� x��� z���� �����
        Vector2 input = value.Get<Vector2>();

        // moveDir�� x�� = �Է¹��� x��, y�� = 0, z�� = �Է¹��� y���̴�
        // �Է°��� ������� �� �̵� ���� moveDir�� �����Ѵ�
        moveDir = new Vector3(input.x, 0, input.y);
    }

    
    private void Jump() 
    {
        // y�������� ����ؼ� �ӷ��� ����
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // ���� CharacterController ������Ʈ���� IsGrounded ������ ������������, ���е��� ���� ������ �� ������� ����
        // GroundCheck�� �����߰� ySpeed�� �����϶�, ySpeed�� -1dlek
        if (GroundCheck() && ySpeed < 0)
            ySpeed = -1;

        // �� �������� y���ǵ常ŭ ���ư��� �Ѵ�
        // ��ŸŸ������ ��ǻ�ͺ� �ӵ��� ���Ͻ�Ŵ
        controller.Move(Vector3.up * ySpeed * Time.deltaTime);    
    }

    private void OnJump(InputValue value) 
    { 
        if(GroundCheck())
        // �����ӷ¸�ŭ y�������� ���� ����
        ySpeed = jumpSpeed; 
    }

    private bool GroundCheck()
    {
        RaycastHit hit;
        // SphereCast�� ����Ͽ� ��ü ������ ���� �浹 üũ
        // ������, ��� ������ �ѷ���, ��� ��������, ��� ������ ��������, �󸶸�ŭ�� ���̷� ����� ����
        return Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.6f);
    }
}
