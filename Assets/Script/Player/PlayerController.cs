using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

#region Player Controller
// Handles player movement, attack input, and AI-controlled auto mode.
// Uses an FSM approach with states for Idle, Move, AutoMove, and AutoAttack.
public class PlayerController : MonoBehaviour
{
    #region State & Settings
    public EPlayerState currentState;
    private Coroutine attackCoroutine;

    public float detectRange = 5f;
    [SerializeField]private float moveSpeed = 4f;
    public float attackInterval = 1.5f;
    public int damage = 10;

    private bool isAutoMode = false;
    public bool IsAutoMode => isAutoMode;
    #endregion

    #region Input
    private Vector2 moveInput;
    private PlayerInputActions inputActions;
    public MobileJoystick joystick;
    #endregion

    #region Target
    private GameObject target;
    #endregion

    #region Unity Events
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
        // Prevent movement if chat is focused
        if (ChatUIController.Instance != null && ChatUIController.Instance.IsChatFocused)
            return;

        // Handle manual movement override in auto mode
        if (isAutoMode && moveInput.magnitude > 0f && (currentState == EPlayerState.AutoMove || currentState == EPlayerState.AutoAttack))
        {
            SetState(EPlayerState.Move);
        }

        // Resume auto-move if player stops manual input
        if (isAutoMode && currentState == EPlayerState.Move && moveInput.magnitude == 0f)
        {
            SetState(EPlayerState.AutoMove);
        }

        // Main FSM logic
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
    #endregion

    #region Manual Movement
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
    #endregion

    #region Manual Attack
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
    #endregion

    #region Auto Mode Logic
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

    IEnumerator AutoAttackRoutine()
    {
        while (currentState == EPlayerState.AutoAttack && target != null)
        {
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist > detectRange)
            {
                target = null;
                SetState(EPlayerState.AutoMove);
                yield break;
            }

            AttackTarget(target);
            yield return new WaitForSeconds(1.5f);
        }

        SetState(EPlayerState.AutoMove);
    }
    #endregion

    #region Combat
    void AttackTarget(GameObject enemy)
    {
        var enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.TakeDamage(10);
        }
    }
    #endregion

    #region State & Auto Toggle
    public void SetState(EPlayerState newState)
    {
        if (currentState == EPlayerState.AutoAttack && attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        currentState = newState;
    }

    public void SetAutoMode(bool isAuto)
    {
        isAutoMode = isAuto;

        if (isAuto)
        {
            SetState(EPlayerState.AutoMove);
        }
        else
        {
            SetState(EPlayerState.Idle);
        }
    }
    #endregion

    #region Unused
    //void HandleIdle()
    //{

    //}
    #endregion
}
#endregion