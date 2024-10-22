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
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, shotPoint.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    public float getRotZ()
    {
        return rotZ;
    }
}
