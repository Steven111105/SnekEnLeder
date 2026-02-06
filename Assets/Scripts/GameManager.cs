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
    public SnakeAndLadderSO snakeAndLadderData;
    public bool isPlayerTurn = true;
    public bool canRollDice = true;
    public int playerPosition = 0;
    public int enemyPosition = 0;
    public class Grid
    {
        public Tile[] tiles = new Tile[100];
    }

    public struct Tile
    {
        public bool hasSnake;
        public bool hasLadder;
        public int snakeEndPos;
        public int ladderEndPos;
    }

    void Start()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {
        // Grid grid = new Grid();

        // foreach(var snake in snakeAndLadderData.snakes)
        // {
        //     grid.tiles[snake.startPos].hasSnake = true;
        //     grid.tiles[snake.startPos].snakeEndPos = snake.endPos;
        // }

        // foreach(var ladder in snakeAndLadderData.ladders)
        // {
        //     grid.tiles[ladder.startPos].hasLadder = true;
        //     grid.tiles[ladder.startPos].ladderEndPos = ladder.endPos;
        // }
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
    
}
