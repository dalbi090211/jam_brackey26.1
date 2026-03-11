using UnityEngine;

public class Movement2D : Movement
{
    [Header("2D Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("2D Components")]
    [SerializeField] private Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    public override void ApplyMovement(Vector2 moveInput)
    {
        if (rb == null) return;

        Vector2 velocity = rb.linearVelocity;
        velocity.x = moveInput.x * moveSpeed;
        rb.linearVelocity = velocity;
    }

    public override void ApplyJump()
    {
        if (rb == null) return;
        if (!IsGrounded()) return;

        Vector2 velocity = rb.linearVelocity;
        velocity.y = reverseGravity ? -curJumpForce : curJumpForce;
        rb.linearVelocity = velocity;
    }

    private void ApplyGravity()
    {
        if (rb == null) return;

        rb.gravityScale = reverseGravity ? -gravityScale : gravityScale;
    }

    private bool IsGrounded()
    {
        if (groundCheck == null) return false;

        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}