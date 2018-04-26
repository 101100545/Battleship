using System;
using System.Collections.Generic;

// The AIMediumPlayer is a type of AIPlayer where it will try and destroy a ship
// if it has found a ship
public class AIMediumPlayer : AIPlayer
{
	// Private enumarator for AI states. currently there are two states,
	// the AI can be searching for a ship, or if it has found a ship it will
	// target the same ship
	private enum AIStates
	{
		Searching,
		TargetingShip,
	}

	private AIStates _currentState = AIStates.Searching;
	private Stack<Location> _targets = new Stack<Location>();

	public AIMediumPlayer(BattleShipsGame controller) : base(controller)
	{
	}

	// GenerateCoordinates should generate random shooting coordinates
	// only when it has not found a ship, or has destroyed a ship and 
	// needs new shooting coordinates
	protected override void GenerateCoords(ref int row, ref int column)
	{
		while (((row < 0) || ((column < 0) || ((row >= EnemyGrid.Height) || ((column >= EnemyGrid.Width) || (EnemyGrid[row, column] != TileView.Sea))))))
		{
			switch (_currentState)
			{
				case AIStates.Searching:
					SearchCoords(ref row, ref column);
					break;
				case AIStates.TargetingShip:
					TargetCoords(ref row, ref column);
					break;
				default:
					throw new ApplicationException("AI has gone in an imvalid state");
			}
		}
	}

	// TargetCoords is used when a ship has been hit and it will try and destroy
	// this ship
	private void TargetCoords(ref int row, ref int column)
	{
		Location l = _targets.Pop();
		if ((_targets.Count == 0))
		{
			_currentState = AIStates.Searching;
		}

		row = l.Row;
		column = l.Column;
	}


	// SearchCoords will randomly generate shots within the grid as long as its not hit that tile already
	private void SearchCoords(ref int row, ref int column)
	{
		row = _random.Next(0, EnemyGrid.Height);
		column = _random.Next(0, EnemyGrid.Width);
	}

	// ProcessShot will be called uppon when a ship is found.
	// It will create a stack with targets it will try to hit. These targets
	// will be around the tile that has been hit.
	protected override void ProcessShot(int row, int col, AttackResult result)
	{
		if (result.Value == ResultOfAttack.Hit)
		{
			_currentState = AIStates.TargetingShip;
			AddTarget((row - 1), col);
			AddTarget(row, (col - 1));
			AddTarget((row + 1), col);
			AddTarget(row, (col + 1));
		}
		else if ((result.Value == ResultOfAttack.ShotAlready))
		{
			throw new ApplicationException("Error in AI");
		}
	}

	// AddTarget will add the targets it will shoot onto a stack
	private void AddTarget(int row, int column)
	{
		if (((row >= 0) && ((column >= 0) && ((row < EnemyGrid.Height) && ((column < EnemyGrid.Width) && (EnemyGrid[row, column] == TileView.Sea))))))
		{
			_targets.Push(new Location(row, column));
		}
	}
}