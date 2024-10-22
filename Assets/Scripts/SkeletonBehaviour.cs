using UnityEngine;
public class Enemy : MonoBehaviour
{
    public int health;
    public Rigidbody2D rb;
    private float pushPower = 250f;
    public Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (health <= 0)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isHealth", false);
            if (animator.GetBool("isPicked")) Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetBool("isTakeDamage", true);
        bool facingRightFromSM = GameObject.Find("Skeleton").GetComponent<Skeleton>().getFacingRight();
        if (facingRightFromSM)
        {
            rb.AddForce(transform.right * -pushPower);
        }
        else
        {
            rb.AddForce(transform.right * pushPower);
        }
    }
}
