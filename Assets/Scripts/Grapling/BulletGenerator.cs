using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    [SerializeField] GameObject[] items = new GameObject[4];

    void Start()
    {
        StartCoroutine(ShootBullets());
        StartCoroutine(SpawnItems());
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

    IEnumerator SpawnItems()
    {
        int whiteSpawned = 0;
        while(whiteSpawned < 3)
        {
            yield return new WaitForSeconds(15f);
            Vector2 spawnPos = Random.insideUnitCircle * 8f;
            items[whiteSpawned].SetActive(true);
            StartCoroutine(HideItemAfterTime(items[whiteSpawned], 10f));
            whiteSpawned++;
        }
        yield return new WaitForSeconds(15f);
        items[3].SetActive(true);
    }

    IEnumerator HideItemAfterTime(GameObject item, float time)
    {
        yield return new WaitForSeconds(time);
        item.SetActive(false);
    }
}
