using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    Transform gunObject;
    public float grapleSpeed = 3f;

    private void Start()
    {
        gunObject = transform.GetChild(0);
    }
    void Update()
    {
        // Screen to world point direction
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = PlayerUI.instance.playerGameObject.transform.position;

        // Then normalize direction, call raycast til hit wall
        Vector2 direction = (mousePos - playerPos).normalized;

        gunObject.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f);
        
        if(Input.GetMouseButtonDown(0))
        {
            LaunchGrapple(direction);
        }

    }

    public void LaunchGrapple(Vector2 dir)
    {
        PlayerUI.instance.playerGameObject.GetComponent<Rigidbody2D>().velocity = dir * grapleSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            PlayerUI.instance.playerGameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
