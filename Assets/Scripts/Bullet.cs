using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    private float rotateZ;
    public LayerMask whatIsSolid;
    public Rigidbody2D rb;
    public Animator animator;
    private bool isTakeDamage = false;
    void Start()
    {
        float rotZFromGun = GameObject.Find("Hand").GetComponent<Gun>().getRotZ();
        bool facingRightFromMovement = GameObject.Find("Player").GetComponent<Movement>().getFacingRight();
        rb = GetComponent<Rigidbody2D>();
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (facingRightFromMovement)
        {
            if (rotZFromGun < 75f && rotZFromGun > -75f)
            {
                rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            }
            else if (rotZFromGun >= 75f)
            {
                rotateZ = 75f;
            }
            else if (rotZFromGun <= -75f)
            {
                rotateZ = -75f;
            }
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
            rb.velocity = transform.right * speed;
        }
        if (!facingRightFromMovement)
        {
            if (rotZFromGun < 75f && rotZFromGun > -75f)
            {
                rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            }
            else if (rotZFromGun <= 75f)
            {
                rotateZ = 105f;
            }
            else
            {
                rotateZ = -105f;
            }
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
            rb.velocity = transform.right * speed;
        }
    }
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 0f, whatIsSolid);
        if (hitInfo.collider != null)
        {
            rb.velocity = new Vector2(0f,0f);
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                if (!isTakeDamage)
                {
                    hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                    isTakeDamage = true;
                }
            }
            animator.SetBool("isSolid", true);
        }
    }
}
