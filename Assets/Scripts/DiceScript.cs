using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{
    public static DiceScript instance;
    Image diceImage;
    public Sprite[] diceFaces;
    public Sprite[] upDiceFaces;
    int upCrackIndex = 0;
    public Sprite[] downDiceFaces;
    int downCrackIndex = 0;
    public Sprite[] leftDiceFaces;
    int leftCrackIndex = 0;
    public Sprite[] rightDiceFaces;
    int rightCrackIndex = 0;

    private void Awake()
    {
        instance = this;
    }

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
        if(Time.timeScale == 0) return;
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

    public void CrackDice(GameUIManager.Direction direction)
    {
        switch (direction)
        {
            case GameUIManager.Direction.Up:
                if (upCrackIndex < upDiceFaces.Length)
                {
                    if(upCrackIndex == 0)
                    {
                        transform.GetChild((int)direction).GetComponent<Image>().color = Color.white; 
                    }
                    else
                    {
                        transform.GetChild((int)direction).GetComponent<Image>().sprite = upDiceFaces[upCrackIndex];
                    }
                    upCrackIndex++;
                    AudioManager.instance.PlaySFX("DiceCrack");
                }
                break;
            case GameUIManager.Direction.Down:
                if (downCrackIndex < downDiceFaces.Length)
                {
                    if(downCrackIndex == 0)
                    {
                        transform.GetChild((int)direction).GetComponent<Image>().color = Color.white;
                    }
                    else
                    {
                        transform.GetChild((int)direction).GetComponent<Image>().sprite = downDiceFaces[downCrackIndex];
                    }
                    downCrackIndex++;
                    AudioManager.instance.PlaySFX("DiceCrack");
                }
                break;
            case GameUIManager.Direction.Left:
                if (leftCrackIndex < leftDiceFaces.Length)
                {
                    if(leftCrackIndex == 0)
                    {
                        transform.GetChild((int)direction).GetComponent<Image>().color = Color.white;
                    }
                    else
                    {
                        transform.GetChild((int)direction).GetComponent<Image>().sprite = leftDiceFaces[leftCrackIndex];
                    }
                    leftCrackIndex++;
                    AudioManager.instance.PlaySFX("DiceCrack");
                }
                break;
            case GameUIManager.Direction.Right:
                if (rightCrackIndex < rightDiceFaces.Length)
                {
                    if(rightCrackIndex == 0)
                    {
                        transform.GetChild((int)direction).GetComponent<Image>().color = Color.white;
                    }
                    else
                    {
                        
                        transform.GetChild((int)direction).GetComponent<Image>().sprite = rightDiceFaces[rightCrackIndex];
                    }
                    rightCrackIndex++;
                    AudioManager.instance.PlaySFX("DiceCrack");
                }
                break;
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
