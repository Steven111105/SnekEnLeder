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
        Grid grid = new Grid();

        foreach(var snake in snakeAndLadderData.snakes)
        {
            grid.tiles[snake.startPos].hasSnake = true;
            grid.tiles[snake.startPos].snakeEndPos = snake.endPos;
        }

        foreach(var ladder in snakeAndLadderData.ladders)
        {
            grid.tiles[ladder.startPos].hasLadder = true;
            grid.tiles[ladder.startPos].ladderEndPos = ladder.endPos;
        }
    }


    public void CalculateEnd(int startPos, int diceOutcome, Effects effects)
    {
        // startPos += diceOutcome;
        
        // if(startPos == hasSnake)
        // {
             
        // }
    }

    public void CheckPlayerDrag(int startTileIndex, int diceRoll, int droppedTileIndex, Effects effect)
    {
        CalculateEnd(startTileIndex, diceRoll, effect);
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

    public void FalseMovement(int calculatedPos, int endPos)
    {
        if(calculatedPos != endPos)
        {
            // move pawn to calculatedPos
        }
    }
    
}
