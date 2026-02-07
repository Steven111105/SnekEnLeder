using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{
    [SerializeField] Image diceImage;
    public Sprite[] diceFaces;
    public void RollDice()
    {
        int randomRoll = Random.Range(1, 7);
        diceImage.sprite = diceFaces[randomRoll - 1];
        // Debug.Log(GameManager.instance? "":"No GameManager instance found!");
        // GameManager.instance.playerPosition += randomRoll;
        // if(GameManager.instance.grid[GameManager.instance.playerPosition].hasSnake)
        // {
        //     GameManager.instance.playerPosition = GameManager.instance.grid[GameManager.instance.playerPosition].snakeEndPos;
        // }
        // else if(GameManager.instance.grid[GameManager.instance.playerPosition].hasLadder)
        // {
        //     GameManager.instance.playerPosition = GameManager.instance.grid[GameManager.instance.playerPosition].ladderEndPos;
        // }
        // GameUIManager.instance.SnapToSlot(PlayerUI.instance.gameObject, GameManager.instance.playerPosition);
        // Check if its players turn
        // if(GameManager.instance.canRollDice == false)
        //     return;

        // StartCoroutine(AnimateDiceRoll());
    }

    IEnumerator AnimateDiceRoll()
    {
        GameManager.instance.canRollDice = false;
        float rollDuration = 1.5f;
        float t = 0;
        while (t < rollDuration)
        {
            int randomFace = Random.Range(1, 7);
            diceImage.sprite = diceFaces[randomFace - 1];
            t += Time.deltaTime;
            yield return null;
        }
        int finalFace = Random.Range(1, 7);
        diceImage.sprite = diceFaces[finalFace - 1];

        // Tell player they can be draged, and give them the dice outcome
        PlayerUI.instance.playerDice = finalFace;
        yield return null;
    }
}
