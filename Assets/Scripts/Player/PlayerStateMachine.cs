
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walking,
        JumpUp,
        JumpMid,
        JumpDown,
        Attack,
    }

    private PlayerState _currentState;

    [SerializeField] private Animator PlayerAnimator;
    [SerializeField] private PlayerController _playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeState(PlayerState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState(_currentState);
    }

    private void EnterState(PlayerState targetState)
    {
        switch (targetState)
        {
            case PlayerState.Idle:
                PlayerAnimator.Play(stateName: "Idle");
                break;
            case PlayerState.Walking:
                PlayerAnimator.Play(stateName: "Walk");
                break;
            case PlayerState.JumpUp:
                PlayerAnimator.Play(stateName: "Jump");
                break;
            case PlayerState.JumpMid:
                PlayerAnimator.Play(stateName: "JumpMid");
                break;
            case PlayerState.JumpDown:
                PlayerAnimator.Play(stateName: "JumpFall");
                break;
            case PlayerState.Attack:
                PlayerAnimator.Play(stateName: "SwordAttack");
                break;
            default:

                break;
        }
    }

    private void exitState (PlayerState exitingState)
    {

    }

    private void UpdateState(PlayerState currentState)
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                //if velocity
                if (_playerController.isWalking)
                {
                    ChangeState(PlayerState.Walking);
                }
                break;
            case PlayerState.Walking:
                if(!_playerController.isWalking == false && _playerController._isGround)
                {
                    ChangeState(PlayerState.Idle);
                }
                else if (_playerController.playerRB.linearVelocityY >= 2)
                {
                    ChangeState(PlayerState.JumpUp);
                }
                break;
            case PlayerState.JumpMid:
                if (_playerController.playerRB.linearVelocityY <= -2)
                {
                    ChangeState(PlayerState.JumpDown);
                }
                break;
            case PlayerState.JumpUp:
                if (_playerController.playerRB.linearVelocityY == 1 -1)
                {
                    ChangeState(PlayerState.JumpMid);
                }
                if (_playerController.playerRB.linearVelocityY <= -2)
                {
                    ChangeState(PlayerState.JumpDown);
                }
                break;
            case PlayerState.JumpDown:
                if (_playerController.isWalking)
                {
                    ChangeState(PlayerState.Walking);
                }
                if (!_playerController.isWalking == false && _playerController._isGround)
                {
                    ChangeState(PlayerState.Idle);
                }
                break;
            default:

                break;
        }
    }

    public void ChangeState(PlayerState targetState)
    {
        //exit current state
        exitState(_currentState);

        //set current state
        _currentState = targetState;

        //enter the new state
        EnterState(_currentState);
    }

    public PlayerState GetCurrentState()
    {
        return _currentState;
    }
}
