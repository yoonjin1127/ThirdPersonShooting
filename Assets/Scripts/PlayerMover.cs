using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private CharacterController controller;
    private Vector3 moveDir;
    private float ySpeed = 0;
    private Animator anim;
    private bool isWalking;
    private float moveSpeed; 

    private void Awake()
    {
        // 캐릭터 컨트롤러 컴포넌트를 controller 변수에 할당
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>(); 
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        if (moveDir.magnitude == 0)     // 안 움직임
        {
            // 천천히 멈추게 하기
            // moveSpeed의 0.5f의 해당하는 속도로 감속
            moveSpeed = Mathf.Lerp(moveSpeed, 0, 0.5f);
        }
        else if (isWalking) 
        {
            // moveSpeed가 walkSpeed로 0.5f의 속도로 변화
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, 0.5f);
        }
        else
        {
            // moveSpeed가 runSpeed로 0.5f의 속도로 변화
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, 0.5f);
        }

        // 원하는 방향,속력으로 이동
        // forward는 z를 사용하고, right는 x를 사용해 이동한다
        // 이에따라 back이나 left 방향으로 이동하려면 forward나 right의 반대방향 벡터를 사용하면 된다
        controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
        controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
        
        anim.SetFloat("XSpeed", moveDir.x, 0.5f, Time.deltaTime);
        anim.SetFloat("YSpeed", moveDir.y, 0.1f, Time.deltaTime);
        anim.SetFloat("Speed", moveSpeed);

        anim.SetBool("YSpeed", moveDir.sqrMagnitude > 0);
    }

    private void OnMove(InputValue value)
    {
        // 게임내에서의 WASD 이동은 x축과 z축을 사용함
        Vector2 input = value.Get<Vector2>();

        // moveDir의 x값 = 입력받은 x값, y값 = 0, z값 = 입력받은 y값이다
        // 입력값을 기반으로 한 이동 벡터 moveDir을 생성한다
        moveDir = new Vector3(input.x, 0, input.y);

    }

    
    private void Jump() 
    {
        // y방향으로 계속해서 속력을 받음
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // 원래 CharacterController 컴포넌트에서 IsGrounded 판정을 지원해주지만, 정밀도가 낮기 때문에 잘 사용하지 않음
        // GroundCheck를 진행했고 ySpeed가 음수일때, ySpeed는 -1이다
        if (GroundCheck() && ySpeed < 0)
            ySpeed = -1;

        // 위 방향으로 y스피드만큼 날아가게 한다
        // 델타타임으로 컴퓨터별 속도를 통일시킴
        controller.Move(Vector3.up * ySpeed * Time.deltaTime);    
    }

    private void OnJump(InputValue value) 
    { 
        if(GroundCheck())
        // 점프속력만큼 y방향으로 힘을 받음
        ySpeed = jumpSpeed; 
    }

    private bool GroundCheck()
    {
        RaycastHit hit;
        // SphereCast를 사용하여 구체 형태의 물리 충돌 체크
        // 어디부터, 어느 정도의 둘레로, 어느 방향으로, 어느 변수에 저장할지, 얼마만큼의 길이로 쏠건지 설정
        return Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.6f);
    }

    private void OnWalk(InputValue value)
    {
        isWalking = value.isPressed;

        anim.SetBool("YSpeed", isWalking);
    }
}
