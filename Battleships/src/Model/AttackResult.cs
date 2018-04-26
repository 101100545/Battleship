// AttackResult gives the result after a shot has been made.
public class AttackResult
{
	private ResultOfAttack _value;
	private Ship _ship;
	private string _text;
	private int _row;
	private int _column;


	// The result of the attack
	public ResultOfAttack Value
	{
		get
		{
			return _value;
		}
	}

	public Ship Ship
	{
		get
		{
			return _ship;
		}
	}

	public string Text
	{
		get
		{
			return _text;
		}
	}

	public int Row
	{
		get
		{
			return _row;
		}
	}

	public int Column
	{
		get
		{
			return _column;
		}
	}

	public AttackResult(ResultOfAttack value, string text, int row, int column)
	{
		_value = value;
		_text = text;
		_ship = null;
		_row = row;
		_column = column;
	}

	// Set the _Value to the PossibleAttack value, and the _Ship to the ship
	public AttackResult(ResultOfAttack value, Ship ship, string text, int row, int column) : this(value, text, row, column)
	{
		_ship = ship;
	}

	// '' Displays the textual information about the attack
	public override string ToString()
	{
		if (_ship == null)
		{
			return Text;
		}

		return Text + " " + _ship.Name;
	}
}