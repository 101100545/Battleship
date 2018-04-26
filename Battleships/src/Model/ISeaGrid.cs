using System;


// The ISeaGrid defines the read only interface of a Grid. This
// allows each player to see and attack their opponents grid.
public interface ISeaGrid
{
	int Width
	{
		get;
	}

	int Height
	{
		get;
	}

	event EventHandler Changed;

	TileView this[int x, int y]
	{
		get;
	}

	AttackResult HitTile(int row, int col);
}