using SwinGameSDK;


// The AIPlayer is a type of player. It can readomly deploy ships, it also has the
// functionality to generate coordinates and shoot at tiles
public abstract class AIPlayer : Player
{
	// Location can store the location of the last hit made by an
	// AI Player. The use of which determines the difficulty.
	protected class Location
	{
		private int _row;
		private int _column;

		// The row of the shot
		public int Row
		{
			get
			{
				return _row;
			}
			set
			{
				_row = value;
			}
		}

		// The column of the shot
		public int Column
		{
			get
			{
				return _column;
			}
			set
			{
				_column = value;
			}
		}

		// Sets the last hit made to the local variables
		public Location(int row, int column)
		{
			_column = column;
			_row = row;
		}

		// Check if two locations are equal
		public static bool operator ==(Location left, Location right)
		{
			if (object.ReferenceEquals(left, right))
			{
				return true;
			}
			if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
			{
				return false;
			}

			return left.Row == right.Row && left.Column == right.Column;
		}

		// Check if two locations are not equal
		public static bool operator !=(Location left, Location right)
		{
			return !(left == right);
		}

		public override bool Equals(object obj)
		{
			return this == obj as Location;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

	}

	protected AIPlayer(BattleShipsGame game) : base(game)
	{
	}

	// Generate a valid row, column to shoot at
	protected abstract void GenerateCoords(ref int row, ref int column);

	// The last shot had the following result. Child classes can use this
	// to prepare for the next shot.
	protected abstract void ProcessShot(int row, int column, AttackResult result);

	// The AI takes its attacks until its go is over.
	public override AttackResult Attack()
	{
		AttackResult result;
		int row = 0;
		int column = 0;

		do // keep hitting until a miss
		{
			Delay();

			GenerateCoords(ref row, ref column); // generate coordinates for shot
			result = _game.Shoot(row, column); // take shot
			ProcessShot(row, column, result);
		} while (result.Value != ResultOfAttack.Miss && result.Value != ResultOfAttack.GameOver && !SwinGame.WindowCloseRequested());

		return result;
	}

	// Wait a short period to simulate the think time
	private void Delay()
	{
		//"Think time" removed to speed up debugging
		//for (int i = 0; i < 150; i++) {
		// Dont delay if window is closed
		if (SwinGame.WindowCloseRequested()) return;

		//SwinGame.Delay (5);
		SwinGame.ProcessEvents();
		SwinGame.RefreshScreen();
	}

}