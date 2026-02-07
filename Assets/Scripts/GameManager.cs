using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Effects
    {
        None,
        SnakePotion,
        BunnyEffect
    }
    public static GameManager instance;
    [SerializeField] Effects currentPlayerEffect = Effects.None;
    [SerializeField] Effects currentEnemyEffect = Effects.None;
    public BoardDataSO boardData;
    public bool isPlayerTurn = true;
    public bool canRollDice = true;
    public int playerPosition = 0;
    public int enemyPosition = 0;
    public Tile[] grid;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else{
            Destroy(gameObject);
            return;
        }
    }

    public struct Tile
    {
        public bool hasSnake;
        public bool hasLadder;
        public int snakeEndPos;
        public int ladderEndPos;
        public TileColor tileColor;
        public enum TileColor
        {
            Red,
            Blue,
            Green,
            Yellow,
            White
        };

        public Tile(bool a)
        {
            hasSnake = false;
            hasLadder = false;
            snakeEndPos = -1;
            ladderEndPos = -1;
            tileColor = TileColor.White;
        }
    }

    void Start()
    {
        InitializeGrid();
        playerPosition = -1;
    }

    void InitializeGrid()
    {
        grid = new Tile[100];

        // foreach(int index in boardData.redTiles)
        // {
        //     grid[index].tileColor = Tile.TileColor.Red;
        // }

        // foreach(int index in boardData.blueTiles)
        // {
        //     grid[index].tileColor = Tile.TileColor.Blue;
        // }

        // foreach(int index in boardData.greenTiles)
        // {
        //     grid[index].tileColor = Tile.TileColor.Green;
        // }

        // foreach(int index in boardData.yellowTiles)
        // {
        //     grid[index].tileColor = Tile.TileColor.Yellow;
        // }

        // foreach(int index in boardData.whiteTiles)
        // {
        //     grid[index].tileColor = Tile.TileColor.White;
        // }

        foreach(var snake in boardData.snakes)
        {
            grid[snake.endPos-1].hasSnake = true;
            grid[snake.endPos-1].snakeEndPos = snake.startPos-1;
        }

        foreach(var ladder in boardData.ladders)
        {
            grid[ladder.startPos-1].hasLadder = true;
            grid[ladder.startPos-1].ladderEndPos = ladder.endPos-1;
        }
    }

    public int CalculateEnd(int startPos, int diceOutcome, Effects effects)
    {
        // startPos += diceOutcome;
        
        // if(startPos == hasSnake)
        // {
             
        // }

        return -1;
    }

    public void CheckPlayerDrag(int diceRoll, int droppedTileIndex)
    {
        Debug.Log("Player dropped on tile: " + droppedTileIndex);
        // Find the correct tile based on dice roll and effects
        int correctTile = CalculateEnd(playerPosition, diceRoll, currentPlayerEffect);

        // Check where player was dropped
        if(droppedTileIndex != correctTile)
        {
            // Move player to starting tile cuz theyre wrong
            UndoMove(playerPosition, true);
        }
        else
        {
            // Move player to dropped tile
            playerPosition = droppedTileIndex;
        }
    }

    public void HandlePlayerTurn()
    {
        // Reset player variables
        currentPlayerEffect = Effects.None;
        
    }
    
    public void HandleEnemyTurn()
    {
        // Reset enemy variables
        currentEnemyEffect = Effects.None;
        
        // Roll dice for enemy
        int diceRoll = Random.Range(1, 7);

        // Check and decide if enemy uses item

        // Check where that dice roll lands

    }

    public void UndoMove(int startPos, bool isPlayer)
    {
        if(isPlayer)
        {
            // Move player back to startPos
            // Set turn to enemy turn
            isPlayerTurn = false;
        }
        else
        {
            // Move enemy back to startPos
            isPlayerTurn = true;
        }
    }
    
    public void HandleSnakeKilled()
    {
        Debug.Log("Snake has been killed!");
        GameUIManager.instance.HideSnake();
        // Handle end of game logic here
    }
}
