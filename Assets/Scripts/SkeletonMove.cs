using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Transform player;
    public float agroDistance;
    private Rigidbody2D rb;
    public float speed;
    private bool facingRight = true;
    public Animator animator;
    public Vector2 attackDistance;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float distanceBtwPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceBtwPlayer <= agroDistance && transform.position.x > player.position.x && Mathf.Abs(transform.position.y - player.position.y) < attackDistance.y)
        {
            animator.SetBool("isAgro", true);
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (facingRight)
            {
                Flip();
            }
        }
        else if (distanceBtwPlayer <= agroDistance && transform.position.x < player.position.x && Mathf.Abs(transform.position.y - player.position.y) < attackDistance.y)
        {
            animator.SetBool("isAgro", true);
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (!facingRight)
            {
                Flip();
            }
        }
        else if (distanceBtwPlayer > agroDistance)
        {
            animator.SetBool("isAgro", false);
        }
        if (Mathf.Abs(transform.position.x - player.position.x) < attackDistance.x && Mathf.Abs(transform.position.y - player.position.y) < attackDistance.y && !animator.GetBool("isPlayDead"))
        {
           animator.Play("Skeleton_Attack");
        }
        if (animator.GetBool("isPlayAttack"))
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
