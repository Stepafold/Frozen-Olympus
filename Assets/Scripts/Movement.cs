using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    public float _dashingVelocity;
    private Vector2 _dashingDirection;
    private bool _canDash = true;
    public  float _cooldownDash = 0.7f;
    private float _dashingTime = 0.4f;
    public GameObject hand;

    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform feetpos;
    public float checkRadius;
    public LayerMask whatIsGrounded;
    private bool facingRight = true;

    public Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (animator.GetBool("isHealth"))
        {
            
            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            Dash();
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
            if (moveInput > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveInput < 0 && facingRight)
            {
                Flip();
            }
        }
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetpos.position, checkRadius, whatIsGrounded);
        animator.SetBool("IsJumping", !isGrounded);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    public void Dash()
    {
        var dashInput = Input.GetButtonDown("Dash");
        if (dashInput && _canDash)
        {
            hand.SetActive(false);
            animator.SetBool("isDashing", true);
            _canDash = false;
            _dashingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            if (_dashingDirection == Vector2.zero)
            {
                _dashingDirection = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDashing());
        }

        if (animator.GetBool("isDashing"))
        {
            rb.velocity = _dashingDirection.normalized * _dashingVelocity;
            return;
        }
    }
    public void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    public bool getFacingRight()
    {
        return facingRight;
    }
    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        hand.SetActive(true);
        animator.SetBool("isDashing", false);
        StartCoroutine(GrantPermissionForTheNextDash());
    }

    private IEnumerator GrantPermissionForTheNextDash()
    {
        yield return new WaitForSeconds(_cooldownDash);
        _canDash = true;
    }
}
