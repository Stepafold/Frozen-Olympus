using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCharacters : MonoBehaviour
{
    public int health;
    public Rigidbody2D rb;
    private float pushPower = 100f;
    public Animator animator;
    public GameObject hand;
    public GameObject HP;
    public GameObject DamagedHP;
    public float damagedSpeed;
    public float duration;
    public float damagedDelay;
    private float lastCallTime = 0f;
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
        if (DamagedHP.transform.localScale != HP.transform.localScale && Time.time >= lastCallTime + damagedDelay)
        {
            StartCoroutine(Shrink());
        }
        else StopAllCoroutines();
    }
    public void TakeDamage(int damage)
    {
        if (animator.GetBool("isHealth"))
        {
            lastCallTime = Time.time;
            health -= damage;
            HP.transform.localScale = new Vector2(health * 2, HP.transform.localScale.y);
            animator.SetBool("isTakeDamage", true);
            bool facingRightFromSM = GameObject.Find("Skeleton").GetComponent<Skeleton>().getFacingRight();
            if (facingRightFromSM)
            {
                rb.AddForce(transform.right * pushPower, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(transform.right * -pushPower, ForceMode2D.Impulse);
            }
        }
    }
    IEnumerator Shrink()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            DamagedHP.transform.localScale = Vector2.Lerp(DamagedHP.transform.localScale, HP.transform.localScale, elapsed / duration);
            elapsed += Time.deltaTime * damagedSpeed;
            yield return null;
        }
        DamagedHP.transform.localScale = HP.transform.localScale;
    }
}
