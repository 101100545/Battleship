using System;
using System.Collections.Generic;

//<summary>
//The AIEasyPlayer is a type of AIPlayer where it will randomly fires on the grid
//</summary>
public class AIEasyPlayer : AIPlayer
{
    // '' <summary>
    // '' Private enumarator for AI states. currently there are two states,
    // '' the AI can be searching for a ship, or if it has found a ship it will
    // '' target the same ship
    // '' </summary>
    private enum AIStates
    {
        Searching,
        TargetingShip,
    }

    private AIStates _currentState = AIStates.Searching;
   

    public AIEasyPlayer(BattleShipsGame controller) : base(controller)
    {
    }

    // '' <summary>
    // '' GenerateCoordinates should generate random shooting coordinates
    // '' only when it has not found a ship, or has destroyed a ship and 
    // '' needs new shooting coordinates
    // '' </summary>
    // '' <param name="row">the generated row</param>
    // '' <param name="column">the generated column</param>
    protected override void GenerateCoords(ref int row, ref int column)
    {
        while (((row < 0) || ((column < 0) || ((row >= EnemyGrid.Height) || ((column >= EnemyGrid.Width) || (EnemyGrid[row, column] != TileView.Sea))))))
        {
            switch (_currentState)
            {
                case AIStates.Searching:
                    SearchCoords(ref row, ref column);
                    break;
                default:
                    throw new ApplicationException("AI has gone in an invalid state");
            }
        }
    }

    protected override void ProcessShot(int row, int col, AttackResult result)
    {
        _currentState = AIStates.Searching;
    }

    // '' <summary>
    // '' SearchCoords will randomly generate shots within the grid as long as its not hit that tile already
    // '' </summary>
    // '' <param name="row">the generated row</param>
    // '' <param name="column">the generated column</param>
    private void SearchCoords(ref int row, ref int column)
    {
        row = _random.Next(0, EnemyGrid.Height);
        column = _random.Next(0, EnemyGrid.Width);
    }
}