using UnityEngine;

public class Movement3D : Movement
{
    [Header("3D Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("3D Components")]
    [SerializeField] private Rigidbody rb;

    private float verticalVelocity;

    protected override void Awake()
    {
        base.Awake();

        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.freezeRotation = true;
        }
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    public override void ApplyMovement(Vector2 moveInput)
    {
        if (rb == null) return;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveInput.x * moveSpeed;
        velocity.z = moveInput.y * moveSpeed;
        velocity.y = verticalVelocity;

        rb.linearVelocity = velocity;
    }

    public override void ApplyJump()
    {
        if (rb == null) return;
        if (!IsGrounded()) return;

        verticalVelocity = reverseGravity ? -curJumpForce : curJumpForce;

        Vector3 velocity = rb.linearVelocity;
        velocity.y = verticalVelocity;
        rb.linearVelocity = velocity;
    }

    private void ApplyGravity()
    {
        if (rb == null) return;

        if (IsGrounded())
        {
            if ((!reverseGravity && verticalVelocity < 0f) ||
                (reverseGravity && verticalVelocity > 0f))
            {
                verticalVelocity = 0f;
            }
        }
        else
        {
            if (reverseGravity)
                verticalVelocity += gravityScale * Time.fixedDeltaTime;
            else
                verticalVelocity -= gravityScale * Time.fixedDeltaTime;
        }
    }

    private bool IsGrounded()
    {
        if (groundCheck == null) return false;

        return Physics.CheckSphere(
            groundCheck.position,
            groundCheckRadius,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}