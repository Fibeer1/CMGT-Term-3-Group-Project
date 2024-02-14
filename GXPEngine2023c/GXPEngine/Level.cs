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
            Enemy enemy1 = new Enemy(100, game.height - 100);
            AddChild(enemy1);
            enemy1.Start();
            Enemy enemy2 = new Enemy(game.width - 100, game.height - 100);
            AddChild(enemy2);
            enemy2.Start();
        }
    }
}
