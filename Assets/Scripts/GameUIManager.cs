using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public static GameUIManager instance;
    public Transform gridParent;
    public GameObject gridSlotPrefab;

    public GameObject snakeTest;
    public GameObject snake2;
    public GameObject snake3;
    public int snakeTile;
    public int snakeTilePrevious;
    public int snakeTilePrevious2;
    public bool isPlayerIn;
    public ScriptableObject pathIndexData;
    public int playerPositionIndex;

    Direction lastMoveDirection;
    float gridSize;
    float gridOffset;
    float slotSize;
    float slotOffset; 
    [SerializeField] Transform[] gridSlots; 
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        playerPositionIndex = -1;
        InstantiateGridSlots();
    }

    [ContextMenu("Instantiate Grid Slots")]
    public void InstantiateGridSlots()
    {
        gridSlots = new Transform[100];
        gridSize = gridParent.GetComponent<RectTransform>().rect.width;
        gridOffset = gridSize / 2f;
        slotSize = gridSize / 10f;
        slotOffset = slotSize / 2f;
        for(int i = 0; i < 100; i++)
        {
            Debug.Log("Instantiating slot " + i);
            GameObject slot = Instantiate(gridSlotPrefab, gridParent);
            RectTransform rect = slot.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0,0);
            rect.sizeDelta = new Vector2(slotSize, slotSize);
            int row = i / 10;
            rect.localPosition = new Vector2((i%10) * slotSize + slotOffset - gridOffset, row * slotSize + slotOffset - gridOffset);
            if(row % 2 == 0)
            {
                Debug.Log("assigned at slot " + i);
                gridSlots[i] = slot.transform;
            }
            else
            {
                // rect.localPosition = new Vector2((9 - (i%10)) * slotSize + slotOffset - gridOffset, row * slotSize + slotOffset - gridOffset);
                Debug.Log("assigned at slot " + ((((row+1)*10) - i%10)-1));
                gridSlots[(((row+1)*10) - i%10)-1] = slot.transform;
            }
        }

        snakeTest.transform.position = gridParent.GetChild(snakeTile).position;
        snake2.transform.position = gridParent.GetChild(snakeTile - 1).position;
        snake3.transform.position = gridParent.GetChild(snakeTile - 2).position;

        alreadyEatenBySnake = false;
    }

    public void SnapToSlot(GameObject playerObject, int slotIndex)
    {
        if(slotIndex >= 100)
            slotIndex = 99;
        playerObject.transform.position = gridSlots[slotIndex].position;
    }

    bool alreadyEatenBySnake = false;
    public void Move(int directionInt)
    {
        if (!snakeTest.activeSelf && isPlayerIn)
        {
            ResetSnake(62);
        }
        else if (!snakeTest.activeSelf && !isPlayerIn)
        {
            ResetSnake(3);
        }

        if (!alreadyEatenBySnake)
        {
            Direction direction = (Direction)directionInt;
            
            switch(direction)
            {
                case Direction.Up:
                    if(snakeTile + 10 > 99 || Direction.Down == lastMoveDirection)
                        return;
                    snakeTilePrevious2 = snakeTilePrevious;
                    snakeTilePrevious = snakeTile;
                    snakeTile += 10;
                    lastMoveDirection = Direction.Up;
                    break;
                case Direction.Down:
                    if(snakeTile - 10 < 0 || Direction.Up == lastMoveDirection)
                        return;
                    snakeTilePrevious2 = snakeTilePrevious;
                    snakeTilePrevious = snakeTile;
                    snakeTile -= 10;
                    lastMoveDirection = Direction.Down;
                    break;
                case Direction.Left:
                    if(snakeTile % 10 == 0 || Direction.Right == lastMoveDirection)
                        return;
                    snakeTilePrevious2 = snakeTilePrevious;
                    snakeTilePrevious = snakeTile;
                    snakeTile -= 1;
                    lastMoveDirection = Direction.Left;
                    break;
                case Direction.Right:
                    if(snakeTile % 10 == 9 || Direction.Left == lastMoveDirection)
                        return;
                    snakeTilePrevious2 = snakeTilePrevious;
                    snakeTilePrevious = snakeTile;
                    snakeTile += 1;
                    lastMoveDirection = Direction.Right;
                    break;
            }
            snake3.transform.position = snake2.transform.position;
            snake2.transform.position = snakeTest.transform.position;
            snakeTest.transform.position = gridParent.GetChild(snakeTile).position;

            AudioManager.instance.PlaySFX("DiceStuck");
            Debug.Log(playerPositionIndex);

            playerPositionIndex += 4;
            Debug.Log(playerPositionIndex + "after");
            if(isPlayerIn)
                SnapToSlot(PlayerUI.instance.playerGameObject, playerPositionIndex);

            if(snakeToPlayerIndex(snakeTile) == playerPositionIndex && isPlayerIn)
            {
                AudioManager.instance.PlaySFX("EatPlayer");
                alreadyEatenBySnake = true; 
                return;
            }

            if(snakeToPlayerIndex(snakeTilePrevious) == playerPositionIndex && isPlayerIn)
            {
                AudioManager.instance.PlaySFX("EatPlayer");
                alreadyEatenBySnake = true; 
                return;
            }

            if(snakeToPlayerIndex(snakeTilePrevious2) == playerPositionIndex && isPlayerIn)
            {
                AudioManager.instance.PlaySFX("EatPlayer");
                alreadyEatenBySnake = true; 
                return;
            }
            
            if(snakeTile == 3 && isPlayerIn)
            {
                AudioManager.instance.PlaySFX("EatPlayer");
                alreadyEatenBySnake = true;   
            }
        }
        else
        {
            SnakeUI.instance.TakeDamage(Random.Range(1,6));
        }

        if(pathIndexData != null)
        {
            PathIndexData pathData = (PathIndexData)pathIndexData;
            if(snakeTile + 1 == pathData.finishIndex)
            {
                Debug.Log("Snake reached the end!");
                SceneManager.LoadSceneAsync("MainMenu");
            }
            else if(snakeTile + 1 == pathData.pathIndexs.Find(x => x == snakeTile + 1))
            {
                AudioManager.instance.PlaySFX("HitBomb");
                ResetSnake(3);
            }
            else if(snakeTile + 1 > 30)
                AudioManager.instance.PlaySFX("SafeLava");
        }  
    }

    public void HideSnake()
    {
        snakeTest.SetActive(false);
        snake2.SetActive(false);
        snake3.SetActive(false);
    }

    public void ResetSnake(int lastTile)
    {
        alreadyEatenBySnake = false;
        snakeTest.SetActive(true);
        snake2.SetActive(true);
        snake3.SetActive(true);
        snakeTest.transform.position = gridParent.GetChild(lastTile).position;
        snake2.transform.position = gridParent.GetChild(lastTile - 1).position;
        snake3.transform.position = gridParent.GetChild(lastTile - 2).position;
        snakeTile = lastTile;
        snakeTilePrevious = lastTile-1;
        snakeTilePrevious2 = lastTile-2;
        lastMoveDirection = Direction.Right;
    }

    int snakeToPlayerIndex(int snakeIndex)
    {
        int row = snakeIndex / 10;
        if(row % 2 == 0)
        {
            return snakeIndex;
        }
        else
        {            
            return (((row+1)*10) - snakeIndex%10)-1;
        }
    }
}