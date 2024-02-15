using System;                                   
using GXPEngine;                                
using System.Drawing;                           

public class MyGame : Game {

	public int currentLevel;
	
	public PlayerData playerData;
	public EnemyData enemyData;

	public MyGame() : base(800, 600, false, false)
	{
		playerData = new PlayerData();
		enemyData = new EnemyData();

		targetFps = 60;
		Menu menu = new Menu("Main Menu");
		AddChild(menu);
		currentLevel = 0;
	}

	public void StartLevel(int levelIndex)
    {
		DestroyChildren();
		Level level = new Level(levelIndex);
		AddChild(level);
    }

	private void DestroyChildren()
    {
		for (int i = 0; i < game.GetChildCount(); i++)
		{
			GameObject child = game.GetChildren()[i];
			child.LateRemove();
			child.LateDestroy();
		}
	}
	static void Main()
	{
		new MyGame().Start();
	}
}