using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSpawing : MonoBehaviour
{
    public bool isGreen;
    BoxCollider2D boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Birds")
            if(!isGreen)
                Destroy(gameObject);
        if(collision.gameObject.tag != "Player")
            return;
            
        if (isGreen)
        {
            AudioManager.instance.PlaySFX("CollectGreenItem");
            SceneManager.LoadSceneAsync("LavaFloorScene");
        }
        else
        {
            AudioManager.instance.PlaySFX("CollectWhiteItem");
            SceneManager.LoadSceneAsync("GrapplingScene");
        }
    }
}
