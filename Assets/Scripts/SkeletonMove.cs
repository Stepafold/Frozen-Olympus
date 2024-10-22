using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Transform player;
    public float agroDistance;
    private Rigidbody2D rb;
    public float speed;
    private bool facingRight = true;
    public Animator animator;
    public Animator animatorPlayer;
    private bool isTakeDamage = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float distanceBtwPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceBtwPlayer <= agroDistance && transform.position.x > player.position.x)
        {
            animator.SetBool("isAgro", true);
            rb.velocity = new Vector2(-speed, 0f);
            if (facingRight)
            {
                Flip();
            }
        }
        else if (distanceBtwPlayer <= agroDistance && transform.position.x < player.position.x)
        {
            animator.SetBool("isAgro", true);
            rb.velocity = new Vector2(speed, 0f);
            if (!facingRight)
            {
                Flip();
            }
        }
        else if (distanceBtwPlayer > agroDistance)
        {
            animator.SetBool("isAgro", false);
        }
        if (Mathf.Abs(transform.position.x - player.position.x) < 1 && animator.GetBool("isHealth") && !animator.GetBool("isPlayAttack"))
        {
            animator.Play("Skeleton_Attack");
        }
        else
        {
            isTakeDamage = false;
        }
        if (animator.GetBool("isPlayAttack") && animator.GetBool("isHealth"))
        {
            rb.velocity = Vector2.zero;
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
}
