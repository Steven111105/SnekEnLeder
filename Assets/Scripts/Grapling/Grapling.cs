using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grapling : MonoBehaviour
{
    public float grapleSpeed = 3f;
    void Update()
    {
        // Screen to world point direction
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = PlayerUI.instance.playerGameObject.transform.position;

        // Then normalize direction, call raycast til hit wall
        Vector2 direction = (mousePos - playerPos).normalized;
        
        if(Input.GetMouseButtonDown(0))
        {
            Grappling(direction);
        }

    }

    public void Grappling(Vector2 dir)
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
