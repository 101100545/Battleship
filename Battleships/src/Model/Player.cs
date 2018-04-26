using System;
using System.Collections;
using System.Collections.Generic;

// Player has its own _PlayerGrid, and can see an _EnemyGrid, it can also check if
// all ships are deployed and if all ships are detroyed. A Player can also attach.
public class Player : IEnumerable
{
	protected static Random _random = new Random();
	protected BattleShipsGame _game;

	private Dictionary<ShipName, Ship> _ships = new Dictionary<ShipName, Ship>();
	private SeaGrid _playerGrid; // = new SeaGrid (_Ships);
	private ISeaGrid _enemyGrid;
	private int _shots;
	private int _hits;
	private int _misses;

	// Returns the game that the player is part of.
	public BattleShipsGame Game
	{
		get
		{
			return _game;
		}
		set
		{
			_game = value;
		}
	}

	public ISeaGrid Enemy
	{
		set
		{
			_enemyGrid = value;
		}
	}

	public Player(BattleShipsGame controller)
	{
		_playerGrid = new SeaGrid(_ships);
		_game = controller;

		// for each ship add the ships name so the seagrid knows about them
		foreach (ShipName name in Enum.GetValues(typeof(ShipName)))
		{
			if ((name != ShipName.None))
			{
				_ships.Add(name, new Ship(name));
			}

		}

		RandomizeDeployment();
	}

	// The EnemyGrid is a ISeaGrid because you shouldn't be allowed to see the enemies ships
	public ISeaGrid EnemyGrid
	{
		get
		{
			return _enemyGrid;
		}
		set
		{
			_enemyGrid = value;
		}
	}

	public SeaGrid PlayerGrid
	{
		get
		{
			return _playerGrid;
		}
	}

	public bool ReadyToDeploy
	{
		get
		{
			return _playerGrid.AllDeployed;
		}
	}

	public bool IsDestroyed
	{
		get
		{
			// Check if all ships are destroyed... -1 for the none ship
			return _playerGrid.ShipsKilled == Enum.GetValues(typeof(ShipName)).Length - 1;
		}
	}

	public Ship Ship(ShipName name)
	{
		if ((name == ShipName.None))
		{
			return null;
		}

		return _ships[name];
	}

	public int Shots
	{
		get
		{
			return _shots;
		}
	}

	public int Hits
	{
		get
		{
			return _hits;
		}
	}

	public int Missed
	{
		get
		{
			return _misses;
		}
	}

	public int Score
	{
		get
		{
			if (IsDestroyed)
			{
				return 0;
			}
			else
			{
				return ((Hits * 12) - (Shots - (PlayerGrid.ShipsKilled * 20)));
			}
		}
	}

	public IEnumerator<Ship> GetShipEnumerator()
	{
		Ship[] result = new Ship[_ships.Values.Count];
		_ships.Values.CopyTo(result, 0);
		List<Ship> lst = new List<Ship>();
		lst.AddRange(result);
		return lst.GetEnumerator();
	}

	// Makes it possible to enumerate over the ships the player
	// has.
	public IEnumerator GetEnumerator()
	{
		Ship[] result = new Ship[_ships.Values.Count];
		_ships.Values.CopyTo(result, 0);
		List<Ship> lst = new List<Ship>();
		lst.AddRange(result);
		return lst.GetEnumerator();
	}

	public virtual AttackResult Attack()
	{
		// human does nothing here...
		return null;
	}

	// Shoot at a given row/column

	internal AttackResult Shoot(int row, int col)
	{
		AttackResult result = EnemyGrid.HitTile(row, col);
		switch (result.Value)
		{
			case ResultOfAttack.Destroyed:
			case ResultOfAttack.Hit:
                _shots++;
                _hits++;
				break;
			case ResultOfAttack.Miss:
                _shots++;
                _misses++;
				break;
		}
		return result;
	}

	public void RandomizeDeployment()
	{
		bool placementSuccessful;
		Direction heading;

		// for each ship to deploy in shipist
		foreach (ShipName shipToPlace in Enum.GetValues(typeof(ShipName)))
		{
			if ((shipToPlace == ShipName.None))
			{
				continue;
			}

			placementSuccessful = false;
			while (!placementSuccessful)
			{
				int dir = _random.Next(2);
				int x = _random.Next(0, 11);
				int y = _random.Next(0, 11);
				if ((dir == 0))
				{
					heading = Direction.UpDown;
				}
				else
				{
					heading = Direction.LeftRight;
				}

				// try to place ship, if position unplaceable, generate new coordinates
				try
				{
					PlayerGrid.MoveShip(x, y, shipToPlace, heading);
					placementSuccessful = true;
				}
				catch
				{
					placementSuccessful = false;
				}
			}

		}
	}

}