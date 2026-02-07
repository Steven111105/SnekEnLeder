using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LavaShow : MonoBehaviour
{
    public Sprite[] lavaSprites;
    // 0 grass
    // 1 path
    // 2 bomb

    public Image boardImage;

    void Start()
    {
        StartCoroutine(ShowPathBoard());
    }
    IEnumerator ShowPathBoard()
    {
        boardImage.sprite = lavaSprites[0];
        yield return new WaitForSeconds(0.3f);
        boardImage.sprite = lavaSprites[1];
        yield return new WaitForSeconds(1.5f);
        boardImage.sprite = lavaSprites[0];
    }

    public void SteppedOnBomb()
    {
        StartCoroutine(SteppedOnBombCoroutine());
    }
    
    public IEnumerator SteppedOnBombCoroutine()
    {
        boardImage.sprite = lavaSprites[2];
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("LavaFloorScene");
    }
}
