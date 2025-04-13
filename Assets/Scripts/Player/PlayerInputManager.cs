using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private InputSystem_Actions _inputActions;

    public event Action<Vector2> OnMove;

    public event Action OnJump;

    public event Action OnAttack;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.Enable();
    }

    private void OnEnable()
    {
        //sub to event
        _inputActions.Player.Move.performed += Move;
        _inputActions.Player.Jump.performed += Jump;
        _inputActions.Player.Attack.performed += Attack;
    }

    private void OnDisable()
    {
        //un-sub from event
        _inputActions.Player.Move.performed -= Move;
        _inputActions.Player.Jump.performed -= Jump;
        _inputActions.Player.Attack.performed -= Attack;
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        OnMove?.Invoke(moveVector);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        OnJump.Invoke();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }
}
