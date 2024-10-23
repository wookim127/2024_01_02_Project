using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾��� ���¸� ����
public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState; // ���� �÷������� ���¸� ��Ÿ���� ����
    public PlayerController PlayerController; // PlayerController �� ����

    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();        // ���� ������Ʈ�� �پ��մ� PlayerController�� ����
    }

    void Start()
    {
        // �ʱ� ���¸� IdeleState �� ����
        TransitionTostate(new IdleState(this));
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���°� �����Ѵٸ� �ش� ������  Update �޼��� ȣ��
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    private void FixedUpdate()
    {
        // ���� ���°� �����Ѵٸ� �ش� ������  FixedUpdate �޼��� ȣ��
        if (currentState != null)
        {
            currentState.FixedUpdate();
        }
    }

    public void TransitionTostate(PlayerState newstate)
    {
        // ���� ���¿� ���ο� ���°��� Ÿ�� �� ���
        if (currentState?.GetType() == newstate.GetType())
        {
            return;     // ���� Ÿ���̸� ���¸� ��ȯ ���� �ʰ� ����
        }

        // ���� ���°� �����Ѵٸ�  Exit �޼��带 ȣ��
        currentState?.Exit();           //�˻��ؼ� ȣ�� ���� (?)�� IF ����

        // ���ο� ���·� ��ȯ
        currentState = newstate;

        // ���ο� ������ Enter �޼��� ȣ�� (���� ����)
        currentState.Enter();

        // �α׿� ���� ��ȯ ������ ���
        Debug.Log($"���� ��ȯ �Ǵ� ������Ʈ : {newstate.GetType().Name}");

    }
}
