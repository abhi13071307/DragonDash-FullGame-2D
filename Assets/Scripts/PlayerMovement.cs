using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Animator animator;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private float horizontalInput;
    [SerializeField] private AudioClip jumpSound;

    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f) {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f){
            transform.localScale = new Vector3(-1, 1, 1);
        }
        animator.SetBool("Run", horizontalInput != 0);
        animator.SetBool("grounded",  IsGrounded());

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }
        if(Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if(OnWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if(IsGrounded())
            {
                coyoteCounter = coyoteTime;
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }
        }

    }

    private void Jump()
    {
        if (coyoteCounter < 0 && !OnWall()) return;
        SoundMananger.instance.PlaySound(jumpSound);
       
        if(OnWall())
        {
            WallJump();
        }
        else
        {
            if(IsGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else
            {
                if (coyoteCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                }
            }

            coyoteCounter = 0;
        }
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return horizontalInput == 0 && IsGrounded() && !OnWall();
    }
}
