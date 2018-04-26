using System;

// The SeaGridAdapter allows for the change in a sea grid view. Whenever a ship is
// presented it changes the view into a sea tile instead of a ship tile.
public class SeaGridAdapter : ISeaGrid
{
	private SeaGrid _myGrid;

	// Create the SeaGridAdapter, with the grid, and it will allow it to be changed
	public SeaGridAdapter(SeaGrid grid)
	{
		_myGrid = grid;
		_myGrid.Changed += new EventHandler(MyGrid_Changed);

		Changed += new EventHandler(GameController.GridChanged);
	}

	// MyGrid_Changed causes the grid to be redrawn by raising a changed event
	private void MyGrid_Changed(object sender, EventArgs e)
	{
		Changed(this, e);
	}

	// Changes the discovery grid. Where there is a ship we will sea water
	public TileView this[int x, int y]
	{
		get
		{
			TileView result = _myGrid[x, y];

			if (result == TileView.Ship)
			{
				return TileView.Sea;
			}
			else
			{
				return result;
			}
		}
	}

	// Indicates that the grid has been changed
	public event EventHandler Changed;

	// Get the width of a tile
	public int Width
	{
		get
		{
			return _myGrid.Width;
		}
	}

	public int Height
	{
		get
		{
			return _myGrid.Height;
		}
	}

	public AttackResult HitTile(int row, int col)
	{
		return _myGrid.HitTile(row, col);
	}

}