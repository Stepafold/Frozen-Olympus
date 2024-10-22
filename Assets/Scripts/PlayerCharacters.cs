using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacters : MonoBehaviour
{
    public int health;
    public Rigidbody2D rb;
    private float pushPower = 100f;
    public Animator animator;
    public GameObject hand;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (health <= 0 && animator.GetBool("isHealth"))
        {
            animator.SetBool("isHealth", false);
            rb.velocity = Vector3.zero;
        }
        if (animator.GetBool("isPicked"))
        {
            Destroy(hand);
        }
    }
    public void TakeDamage(int damage)
    {
        if (animator.GetBool("isHealth"))
        {
            health -= damage;
            Debug.Log(health);
            animator.SetBool("isTakeDamage", true);
            bool facingRightFromSM = GameObject.Find("Skeleton").GetComponent<Skeleton>().getFacingRight();
            if (facingRightFromSM)
            {
                rb.AddForce(transform.right * pushPower);
            }
            else
            {
                rb.AddForce(transform.right * -pushPower);
            }
        }
    }
}
