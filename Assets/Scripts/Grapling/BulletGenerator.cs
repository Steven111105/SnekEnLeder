using System.Collections;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public static BulletGenerator instance;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public bool birdHostile = false;
    [SerializeField] GameObject[] items;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Invoke("StartSpawningBullets", 2.5f);
    }
    void StartSpawningBullets()
    {
        birdHostile = false;
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
        int index = 0;
        while(index < 4)
        {
            yield return new WaitForSeconds(7f);
            Vector2 spawnPos = Random.insideUnitCircle * 5f;
            GameObject item = Instantiate(items[index], spawnPos, Quaternion.identity);
            item.SetActive(true);
            
            if(index < 3)
                StartCoroutine(HideItemAfterTime(items[index], 10f));
            else
                birdHostile = true;
            
            index++;
        }
    }

    IEnumerator HideItemAfterTime(GameObject item, float time)
    {
        yield return new WaitForSeconds(time);
        item.SetActive(false);
    }
}
