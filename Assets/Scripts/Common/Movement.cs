using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [Header("Move Stat")]
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 5f;
    [SerializeField] protected float gravityScale = 9.81f;

    protected float curJumpForce;
    protected bool reverseGravity = false;
    protected virtual void Awake()
    {
        curJumpForce = jumpForce;
    }

    public virtual void ResetForce()
    {
        curJumpForce = jumpForce;
    }

    public virtual void ResetGravity()
    {
        reverseGravity = false;
    }

    public virtual void ChangeGravity()
    {
        reverseGravity = !reverseGravity;
    }

    public abstract void ApplyMovement(Vector2 moveInput);
    public abstract void ApplyJump();
}