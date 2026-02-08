using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
            TransitionManager.instance.FadeColor("LavaFloorScene", new Color(1,1,1,0), Color.white);
            // SceneManager.LoadSceneAsync("LavaFloorScene");
        }
        else
        {
            AudioManager.instance.PlaySFX("CollectWhiteItem");
            TransitionManager.instance.ShowYouDiedPanel("GrapplingScene");
            // SceneManager.LoadSceneAsync("GrapplingScene");
        }
    }
}
