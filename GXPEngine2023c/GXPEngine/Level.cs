using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Level : GameObject
    {
        public Player player;
        public Transformable spawnPoint;
        HUD hud;
        public Level(int index) : base()
        {
            Start();
            
        }
        private void Start()
        {
            //spawn the player, after that spawn enemies at specific places depending on the level

            //spawnPoint = new Transformable();
            //spawnPoint.SetXY(game.width / 2, game.height / 2);
            //AddChild(spawnPoint as GameObject);
            player = new Player();
            AddChild(player);

            AddChild(new Terrain(game.width / 2, game.height / 4 * 3, 800, 50));
            AddChild(new Terrain(game.width / 2, game.height / 4, 200, 50));
            Enemy enemy1 = new Enemy(100, game.height - 100);
            AddChild(enemy1);
            enemy1.Start();
            Enemy enemy2 = new Enemy(game.width - 100, game.height - 100); 
            AddChild(enemy2);
            enemy2.Start();
            //Need to find a way to add the level as a parent before the start method is called

            //HUD gets added last
            hud = new HUD();
            AddChild(hud);
            hud.Start();
        }
    }
}
