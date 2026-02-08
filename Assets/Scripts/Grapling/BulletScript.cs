using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(DestroyAfterTime(10f));
    }
    void Update()
    {
        Vector2 direction = GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 120f);
    }
    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && BulletGenerator.instance.birdHostile)
        {
            TransitionManager.instance.ShowYouDiedPanel("GrapplingScene");
        }
    }
}
