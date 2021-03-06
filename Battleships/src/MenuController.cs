using SwinGameSDK;
using System;

// '' <summary>
// '' The menu controller handles the drawing and user interactions
// '' from the menus in the game. These include the main menu, game
// '' menu and the settings m,enu.
// '' </summary>
public static class MenuController
{
	private static string[][] _menuStructure = {

		new string[] {"PLAY", "SETUP", "SCORES", "RST SCORES", "RULES", "MENUCOLOR", "QUIT",},
		new string[] {"RETURN", "SURRENDER", "QUIT"},
		new string[] {"EASY", "MEDIUM", "HARD", "FONT"},
    new string[] {"White","Purple","Red"},
	};

	private const int MENU_TOP = 575;
	private const int MENU_LEFT = 30;
	private const int MENU_GAP = 0;

	private const int BUTTON_WIDTH = 75;
	private const int BUTTON_HEIGHT = 15;
	private const int BUTTON_SEP = (BUTTON_WIDTH + MENU_GAP);
	private const int TEXT_OFFSET = 0;

	private const int MAIN_MENU = 0;
	private const int GAME_MENU = 1;
	private const int SETUP_MENU = 2;
    private const int COLOR_MENU = 3;

    private const int MAIN_MENU_PLAY_BUTTON = 0;
	private const int MAIN_MENU_SETUP_BUTTON = 1;
	private const int MAIN_MENU_TOP_SCORES_BUTTON = 2;
    private const int MAIN_MENU_RST_SCORES = 3; //RST SCORE
    private const int MAIN_MENU_RULES_BUTTON = 4;
  private const int MAIN_MENU_COLOR_MENU_BUTTON = 5;
    private const int MAIN_MENU_QUIT_BUTTON = 6;
    private const int SETUP_MENU_EASY_BUTTON = 0;
	private const int SETUP_MENU_MEDIUM_BUTTON = 1;
	private const int SETUP_MENU_HARD_BUTTON = 2;
    private const int MAIN_MENU_CHANGE_FONT_BUTTON = 3;
    private const int SETUP_MENU_EXIT_BUTTON = 3;
	private const int GAME_MENU_RETURN_BUTTON = 0;
	private const int GAME_MENU_SURRENDER_BUTTON = 1;
	private const int GAME_MENU_QUIT_BUTTON = 2;
  
      private const int COLOR_MENU_WHITE = 0;
    private const int COLOR_MENU_PURPLE = 1;
    private const int COLOR_MENU_RED = 2;
  
  private static Color MENU_COLOR = SwinGame.RGBAColor(2, 167, 252, 255);
  
	//private static Color MENU_COLOR = SwinGame.RGBAColor(2, 167, 252, 255);
	private static Color HIGHLIGHT_COLOR = SwinGame.RGBAColor(1, 57, 86, 255);

	// '' <summary>
	// '' Handles the processing of user input when the main menu is showing
	// '' </summary>
	public static void HandleMainMenuInput()
	{
		HandleMenuInput(MAIN_MENU, 0, 0);
	}

	// '' <summary>
	// '' Handles the processing of user input when the main menu is showing
	// '' </summary>
	public static void HandleSetupMenuInput()
	{
		bool handled = HandleMenuInput(SETUP_MENU, 1, 1);
		if (!handled)
		{
			HandleMenuInput(MAIN_MENU, 0, 0);
		}
	}

	// '' <summary>
	// '' Handle input in the game menu.
	// '' </summary>
	// '' <remarks>
	// '' Player can return to the game, surrender, or quit entirely
	// '' </remarks>
	public static void HandleGameMenuInput()
	{
		HandleMenuInput(GAME_MENU, 0, 0);
	}
  
    public static void HandleColorMenuInput()
    {
        bool handled = HandleMenuInput(COLOR_MENU, 1, 2);
        if (!handled)
        {
            HandleMenuInput(MAIN_MENU, 0, 0);
            
        }
        
    }

    //Handles input for the specified menu.
    //<param name="menu">the identifier of the menu being processed</param>
    //<param name="level">the vertical level of the menu</param>
    //<param name="xOffset">the xoffset of the menu</param>
    //<returns>false if a clicked missed the buttons. This can be used to check prior menus.</returns>
    private static bool HandleMenuInput(int menu, int level, int xOffset)
	{
		if (SwinGame.KeyTyped(KeyCode.EscapeKey))
		{
			GameController.EndCurrentState();
			return true;
		}

		if (SwinGame.MouseClicked(MouseButton.LeftButton))
		{
			for (int i = 0; i <= _menuStructure[menu].Length - 1; i++)
			{
				// IsMouseOver the i'th button of the menu
				if (IsMouseOverMenu(i, level, xOffset))
				{
					PerformMenuAction(menu, i);
					return true;
				}
			}

			if (level > 0)
			{
				// none clicked - so end this sub menu
				GameController.EndCurrentState();
			}
		}

		return false;
	}

	// '' <summary>
	// '' Draws the main menu to the screen.
	// '' </summary>
	public static void DrawMainMenu()
	{
		// Clears the Screen to Black
		// SwinGame.DrawText("Main Menu", Color.White, GameFont("ArialLarge"), 50, 50)
		DrawButtons(MAIN_MENU);
	}

	// '' <summary>
	// '' Draws the Game menu to the screen
	// '' </summary>
	public static void DrawGameMenu()
	{
		// Clears the Screen to Black
		// SwinGame.DrawText("Paused", Color.White, GameFont("ArialLarge"), 50, 50)
		DrawButtons(GAME_MENU);
	}

	// '' <summary>
	// '' Draws the settings menu to the screen.
	// '' </summary>
	// '' <remarks>
	// '' Also shows the main menu
	// '' </remarks>
	public static void DrawSettings()
	{
		// Clears the Screen to Black
		// SwinGame.DrawText("Settings", Color.White, GameFont("ArialLarge"), 50, 50)
		DrawButtons(MAIN_MENU);
		DrawButtons(SETUP_MENU, 1, 1);
	}

    public static void DrawMenuColor()
    {
        DrawButtons(MAIN_MENU);
        DrawButtons(COLOR_MENU, 1, 2);
    }
  
	private static void DrawButtons(int menu)
	{
		DrawButtons(menu, 0, 0);
	}

	// '' <summary>
	// '' Draws the menu at the indicated level.
	// '' </summary>
	// '' <param name="menu">the menu to draw</param>
	// '' <param name="level">the level (height) of the menu</param>
	// '' <param name="xOffset">the offset of the menu</param>
	// '' <remarks>
	// '' The menu text comes from the _menuStructure field. The level indicates the height
	// '' of the menu, to enable sub menus. The xOffset repositions the menu horizontally
	// '' to allow the submenus to be positioned correctly.
	// '' </remarks>
	private static void DrawButtons(int menu, int level, int xOffset)
	{
		int btnTop = (MENU_TOP - ((MENU_GAP + BUTTON_HEIGHT) * level));
		Rectangle toDraw = new Rectangle();

		for (int i = 0; i <= (_menuStructure[menu].Length - 1); i++)
		{
			int btnLeft = (MENU_LEFT + (BUTTON_SEP * (i + xOffset)));
			toDraw.X = (btnLeft + TEXT_OFFSET);
			toDraw.Y = (btnTop + TEXT_OFFSET);
			toDraw.Width = BUTTON_WIDTH;
			toDraw.Height = BUTTON_HEIGHT;

			// TODO(Xavier): DrawTextLines
			// SwinGame.DrawTextLines (_menuStructure [menu] [i], MENU_COLOR, Color.Black, GameResources.GameFont ("Menu"), FontAlignment.AlignCenter, toDraw);
			SwinGame.DrawText(_menuStructure[menu][i], Color.White, btnLeft, btnTop);

			if ((SwinGame.MouseDown(MouseButton.LeftButton) && IsMouseOverMenu(i, level, xOffset)))
			{
				SwinGame.DrawRectangle(HIGHLIGHT_COLOR, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
			}
		}
	}

	// '' <summary>
	// '' Determined if the mouse is over one of the button in the main menu.
	// '' </summary>
	// '' <param name="button">the index of the button to check</param>
	// '' <returns>true if the mouse is over that button</returns>
	private static bool IsMouseOverButton(int button)
	{
		return IsMouseOverMenu(button, 0, 0);
	}

	// '' <summary>
	// '' Checks if the mouse is over one of the buttons in a menu.
	// '' </summary>
	// '' <param name="button">the index of the button to check</param>
	// '' <param name="level">the level of the menu</param>
	// '' <param name="xOffset">the xOffset of the menu</param>
	// '' <returns>true if the mouse is over the button</returns>
	private static bool IsMouseOverMenu(int button, int level, int xOffset)
	{
		int btnTop = (MENU_TOP - ((MENU_GAP + BUTTON_HEIGHT) * level));
		int btnLeft = (MENU_LEFT + (BUTTON_SEP * (button + xOffset)));
		return UtilityFunctions.IsMouseInRectangle(btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
	}

	// '' <summary>
	// '' A button has been clicked, perform the associated action.
	// '' </summary>
	// '' <param name="menu">the menu that has been clicked</param>
	// '' <param name="button">the index of the button that was clicked</param>
	private static void PerformMenuAction(int menu, int button)
	{
		switch (menu)
		{
			case MAIN_MENU:
				PerformMainMenuAction(button);
				break;
			case SETUP_MENU:
				PerformSetupMenuAction(button);
				break;
			case GAME_MENU:
				PerformGameMenuAction(button);
				break;
            case COLOR_MENU:
                PerformColorChangeAction(button);
                break;
        }
	}

	// '' <summary>
	// '' The main menu was clicked, perform the button's action.
	// '' </summary>
	// '' <param name="button">the button pressed</param>
	private static void PerformMainMenuAction(int button)
	{
		switch (button)
		{
			case MAIN_MENU_PLAY_BUTTON:
				GameController.StartGame();
				break;
			case MAIN_MENU_SETUP_BUTTON:
				GameController.AddNewState(GameState.AlteringSettings);
				break;
			case MAIN_MENU_TOP_SCORES_BUTTON:
				GameController.AddNewState(GameState.ViewingHighScores);
				break;
            case MAIN_MENU_RST_SCORES:
                HighScoreController.ClearHighScore();
                break;
            case MAIN_MENU_RULES_BUTTON:
                GameController.AddNewState(GameState.Instructions);
                break;
            case MAIN_MENU_COLOR_MENU_BUTTON:
                GameController.AddNewState(GameState.AlteringMenuColor);
                break;
            case MAIN_MENU_QUIT_BUTTON:
				GameController.EndCurrentState();
				break;
		}
	}

	// '' <summary>
	// '' The setup menu was clicked, perform the button's action.
	// '' </summary>
	// '' <param name="button">the button pressed</param>
  
	private static void PerformSetupMenuAction(int button)
	{
		switch (button)
		{
			case SETUP_MENU_EASY_BUTTON:
				GameController.SetDifficulty(AIOption.Hard);
				break;
			case SETUP_MENU_MEDIUM_BUTTON:
				GameController.SetDifficulty(AIOption.Hard);
				break;
      case SETUP_MENU_HARD_BUTTON:
        GameController.SetDifficulty(AIOption.Hard);
        break;
      case MAIN_MENU_CHANGE_FONT_BUTTON:
         GameResources.LoadChangedFontResources();
        break;
        }

        GameController.EndCurrentState();
	}

    private static void PerformColorChangeAction(int button)
    {
        switch (button)
        {
            case COLOR_MENU_WHITE:
                ChangeMenuColor(Color.White);
                break;
            case COLOR_MENU_PURPLE:
                ChangeMenuColor(Color.Purple);
                break;
            case COLOR_MENU_RED:
                ChangeMenuColor(Color.Red);
                break;
        }
        GameController.EndCurrentState();


    }

    private static void ChangeMenuColor(Color _color)
    {
        MENU_COLOR = _color;
    }

	// '' <summary>
	// '' The game menu was clicked, perform the button's action.
	// '' </summary>
	// '' <param name="button">the button pressed</param>
	private static void PerformGameMenuAction(int button)
	{
		switch (button)
		{
			case GAME_MENU_RETURN_BUTTON:
				GameController.EndCurrentState();
				break;
			case GAME_MENU_SURRENDER_BUTTON:
				GameController.EndCurrentState();
				// end game menu
				GameController.EndCurrentState();
				// end game
				break;
			case GAME_MENU_QUIT_BUTTON:
				GameController.AddNewState(GameState.Quitting);
				break;
		}
	}
}