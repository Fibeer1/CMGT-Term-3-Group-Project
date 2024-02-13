using System;                                   
using GXPEngine;                                
using System.Drawing;                           

public class MyGame : Game {
	//test
	Player player;
	public Transformable spawnPoint;
	SoundChannel audioSource;

	public MyGame() : base(800, 600, false, false)
	{
		targetFps = 60;
		spawnPoint = new Transformable();
		spawnPoint.SetXY(width / 2, height / 2);
		player = new Player();
		AddChild(player);
		Enemy enemy = new Enemy();
		enemy.SetXY(width / 2, height / 2);
		AddChild(enemy);
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