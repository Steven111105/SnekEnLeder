using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    GridSlot[] gridSlots;
    public GameObject gridSlotPrefab;

    public GameObject snakeTest;
    public GameObject snake2;
    public GameObject snake3;
    public int snakeTile;

    float gridSize;
    float gridOffset;
    float slotSize;
    float slotOffset; 
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    void Start()
    {
        InstantiateGridSlots();
    }

    [ContextMenu("Instantiate Grid Slots")]
    public void InstantiateGridSlots()
    {
        gridSize = gridParent.GetComponent<RectTransform>().rect.width;
        gridOffset = gridSize / 2f;
        slotSize = gridSize / 10f;
        slotOffset = slotSize / 2f;
        for(int i = 0; i < 100; i++)
        {
            GameObject slot = Instantiate(gridSlotPrefab, gridParent);
            RectTransform rect = slot.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0,0);
            rect.sizeDelta = new Vector2(slotSize, slotSize);
            int row = i / 10;
                // if(row % 2 == 0)
                // {
                rect.localPosition = new Vector2((i%10) * slotSize + slotOffset - gridOffset, row * slotSize + slotOffset - gridOffset);
            // }
            // else
            // {
            //     rect.localPosition = new Vector2((9 - (i%10)) * slotSize + slotOffset - gridOffset, row * slotSize + slotOffset - gridOffset);
            // }
            // slot.GetComponent<GridSlot>().slotIndex = i;
        }

        snakeTest.transform.localPosition = gridParent.GetChild(62).localPosition;
        snake2.transform.localPosition = gridParent.GetChild(61).localPosition;
        snake3.transform.localPosition = gridParent.GetChild(60).localPosition;

        snakeTile = 62;
        alreadyEatenBySnake = false;
        hasResetSnake = false;
    }

    public void SnapToSlot(GameObject playerObject, int slotIndex)
    {
        if(slotIndex >= 100)
            slotIndex = 99;
        playerObject.transform.localPosition = gridParent.transform.GetChild(slotIndex).localPosition;
    }

    bool alreadyEatenBySnake = false;
    bool hasResetSnake = false;
    public void Move(int directionInt)
    {
        if (hasResetSnake)
        {
            Debug.Log("Dice Breaks!");
            return;
        }

        if (!snakeTest.activeSelf)
        {
            ResetSnake();
        }
        if (!alreadyEatenBySnake)
        {
            
            SnapToSlot(PlayerUI.instance.playerGameObject, 3);
            Direction direction = (Direction)directionInt;
            
            switch(direction)
            {
                case Direction.Up:
                    if(snakeTile + 10 > 99)
                        return;
                    snakeTile += 10;
                    snake3.transform.localPosition = snake2.transform.localPosition;
                    snake2.transform.localPosition = snakeTest.transform.localPosition;
                    snakeTest.transform.localPosition = gridParent.GetChild(snakeTile).localPosition;
                    break;
                case Direction.Down:
                    if(snakeTile - 10 < 0)
                        return;
                    snakeTile -= 10;
                    snake3.transform.localPosition = snake2.transform.localPosition;
                    snake2.transform.localPosition = snakeTest.transform.localPosition;
                    snakeTest.transform.localPosition = gridParent.GetChild(snakeTile).localPosition;
                    break;
                case Direction.Left:
                    if(snakeTile % 10 == 0)
                        return;
                    snakeTile -= 1;
                    snake3.transform.localPosition = snake2.transform.localPosition;
                    snake2.transform.localPosition = snakeTest.transform.localPosition;
                    snakeTest.transform.localPosition = gridParent.GetChild(snakeTile).localPosition;
                    break;
                case Direction.Right:
                    if(snakeTile % 10 == 9)
                        return;
                    snakeTile += 1;
                    snake3.transform.localPosition = snake2.transform.localPosition;
                    snake2.transform.localPosition = snakeTest.transform.localPosition;
                    snakeTest.transform.localPosition = gridParent.GetChild(snakeTile).localPosition;
                    break;
            }
            if(snakeTile == 3)
            {
                Debug.Log("Snake ate you!");
                alreadyEatenBySnake = true;   
            }
        }
        else
        {
            SnakeUI.instance.TakeDamage(Random.Range(1,6));
        }
    }

    public void HideSnake()
    {
        snakeTest.SetActive(false);
        snake2.SetActive(false);
        snake3.SetActive(false);
    }

    public void ResetSnake()
    {
        snakeTile = 62;
        alreadyEatenBySnake = false;
        snakeTest.SetActive(true);
        snake2.SetActive(true);
        snake3.SetActive(true);
        snakeTest.transform.localPosition = gridParent.GetChild(62).localPosition;
        snake2.transform.localPosition = gridParent.GetChild(61).localPosition;
        snake3.transform.localPosition = gridParent.GetChild(60).localPosition;
        hasResetSnake = true;
    }

}