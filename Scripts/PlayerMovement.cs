using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private float horizontal;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpingPower = 12f;
    private bool isFacingRight=false;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField]private float dashinPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;


    


    // Update is called once per frame
    void Update()
    {

        if(isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal*speed,rb.velocity.y);
        
        if(!isFacingRight && horizontal >0f)
        {
            Flip();
        }
        else if(isFacingRight && horizontal<0f)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)&& canDash)
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
    }
    public void Jump(InputAction.CallbackContext  context)
    {
        if(context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpingPower);
        } 

        if(context.canceled && rb.velocity.y> 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y* 0.5f);
        }

 
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale= localScale;
    }
    public void  Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

  public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(-transform.localScale.x * dashinPower, 0f);
        tr.emitting= true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing= false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash= true;
    }
}
