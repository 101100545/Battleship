using System;

// '' <summary>
// '' Tile knows its location on the grid, if it is a ship and if it has been 
// '' shot before
// '' </summary>
public class Tile
{
	private int _rowValue;
	private int _columnValue;
	private Ship _ship;
	private bool _shot;

	public bool Shot
	{
		get
		{
			return _shot;
		}
		set
		{
			_shot = value;
		}
	}

	public int Row
	{
		get
		{
			return _rowValue;
		}
	}

	public int Column
	{
		get
		{
			return _columnValue;
		}
	}

	public Ship Ship
	{
		get
		{
			return _ship;
		}
		set
		{
			if (_ship == null)
			{
				_ship = value;
				if (value != null)
				{
					_ship.AddTile(this);
				}

			}
			else
			{
				throw new InvalidOperationException(("There is already a ship at [" + (Row + (", " + (Column + "]")))));
			}

		}
	}

	public Tile(int row, int col, Ship ship)
	{
		_rowValue = row;
		_columnValue = col;
		_ship = ship;
	}

	// '' <summary>
	// '' Clearship will remove the ship from the tile
	// '' </summary>
	public void ClearShip()
	{
		_ship = null;
	}

	// '' <summary>
	// '' View is able to tell the grid what the tile is
	// '' </summary>
	public TileView View
	{
		get
		{
			// if there is no ship in the tile
			if (_ship == null)
			{
				// and the tile has been hit
				if (_shot)
				{
					return TileView.Miss;
				}
				else
				{
					// and the tile hasn't been hit
					return TileView.Sea;
				}
			}
			else
			{
				// if there is a ship and it has been hit
				if (_shot)
				{
					return TileView.Hit;
				}
				else
				{
					// if there is a ship and it hasn't been hit
					return TileView.Ship;
				}
			}
		}
	}

	internal void Shoot()
	{
		if (false == Shot)
		{
			Shot = true;
			if (_ship != null)
			{
				_ship.Hit();
			}

		}
		else
		{
			throw new ApplicationException("You have already shot this square");
		}

	}
}