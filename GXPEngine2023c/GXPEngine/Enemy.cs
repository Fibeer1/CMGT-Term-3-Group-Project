using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Enemy : Sprite
    {
        //Movement pattern parameters
        string pattern;
        Player player;
        float delta;
        float distanceTillAction;
        public Level level;
        bool outsideBorders => x < width / 2 || x > game.width - width / 2 || y < height / 2 || y > game.height - height / 2;
        public Enemy(float xPos, float yPos) : base("Enemy.png")
        {
            SetXY(xPos, yPos);
        }
        public void Start()
        {
            level = parent as Level;
            player = level.player;
            player.enemies.Add(this);
            SetOrigin(width / 2, height / 2);
            SetPattern();
        }
        private void Update()
        {
            delta = DistanceTo(player);
            if (pattern == "LeftNRight")
            {

            }
            else if (pattern == "Chasing")
            {

            }
            else if (pattern == "Fleeing")
            {
                if (delta < distanceTillAction)
                {
                    
                }
            }
            else if (pattern == "Charging")
            {

            }
            //Console.WriteLine(DistanceTo(game.FindObjectOfType<Player>()));
            //Console.WriteLine(delta);
        }
        private void SetPattern()
        {
            int patternRNG = 2;//Utils.Random(0, 4);
            if (patternRNG == 0)
            {
                pattern = "LeftNRight";
            }
            if (patternRNG == 1)
            {
                pattern = "Chasing";
                distanceTillAction = 100;
            }
            if (patternRNG == 2)
            {
                pattern = "Fleeing";
                distanceTillAction = 100;
            }
            if (patternRNG == 3)
            {
                pattern = "Charging";
            }
        }
        public void Die()
        {
            player.enemies.Remove(this);
            LateRemove();
            LateDestroy();
        }
    }
}
