using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;
    private float timeBtwShots;
    private float rotZ;
    public float startTimeBtwShots;
    public Movement playerMovement;

    public GameObject mana;
    public GameObject damagedMana;
    public int howMuchManaDecrease;
    public float duration;
    public float damagedSpeed;
    private float lastCallTime = 0f;
    public float damagedDelay;
    void Update()
    {
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (playerMovement.getFacingRight())
        {
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            if (rotZ < 75f && rotZ > -75f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            }
            else if (rotZ > 75f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 75f + offset);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -75f + offset);
            }
        }
        else
        {
            rotZ = Mathf.Atan2(-difference.y, -difference.x) * Mathf.Rad2Deg;
            if (rotZ < 75f && rotZ > -75f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            }
            else if (rotZ > 75f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 75f + offset);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -75f + offset);
            }
        }
        if (mana.transform.localScale.x > 0)
        {
            if (timeBtwShots <= 0)
            {
                if (Input.GetMouseButton(0))
                {
                    lastCallTime = Time.time;
                    mana.transform.localScale = new Vector2(mana.transform.localScale.x - howMuchManaDecrease, mana.transform.localScale.y);
                    Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    timeBtwShots = startTimeBtwShots;
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        if (Mathf.Abs(damagedMana.transform.localScale.x - mana.transform.localScale.x) > 0.01 && Time.time >= lastCallTime + damagedDelay)
        {
            StartCoroutine(ManaShrink());
        }
        else if (Mathf.Abs(damagedMana.transform.localScale.x - mana.transform.localScale.x) < 0.01)
        {
            StopAllCoroutines();
        }
    }
    public float getRotZ()
    {
        return rotZ;
    }
    IEnumerator ManaShrink()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            damagedMana.transform.localScale = Vector2.Lerp(damagedMana.transform.localScale, mana.transform.localScale, elapsed / duration);
            elapsed += Time.deltaTime * damagedSpeed;
            yield return null;
        }
        damagedMana.transform.localScale = mana.transform.localScale;
    }
}
