using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerState ��� �÷��̾� ������ �⺻�� �Ǵ� �߻� Ŭ����
public abstract class PlayerState 
{
    protected PlayerStateMachine stateMachine;        // ���� �ӽſ� ���� ����
    protected PlayerController playerController;        // �÷��̾� ��Ʈ�ѷ��� ���� ����
    protected PlayerAnimationManager animationManager; // �ִϸ��̼� �Ŵ����� �����´�.

    // ������ ���� �ӽŰ� �÷��̾� ��Ʈ�ѷ� ���� �ʱ�ȭ
    public PlayerState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.playerController = stateMachine.PlayerController;
        this.animationManager = stateMachine.GetComponent<PlayerAnimationManager>();
    }

    // ���� �޼��带 : ���� Ŭ�������� �ʿ信 ���� �������̵�
    public virtual void Enter() { }     // ���� ���� �� ȣ��

    public  virtual void Exit() { }     // ���� ���� �� ȣ��

    public virtual void Update() { } // �� ������ ȣ��
    public virtual void FixedUpdate() { }   // ���� �ð� ������� ȣ�� (���� �����)

    // ���� ��ȯ�� ������ üũ�ϴ� �޼���
    protected void CheckTransitions()
    {
        if (playerController.isGrounded())
        {
            if (Input.GetKey(KeyCode.Space))
            {
                stateMachine.TransitionTostate(new JumpingState(stateMachine));
            }
            else if (Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") != 0) // �̵�Ű�� ������ ��
            {
                stateMachine.TransitionTostate(new MovingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionTostate(new IdleState(stateMachine));
            }
        }
        else
        {
            // ���߿� ������ ���� ��ȯ ����
            if (playerController.GetVerticalVelocity() > 0)     // �޾ƿ� Y �� �ӵ� ���� + �϶�
            {
                stateMachine.TransitionTostate(new JumpingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionTostate(new FallingState(stateMachine)); // �޾ƿ� Y�� �ӵ����� - dlfeO [���� ����]
            }
        }
    }
}

//IdleState : �÷��̾ ������ �ִ� ����
public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Update()
    {
        CheckTransitions();         // �� ������ ���� ���� ��ȯ ���� üũ
    }
}

// MovingState : �÷��̾ �̵� �����϶�
public class MovingState : PlayerState
{
    private bool IsRunning;
    public MovingState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Update()
    {
        // �޸��� �Է� Ȯ��
        IsRunning = Input.GetKey(KeyCode.LeftShift);
        CheckTransitions();         // �� ������ ���� ���� ��ȯ ���� üũ
    }
    public override void FixedUpdate() 
    {
        playerController.HandleMovement();      // ���� ��� �̵� ó��
    }
}

// JumpingState : �÷��̾ ���� �����϶�
public class JumpingState : PlayerState
{
    public JumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Update()
    {
        CheckTransitions();         // �� ������ ���� ���� ��ȯ ���� üũ
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();      // ���� ��� �̵� ó��
    }
}

// FallingState : �÷��̾ ���� ���϶�
public class FallingState : PlayerState
{
    public FallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Update()
    {
        CheckTransitions();         // �� ������ ���� ���� ��ȯ ���� üũ
    }
    public override void FixedUpdate()
    {
        playerController.HandleMovement();      // ���� ��� �̵� ó��
    }
}


