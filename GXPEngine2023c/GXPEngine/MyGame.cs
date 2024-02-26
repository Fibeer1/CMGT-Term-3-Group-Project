using System;                                   
using GXPEngine;                                
using System.Drawing;
using System.Collections.Generic;

class MyGame : Game {

	public Level currentLevel;
	public int currentLevelIndex;
	
	public PlayerData playerData;
	public EnemyData enemyData;
	public HUDData hudData;

	public MyGame() : base(1366, 768, false, false)
	{
		playerData = new PlayerData();
		enemyData = new EnemyData();
		hudData = new HUDData();

		targetFps = 60;
		Menu menu = new Menu("Main Menu");
		AddChild(menu);
		currentLevelIndex = 0;
	}

	public void StartLevel(int levelIndex)
    {
		DestroyChildren();
		Level level = new Level(levelIndex);
		currentLevel = level;
		AddChild(level);
	}

	private void DestroyChildren()
    {
		List<GameObject> children = GetChildren();
		foreach (GameObject child in children)
		{
			child.LateDestroy();
		}
		//for (int i = 0; i < game.GetChildCount(); i++)
		//{
		//	GameObject child = game.GetChildren()[i];
		//	child.LateRemove();
		//	child.LateDestroy();
		//}
	}
	static void Main()
	{
		new MyGame().Start();
	}
}