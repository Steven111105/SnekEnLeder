using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

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

    [Header("UI Refs")]
    public Transform gridParent;
    public GameObject gridSlotPrefab;
    public GameObject snakeTest;
    public GameObject snake2;
    public GameObject snake3;
    public int snakeTile;
    [HideInInspector] public int snakeTilePrevious;
    [HideInInspector] public int snakeTilePrevious2;
    [HideInInspector] public bool isPlayerIn;
    public ScriptableObject pathIndexData;
    [HideInInspector] public int playerPositionIndex;
    public LavaShow lavaShow;
    [SerializeField] Sprite[] snakeHeadSprite;
    [SerializeField] Sprite[] snakeBodySprite;
    [SerializeField] Sprite[] snakeTailSprite;

    Direction lastMoveDirection;
    float gridSize;
    float gridOffset;
    float slotSize;
    float slotOffset; 
    Transform[] gridSlots; 

    public AsyncOperation asyncLoad;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // fadePanelImage.color = new Color(1f, 1f, 1f, 0f);
            // fadePanelImage.gameObject.SetActive(false);
        }
    }
    void Start()
    {
        playerPositionIndex = -1;
        lastMoveDirection = Direction.Right;
        last2Direction = Direction.Right;
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
            // Debug.Log("Instantiating slot " + i);
            GameObject slot = Instantiate(gridSlotPrefab, gridParent);
            RectTransform rect = slot.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0,0);
            rect.sizeDelta = new Vector2(slotSize, slotSize);
            int row = i / 10;
            rect.localPosition = new Vector2((i%10) * slotSize + slotOffset - gridOffset, row * slotSize + slotOffset - gridOffset);
            if(row % 2 == 0)
            {
                // Debug.Log("assigned at slot " + i);
                gridSlots[i] = slot.transform;
            }
            else
            {
                // rect.localPosition = new Vector2((9 - (i%10)) * slotSize + slotOffset - gridOffset, row * slotSize + slotOffset - gridOffset);
                // Debug.Log("assigned at slot " + ((((row+1)*10) - i%10)-1));
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
    Direction last2Direction;
    int wallDurability = 3;
    public void Move(int directionInt)
    {
        if(Time.timeScale == 0f || TransitionManager.instance.isInTransition || DialogManager.instance.isInDialog)
            return;
        
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
                    last2Direction = lastMoveDirection;
                    lastMoveDirection = Direction.Up;
                    break;
                case Direction.Down:
                    if(snakeTile - 10 < 0 || Direction.Up == lastMoveDirection)
                        return;
                    snakeTilePrevious2 = snakeTilePrevious;
                    snakeTilePrevious = snakeTile;
                    snakeTile -= 10;
                    last2Direction = lastMoveDirection;
                    lastMoveDirection = Direction.Down;
                    break;
                case Direction.Left:
                    if(SceneManager.GetActiveScene().name == "LavaFloorScene")
                    {
                        if(snakeTile == 10)
                        {
                            wallDurability -= 1;
                            if(wallDurability <= 0)
                            {
                                TransitionManager.instance.FadeColor("Epilog1", new Color(1,1,1,0), Color.white);
                            }
                            else
                            {
                                lavaShow?.CrackWall(3-wallDurability);
                            }
                        }
                        if(snakeTile % 10 == 0 || Direction.Right == lastMoveDirection)
                            return;
                    }
                    else if(snakeTile % 10 == 0 || Direction.Right == lastMoveDirection)
                        return;
                    snakeTilePrevious2 = snakeTilePrevious;
                    snakeTilePrevious = snakeTile;
                    snakeTile -= 1;
                    last2Direction = lastMoveDirection;
                    lastMoveDirection = Direction.Left;
                    break;
                case Direction.Right:
                    if(snakeTile % 10 == 9 || Direction.Left == lastMoveDirection)
                        return;
                    snakeTilePrevious2 = snakeTilePrevious;
                    snakeTilePrevious = snakeTile;
                    snakeTile += 1;
                    last2Direction = lastMoveDirection;
                    lastMoveDirection = Direction.Right;
                    break;
            }
            DiceScript.instance?.CrackDice(direction);
            snake3.transform.position = snake2.transform.position;
            snake2.transform.position = snakeTest.transform.position;
            snakeTest.transform.position = gridParent.GetChild(snakeTile).position;

            AudioManager.instance.PlaySFX("DiceStuck");
            // Debug.Log(playerPositionIndex);

            playerPositionIndex += 4;
            // Debug.Log(playerPositionIndex + "after");
            if(isPlayerIn)
                SnapToSlot(PlayerUI.instance.playerGameObject, playerPositionIndex);
            SetSnakeSprite();

            if((snakeToPlayerIndex(snakeTile) == playerPositionIndex && isPlayerIn)
            || (snakeToPlayerIndex(snakeTilePrevious) == playerPositionIndex && isPlayerIn)
            || (snakeToPlayerIndex(snakeTilePrevious2) == playerPositionIndex && isPlayerIn))
            {
                AudioManager.instance.PlaySFX("EatPlayer");
                alreadyEatenBySnake = true;
            }
        }
        else
        {
            
            SnakeUI.instance.TakeDamage(Random.Range(2,7));
        }

        if(pathIndexData != null)
        {
            PathIndexData pathData = (PathIndexData)pathIndexData;
            if(snakeTile + 1 == pathData.finishIndex)
            {
                Debug.Log("Snake reached the end!");
                TransitionManager.instance.FadeColor("Epilog2", new Color(1,1,1,0), Color.white);
                AudioManager.instance.StopBGM();
            }
            else if(snakeTile + 1 == pathData.pathIndexs.Find(x => x == snakeTile + 1))
            {
                AudioManager.instance.PlaySFX("HitBomb");
                lavaShow?.SteppedOnBomb();
                TransitionManager.instance.ShowYouDiedPanel("LavaFloorScene");
            }
            else if(snakeTile + 1 > 30)
                AudioManager.instance.PlaySFX("SafeLava");
        }  
    }

    void SetSnakeSprite()
    {
        // Set the head sprite
        snakeTest.GetComponent<Image>().sprite = snakeHeadSprite[(int)lastMoveDirection];
        snake3.GetComponent<Image>().sprite = snakeTailSprite[(int)last2Direction];

        int spriteIndex = -1;

        if((last2Direction == Direction.Left && lastMoveDirection == Direction.Up)
        || last2Direction == Direction.Down && lastMoveDirection == Direction.Right)
        {
            spriteIndex = 0;
        }else if((last2Direction == Direction.Up && lastMoveDirection == Direction.Right)
        || (last2Direction == Direction.Left && lastMoveDirection == Direction.Down))
        {
            spriteIndex = 1;
        }else if((last2Direction == Direction.Right && lastMoveDirection == Direction.Right)
        || (last2Direction == Direction.Left && lastMoveDirection == Direction.Left))
        {
            spriteIndex = 2;
        }else if((last2Direction == Direction.Right && lastMoveDirection == Direction.Down)
        || (last2Direction == Direction.Up && lastMoveDirection == Direction.Left))
        {
            spriteIndex = 3;
        }else if((last2Direction == Direction.Down && lastMoveDirection == Direction.Down)
        || (last2Direction == Direction.Up && lastMoveDirection == Direction.Up))
        {
            spriteIndex = 4;
        }else if((last2Direction == Direction.Down && lastMoveDirection == Direction.Left)
        || (last2Direction == Direction.Right && lastMoveDirection == Direction.Up))
        {
            spriteIndex = 5;
        }
        else
        {
            Debug.LogError("Uhh idk impossible directions");
        }
        snake2.GetComponent<Image>().sprite = snakeBodySprite[spriteIndex];
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
        last2Direction = Direction.Right;
        SetSnakeSprite();
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