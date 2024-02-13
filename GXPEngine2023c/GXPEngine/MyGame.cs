using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {

	Player player;
	public Transformable spawnPoint;
	SoundChannel audioSource;

	public MyGame() : base(800, 600, false, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		targetFps = 60;
		spawnPoint = new Transformable();
		spawnPoint.SetXY(width / 2, height / 2);
		player = new Player();
		AddChild(player);
	}

	public void StartLevel(int levelIndex)
    {
		DestroyChildren();
		Level level = new Level(levelIndex);
    }
	private void DestroyChildren()
    {
		audioSource.Stop();
		for (int i = 0; i < game.GetChildCount(); i++)
		{
			GameObject child = game.GetChildren()[i];
			child.LateRemove();
			child.LateDestroy();
		}
	}
	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}