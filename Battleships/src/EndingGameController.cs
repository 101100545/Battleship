using SwinGameSDK;

// '' <summary>
// '' The EndingGameController is responsible for managing the interactions at the end
// '' of a game.
// '' </summary>
public static class EndingGameController
{
	// '' <summary>
	// '' Draw the end of the game screen, shows the win/lose state
	// '' </summary>
	public static void DrawEndOfGame()
	{
		UtilityFunctions.DrawField(GameController.ComputerPlayer.PlayerGrid, GameController.ComputerPlayer, true);
		UtilityFunctions.DrawSmallField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);

		string whatShouldIPrint;
		if (GameController.HumanPlayer.IsDestroyed)
		{
			whatShouldIPrint = "YOU LOSE!";
		}
		else
		{
			whatShouldIPrint = "-- WINNER --";
		}


		// SwinGame.DrawTextLines (whatShouldIPrint, Color.White, Color.Transparent, GameResources.GameFont ("ArialLarge"), FontAlignment.AlignCenter, toDraw);
		Rectangle toDraw = new Rectangle();
		toDraw.X = 0;
		toDraw.Y = 250;
		toDraw.Width = SwinGame.ScreenWidth();
		toDraw.Height = SwinGame.ScreenHeight();
		SwinGame.DrawText(whatShouldIPrint, Color.White, Color.Transparent, GameResources.GameFont("ArialLarge"), FontAlignment.AlignCenter, toDraw);
	}

	// '' <summary>
	// '' Handle the input during the end of the game. Any interaction
	// '' will result in it reading in the highsSwinGame.
	// '' </summary>
	public static void HandleEndOfGameInput()
	{
		if (SwinGame.MouseClicked(MouseButton.LeftButton) || SwinGame.KeyTyped(KeyCode.ReturnKey) || SwinGame.KeyTyped(KeyCode.EscapeKey))
		{
			HighScoreController.ReadHighScore(GameController.HumanPlayer.Score);
			GameController.EndCurrentState();
		}
	}
}