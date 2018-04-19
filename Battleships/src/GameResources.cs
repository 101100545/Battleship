using SwinGameSDK;
using System.Collections.Generic;
using System;

public static class GameResources
{
	private static Dictionary<string, Bitmap> _images = new Dictionary<string, Bitmap>();
	private static Dictionary<string, Font> _fonts = new Dictionary<string, Font>();
	private static Dictionary<string, SoundEffect> _sounds = new Dictionary<string, SoundEffect>();
	private static Dictionary<string, Music> _music = new Dictionary<string, Music>();
	private static Bitmap _background;
	private static Bitmap _animation;
	private static Bitmap _loaderFull;
	private static Bitmap _loaderEmpty;
	private static Font _loadingFont;
	private static SoundEffect _startSound;

	private static void LoadFonts()
	{
		NewFont("ArialLarge", "arial.ttf", 80);
		NewFont("Courier", "cour.ttf", 14);
		NewFont("CourierSmall", "cour.ttf", 8);
		NewFont("Menu", "ffaccess.ttf", 8);
	}

	private static void LoadImages()
	{
		// Backgrounds:
		NewImage("Menu", "main_page.jpg");
		NewImage("Discovery", "discover.jpg");
		NewImage("Deploy", "deploy.jpg");

		// Deployment:
		NewImage("LeftRightButton", "deploy_dir_button_horiz.png");
		NewImage("UpDownButton", "deploy_dir_button_vert.png");
		NewImage("SelectedShip", "deploy_button_hl.png");
		NewImage("PlayButton", "deploy_play_button.png");
		NewImage("RandomButton", "deploy_randomize_button.png");

		// Ships:
		for (int i = 1; i <= 5; i++)
		{
			NewImage(("ShipLR" + i), ("ship_deploy_horiz_" + (i + ".png")));
			NewImage(("ShipUD" + i), ("ship_deploy_vert_" + (i + ".png")));
		}

		// Explosions:
		NewImage("Explosion", "explosion.png");
		NewImage("Splash", "splash.png");
	}

	private static void LoadSounds()
	{
		NewSound("Error", "error.wav");
		NewSound("Hit", "hit.wav");
		NewSound("Sink", "sink.wav");
		NewSound("Miss", "watershot.wav");
		NewSound("Winner", "winner.wav");
		NewSound("Lose", "lose.wav");
	}

	private static void LoadMusic()
	{
		NewMusic("Background", "horrordrone.mp3");
	}

	// '' <summary>
	// '' Gets a Font Loaded in the Resources
	// '' </summary>
	// '' <param name="font">Name of Font</param>
	// '' <returns>The Font Loaded with this Name</returns>
	public static Font GameFont(string font)
	{
		return _fonts[font];
	}

	// '' <summary>
	// '' Gets an Image loaded in the Resources
	// '' </summary>
	// '' <param name="image">Name of image</param>
	// '' <returns>The image loaded with this name</returns>
	public static Bitmap GameImage(string image)
	{
		return _images[image];
	}

	// '' <summary>
	// '' Gets an sound loaded in the Resources
	// '' </summary>
	// '' <param name="sound">Name of sound</param>
	// '' <returns>The sound with this name</returns>
	public static SoundEffect GameSound(string sound)
	{
		return _sounds[sound];
	}

	// '' <summary>
	// '' Gets the music loaded in the Resources
	// '' </summary>
	// '' <param name="music">Name of music</param>
	// '' <returns>The music with this name</returns>
	public static Music GameMusic(string music)
	{
		return _music[music];
	}

	// '' <summary>
	// '' The Resources Class stores all of the Games Media Resources, such as Images, Fonts
	// '' Sounds, Music.
	// '' </summary>
	public static void LoadResources()
	{
		int width = SwinGame.ScreenWidth();
		int height = SwinGame.ScreenHeight();

		SwinGame.ChangeScreenSize(800, 600);
		ShowLoadingScreen();

		ShowMessage("Loading fonts...", 0);
		LoadFonts();

		ShowMessage("Loading images...", 1);
		LoadImages();

		ShowMessage("Loading sounds...", 2);
		LoadSounds();

		ShowMessage("Loading music...", 3);
		LoadMusic();


		ShowMessage("Game loaded...", 5);

		EndLoadingScreen(width, height);
	}

	private static void ShowLoadingScreen()
	{
		_background = SwinGame.LoadBitmap(SwinGame.PathToResource("SplashBack.png", ResourceKind.BitmapResource));
		SwinGame.DrawBitmap(_background, 0, 0);
		SwinGame.RefreshScreen();
		SwinGame.ProcessEvents();

		_animation = SwinGame.LoadBitmap(SwinGame.PathToResource("SwinGameAni.jpg", ResourceKind.BitmapResource));
		_loadingFont = SwinGame.LoadFont(SwinGame.PathToResource("arial.ttf", ResourceKind.FontResource), 12);
		_startSound = Audio.LoadSoundEffect(SwinGame.PathToResource("SwinGameStart.ogg", ResourceKind.SoundResource));
		_loaderFull = SwinGame.LoadBitmap(SwinGame.PathToResource("loader_full.png", ResourceKind.BitmapResource));
		_loaderEmpty = SwinGame.LoadBitmap(SwinGame.PathToResource("loader_empty.png", ResourceKind.BitmapResource));

		PlaySwinGameIntro();
	}

	private static void PlaySwinGameIntro()
	{
		const int ANI_CELL_COUNT = 11;
		Audio.PlaySoundEffect(_startSound);


		for (int i = 0; i <= ANI_CELL_COUNT - 1; i++)
		{
			SwinGame.DrawBitmap(_background, 0, 0);

			SwinGame.RefreshScreen();
			SwinGame.ProcessEvents();
		}

	}

	private static void ShowMessage(string message, int number)
	{
		const int BG_Y = 453;
		const int TX = 310;
		const int TY = 493;
		const int TW = 200;
		const int TH = 25;
		const int BG_X = 279;
	
		// const int STEPS = 5;

		SwinGame.DrawBitmap(_loaderEmpty, BG_X, BG_Y);
		SwinGame.DrawCell(_loaderFull, 0, BG_X, BG_Y);


		// int fullW = (260 * number) / STEPS;
		// SwinGame.DrawBitmapPart(_LoaderFull, 0, 0, fullW, 66, BG_X, BG_Y)

		Rectangle toDraw = new Rectangle();
		toDraw.X = TX;
		toDraw.Y = TY;
		toDraw.Width = TW;
		toDraw.Height = TH;


		// SwinGame.DrawTextLines (message, Color.White, Color.Transparent, _LoadingFont, FontAlignment.AlignCenter, toDraw);
		// SwinGame.DrawTextLines(message, Color.White, Color.Transparent, _LoadingFont, FontAlignment.AlignCenter, TX, TY, TW, TH)
		SwinGame.DrawText(message, Color.White, Color.Transparent, _loadingFont, FontAlignment.AlignCenter, toDraw);
		SwinGame.DrawText(message, Color.White, Color.Transparent, _loadingFont, FontAlignment.AlignCenter, toDraw);

		SwinGame.RefreshScreen();
		SwinGame.ProcessEvents();
	}

	private static void EndLoadingScreen(int width, int height)
	{
		SwinGame.ProcessEvents();


		SwinGame.ClearScreen();
		SwinGame.RefreshScreen();
		SwinGame.FreeFont(_loadingFont);
		SwinGame.FreeBitmap(_background);
		SwinGame.FreeBitmap(_animation);
		SwinGame.FreeBitmap(_loaderEmpty);
		SwinGame.FreeBitmap(_loaderFull);

		// Audio.FreeSoundEffect (_StartSound);

		SwinGame.ChangeScreenSize(width, height);
	}

	private static void NewFont(string fontName, string filename, int size)
	{
		_fonts.Add(fontName, SwinGame.LoadFont(SwinGame.PathToResource(filename, ResourceKind.FontResource), size));
	}

	private static void NewImage(string imageName, string filename)
	{
		_images.Add(imageName, SwinGame.LoadBitmap(SwinGame.PathToResource(filename, ResourceKind.BitmapResource)));
	}

	private static void NewTransparentColorImage(string imageName, string fileName, Color transColor)
	{
		_images.Add(imageName, SwinGame.LoadBitmap(SwinGame.PathToResource(fileName, ResourceKind.BitmapResource)));
	}

	private static void NewTransparentColourImage(string imageName, string fileName, Color transColor)
	{
		NewTransparentColorImage(imageName, fileName, transColor);
	}

	private static void NewSound(string soundName, string filename)
	{
		_sounds.Add(soundName, Audio.LoadSoundEffect(SwinGame.PathToResource(filename, ResourceKind.SoundResource)));
	}

	private static void NewMusic(string musicName, string filename)
	{
		_music.Add(musicName, Audio.LoadMusic(SwinGame.PathToResource(filename, ResourceKind.SoundResource)));
	}

	private static void FreeFonts()
	{
		foreach (Font obj in _fonts.Values)
		{
			SwinGame.FreeFont(obj);
		}
	}

	private static void FreeImages()
	{
		foreach (Bitmap obj in _images.Values)
		{
			SwinGame.FreeBitmap(obj);
		}
	}

	private static void FreeSounds()
	{
		// foreach (SoundEffect obj in _Sounds.Values) {
		//     Audio.FreeSoundEffect (obj);
		// }
	}

	private static void FreeMusic()
	{
		foreach (Music obj in _music.Values)
		{
			Audio.FreeMusic(obj);
		}
	}

	public static void FreeResources()
	{
		FreeFonts();
		FreeImages();
		FreeMusic();
		FreeSounds();
		SwinGame.ProcessEvents();
	}

}