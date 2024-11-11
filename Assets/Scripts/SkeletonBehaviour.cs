using UnityEngine;
public class Enemy : MonoBehaviour
{
    public int health;
    private Rigidbody2D rb;
    private float pushPower = 0f;
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
            animator.Play("Skeleton_Dead");
            if (animator.GetBool("isPicked")) Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (!animator.GetBool("isPlayAttack")) animator.SetBool("isTakeDamage", true);
        bool facingRightFromSM = GameObject.Find("Skeleton").GetComponent<Skeleton>().getFacingRight();
        if (facingRightFromSM)
        {
            rb.AddForce(transform.right * -pushPower, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(transform.right * pushPower, ForceMode2D.Impulse);
        }
    }
}
