using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float enhanceJumpForce = 8f;
    private float curJumpForce;
    private Vector3 resetPosition;
    private float gravityScale = -9.81f;
    private Vector2 horizontalMovement;
    private float verticalMovement;
    private CharacterController playerInfo;
    private bool changeTrigger = false;
    private bool reverseTrigger = false;
    

    void Start()
    {
        reverseTrigger = false;
        curJumpForce = jumpForce;
        clearDir();
        playerInfo = transform.GetComponent<CharacterController>();
        changeTrigger = false;
    }

    void Update()
    {
        if(changeTrigger)
        {
            applyMovement3D();
        }
        else
        {
            applyMovement2D();
        }
    }
    public void resetGravity()
    {
        reverseTrigger = false;
    }
    public void chnageGravity()
    {
        reverseTrigger = !reverseTrigger;
    }
    public void resetForce()
    {
        curJumpForce = jumpForce;
    }
    public void enhanceForce()
    {
        curJumpForce = enhanceJumpForce;
    }
    public void resetMove()
    {
        clearDir();
        changeTrigger = false;
    }
    public void chgMove()
    {
        StartCoroutine(changeMove());
    }
    public IEnumerator changeMove()
    {
        yield return null;
        changeTrigger = true;
        yield return null;
    }

    private void clearDir()
    {
        horizontalMovement = new Vector2(0, 0);
        verticalMovement = 0;
    }

    private void OnMove(InputValue input)
    {
        horizontalMovement = input.Get<Vector2>();
    }

    public void differVerticalPower(float force)
    {
        if(reverseTrigger)
        {
            verticalMovement = -force;
        }
        else
        {
            verticalMovement = force;        
        }
    }

    private void OnJump(InputValue input)
    {
        if(!playerInfo.isGrounded) return;
        if(reverseTrigger)
        {
            verticalMovement = -curJumpForce;
        }
        else
        {
            verticalMovement = curJumpForce;        
        }
    }

    private void applyMovement3D()
    {
        if (!playerInfo.isGrounded)
        {
            if(reverseTrigger)
            {
                verticalMovement -= gravityScale * Time.deltaTime;
            }
            else
            {
                verticalMovement += gravityScale * Time.deltaTime;
            }
        }

        Vector3 resultMove = new Vector3(horizontalMovement.x * moveSpeed, verticalMovement, horizontalMovement.y * moveSpeed);
        playerInfo.Move(resultMove * Time.deltaTime);
        
    }
    private void applyMovement2D()
    {
        if (!playerInfo.isGrounded)
        {
            if(reverseTrigger)
            {
                verticalMovement -= gravityScale * Time.deltaTime;
            }
            else
            {
                verticalMovement += gravityScale * Time.deltaTime;
            }
        }

        Vector3 resultMove = new Vector3(horizontalMovement.x * moveSpeed, verticalMovement, 0);
        playerInfo.Move(resultMove * Time.deltaTime);
    }
}
