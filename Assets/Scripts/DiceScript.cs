using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{
    Image diceImage;
    public Sprite[] diceFaces;

    private void Start()
    {
        diceImage = GetComponent<Image>();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    public void RollDice()
    {
        int randomRoll = Random.Range(1, 7);
        diceImage.sprite = diceFaces[randomRoll - 1];
        StartCoroutine(AnimateDiceRoll());
    }

    IEnumerator AnimateDiceRoll()
    {
        AudioManager.instance.PlaySFX("DiceRoll");
        float rollDuration = 1f;
        float t = 0;
        while (t < rollDuration)
        {
            int randomFace = Random.Range(1, 7);
            diceImage.sprite = diceFaces[randomFace - 1];
            t += Time.deltaTime;
            yield return null;
        }
        int finalFace = 4;
        diceImage.sprite = diceFaces[finalFace - 1];
        gameObject.GetComponent<Button>().enabled = false;
        GameUIManager.instance.playerPositionIndex = 3;
        GameUIManager.instance.SnapToSlot(PlayerUI.instance.playerGameObject, 3);
        
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        yield return null;
    }
}
