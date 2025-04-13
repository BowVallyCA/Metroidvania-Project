using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [field:SerializeField] public PlayerStatsSO Stats { get; private set; }

    [SerializeField] private PlayerInputManager inputManager;
    [SerializeField] private SpriteRenderer playervisual;
    [SerializeField] public Rigidbody2D playerRB;
    [SerializeField] public int _health = 3;

    private float horizontalInput;
    private int Direction = 20;
    public int JumpDirection = 1;
    public bool _isGround = true;
    public int _healthMax = 3;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 4f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    private bool doubleJumped;
    public bool doubleJumpActive = false;
    public bool dashActive = false;

    [SerializeField] private TrailRenderer _trailRenderer;
    public bool isWalking { get; private set; } = false;
    public bool isJumping { get; private set; } = false;

    private void OnEnable()
    {
        inputManager.OnMove += SetHorizontal;
        inputManager.OnJump += HandleJump;
    }

    private void OnDisable()
    {
        inputManager.OnMove -= SetHorizontal;
        inputManager.OnJump -= HandleJump;
    }

    private void SetHorizontal(Vector2 moveVector)
    {
        horizontalInput = moveVector.x;
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (_health > _healthMax)
        {
            _health = _healthMax;
        }

        isWalking = horizontalInput != 0;

        if (horizontalInput > 0) Direction = 1;
        else if (horizontalInput < 0) Direction = -1;

        playervisual.flipX = Direction < 0;


        isJumping = playerRB.linearVelocityY != 0;

        if (playerRB.linearVelocityY > 1) JumpDirection = 1;
        else if (playerRB.linearVelocityY < 1) JumpDirection = -1;
        else if (playerRB.linearVelocityY == 0) JumpDirection = 0;

        Debug.Log(_health);

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (_isGround)
        {
            doubleJumped = false;
        }

        HandleMove();
        CheckGround();


    }

    public void HandleMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right * horizontalInput, Stats.Speed * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (_isGround)
        {
            playerRB.linearVelocityY += Stats._jumpVelocity;
        }

        if (!_isGround && doubleJumped == false && doubleJumpActive)
        {
            playerRB.linearVelocityY += Stats._jumpVelocity * 1.5f;
            doubleJumped = true;
        }


    }

    private void CheckGround()
    {
        _isGround = Physics2D.Raycast((Vector2)transform.position + Stats.groundCheckOffset, Vector2.down, Stats.GroundCheckDistance, Stats.GroundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(
            (Vector2)transform.position + new Vector2(Direction * Stats.groundCheckOffset.x, Stats.groundCheckOffset.y),
            (Vector2)transform.position + new Vector2(Direction * Stats.groundCheckOffset.x, Stats.groundCheckOffset.y) + Direction * Stats.GroundCheckDistance * Vector2.down);
    }

    private IEnumerator Dash()
    {
        if (dashActive == true)
        {
            canDash = false;
            isDashing = true;
            float originalGravity = playerRB.gravityScale;
            playerRB.gravityScale = 0f;
            Vector2 dashVelocity = new Vector2(Direction * dashingPower, 0f);
            playerRB.linearVelocity = dashVelocity;
            _trailRenderer.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            _trailRenderer.emitting = false;
            playerRB.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }

}
