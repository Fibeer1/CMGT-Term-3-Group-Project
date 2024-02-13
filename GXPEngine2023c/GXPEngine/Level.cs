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
            //Spawn enemies at specific places depending on the level, after that spawn the player

            //spawnPoint = new Transformable();
            //spawnPoint.SetXY(game.width / 2, game.height / 2);
            //AddChild(spawnPoint as GameObject);            
            player = new Player();
            AddChild(player);
            Enemy enemy = new Enemy();
            enemy.SetXY(game.width / 2, game.height / 2);
            AddChild(enemy);
            enemy.Start(); //change this, start method either has to be private or shouldn't exist
            player.target = enemy;
        }
    }
}
