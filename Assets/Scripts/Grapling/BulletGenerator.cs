using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;

    void Start()
    {
        StartCoroutine(ShootBullets());
    }
    IEnumerator ShootBullets()
    {
        while(true)
        {
            // Get a random directin from the middle
            Vector2 target = Random.insideUnitCircle * 5f;
            // Spawn pos = target + random normalized circle * 10f
            Vector2 spawnPos = target + (Random.insideUnitCircle.normalized * 10f);
            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            Vector2 direction = (target - spawnPos).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            yield return new WaitForSeconds(1f);
        }
    }
}
