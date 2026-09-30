using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Rigidbody2D rb;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private float moveInput;
    private bool isGrounded;

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.MoveEvent += HandleMove;
            InputManager.Instance.JumpEvent += HandleJump;
            InputManager.Instance.PauseEvent += HandlePause;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.MoveEvent -= HandleMove;
            InputManager.Instance.JumpEvent -= HandleJump;
            InputManager.Instance.PauseEvent -= HandlePause;
        }
    }

    private void Update()
    {
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
    }

    private void FixedUpdate()
    {
        if (PauseManager.Instance != null && PauseManager.Instance.IsPaused)
        {
            return;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleMove(Vector2 direction)
    {
        moveInput = direction.x;
    }

    private void HandleJump(bool isPressed)
    {
        if (PauseManager.Instance != null && PauseManager.Instance.IsPaused)
        {
            return;
        }

        if (isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void HandlePause()
    {
        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.TogglePause();
        }
    }
}
