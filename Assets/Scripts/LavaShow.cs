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

    public Sprite[] brokenWallSprites;

    public Image boardImage;

    void Start()
    {
        boardImage = GetComponent<Image>();
        StartCoroutine(ShowPathBoard());
    }
    IEnumerator ShowPathBoard()
    {
        yield return new WaitUntil(() => (DialogManager.instance.isInDialog == false && TransitionManager.instance.isInTransition == false));
        boardImage.sprite = lavaSprites[0];
        yield return new WaitForSecondsRealtime(0.3f);
        boardImage.sprite = lavaSprites[1];
        yield return new WaitForSecondsRealtime(1.5f);
        boardImage.sprite = lavaSprites[0];
    }

    public void SteppedOnBomb()
    {
        boardImage.sprite = lavaSprites[2];
    }

    public void CrackWall(int index)
    {
        boardImage.sprite = brokenWallSprites[index];
    }
}
