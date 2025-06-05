using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public EPlayerState currentState;
    private Coroutine attackCoroutine;

    public float detectRange = 5f;
    [SerializeField]private float moveSpeed = 4f;
    public float attackInterval = 1.5f;
    public int damage = 10;

    private Vector2 moveInput;
    private GameObject target;

    private PlayerInputActions inputActions;
    public MobileJoystick joystick;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.LeftClickAttack.performed += ctx => TryAttack();
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void Update()
    {
        if ((currentState == EPlayerState.AutoMove || currentState == EPlayerState.AutoAttack) && moveInput.magnitude > 0f)
        {
            SetState(EPlayerState.Move);
        }


        switch (currentState)
        {
            case EPlayerState.Idle:
                if (moveInput.magnitude > 0.1f)
                    SetState(EPlayerState.Move);
                break;

            case EPlayerState.Move:
                HandleManualMove();
                break;

            case EPlayerState.AutoMove:
                HandleAutoMove();
                break;

            case EPlayerState.AutoAttack:
                HandleAutoAttack();
                break;
        }
    }

    void HandleIdle()
    {

    }

    void HandleManualMove()
    {
        Vector2 input = joystick != null ? joystick.InputDirection : moveInput;
        Vector3 dir = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        if (dir.magnitude > 0.1f)
        {
            transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.LookRotation(dir);
        }
        else
        {
            SetState(EPlayerState.Idle);
        }
    }

    void TryAttack()
    {
        if (currentState == EPlayerState.Move || currentState == EPlayerState.Idle)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, detectRange);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    AttackTarget(hit.gameObject);
                    break;
                }
            }
        }
    }

    void HandleAutoMove()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 2f);

        Collider[] hits = Physics.OverlapSphere(transform.position, detectRange);
        foreach(var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                target = hit.gameObject;
                SetState(EPlayerState.AutoAttack);
                break;
            }
        }
    }

    void HandleAutoAttack()
    {
        if (target == null)
        {
            SetState(EPlayerState.AutoMove);
            return;
        }

        if(attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AutoAttackRoutine());
        }
    }

    public void SetState(EPlayerState newState)
    {
        if (currentState == EPlayerState.AutoAttack && attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        currentState = newState;
    }

    IEnumerator AutoAttackRoutine()
    {
        while (currentState == EPlayerState.AutoAttack && target != null)
        {
            // 타겟과 거리 확인
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist > detectRange)
            {
                target = null;
                SetState(EPlayerState.AutoMove);
                yield break;
            }

            AttackTarget(target);
            yield return new WaitForSeconds(1.5f); // 공격 간격
        }

        SetState(EPlayerState.AutoMove);
    }

    void AttackTarget(GameObject enemy)
    {
        var enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.TakeDamage(10);
        }
    }
}
