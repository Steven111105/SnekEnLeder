using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHeadScript : MonoBehaviour
{
    public static GrappleHeadScript instance;
    public GameObject ropeObject;
    SpriteRenderer ropeSpriteRenderer;
    Transform anchorPoint;
    Rigidbody2D rb;

    private void Awake()
    {
        instance = this;
        ropeSpriteRenderer = ropeObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (anchorPoint != null)
        {
            Vector2 direction = (Vector2)transform.position - (Vector2)anchorPoint.position;
            float distance = direction.magnitude;
            ropeSpriteRenderer.size = new Vector2(ropeSpriteRenderer.size.x, distance);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            ropeObject.transform.position = (anchorPoint.position + transform.position)/2;
            ropeObject.transform.rotation = Quaternion.Euler(0, 0, angle + 90f);
        }
    }


    public void Shoot(Vector2 direction, float speed, Transform origin)
    {
        gameObject.SetActive(true);
        transform.position = origin.position;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = direction * speed;
        GetComponent<Collider2D>().enabled = true;
        anchorPoint = origin;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localEulerAngles = new Vector3(0, 0, angle + 90f);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
    }
}
