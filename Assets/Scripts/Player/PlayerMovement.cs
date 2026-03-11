using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dimension")]
    [SerializeField] private Movement curMovement;
    [SerializeField] private Movement2D move2D;
    [SerializeField] private Movement3D move3D;

    private Vector2 moveInput;
    private bool jumpQueued;

    private void Start()
    {
        Set2DMode();
    }

    private void FixedUpdate()
    {
        if (curMovement == null) return;

        curMovement.ApplyMovement(moveInput);

        if (jumpQueued)
        {
            curMovement.ApplyJump();
            jumpQueued = false;
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed) return;
        jumpQueued = true;
    }

    public void Set2DMode()
    {
        if (move2D == null || move3D == null) return;

        move2D.gameObject.SetActive(true);
        move3D.gameObject.SetActive(false);
        curMovement = move2D;
    }

    public void Set3DMode()
    {
        if (move2D == null || move3D == null) return;

        move2D.gameObject.SetActive(false);
        move3D.gameObject.SetActive(true);
        curMovement = move3D;
    }

    public void ToggleDimension()
    {
        if (curMovement == move2D) Set3DMode();
        else Set2DMode();
    }

    
}