using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBulletOutScreen : MonoBehaviour
{
    public GameObject bullet;
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (!onScreen)
        {
            Destroy(bullet);
        }
    }
}
