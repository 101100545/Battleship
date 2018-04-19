// '' <summary>
// '' AttackResult gives the result after a shot has been made.
// '' </summary>
public class AttackResult
{
	private ResultOfAttack _value;
	private Ship _ship;
	private string _text;
	private int _row;
	private int _column;

	// '' <summary>
	// '' The result of the attack
	// '' </summary>
	// '' <value>The result of the attack</value>
	// '' <returns>The result of the attack</returns>
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

	// '' <summary>
	// '' Set the _Value to the PossibleAttack value, and the _Ship to the ship
	// '' </summary>
	// '' <param name="value">either hit, miss, destroyed, shotalready</param>
	// '' <param name="ship">the ship information</param>
	public AttackResult(ResultOfAttack value, Ship ship, string text, int row, int column) : this(value, text, row, column)
	{
		_ship = ship;
	}

	// '' <summary>
	// '' Displays the textual information about the attack
	// '' </summary>
	// '' <returns>The textual information about the attack</returns>
	public override string ToString()
	{
		if (_ship == null)
		{
			return Text;
		}

		return Text + " " + _ship.Name;
	}
}