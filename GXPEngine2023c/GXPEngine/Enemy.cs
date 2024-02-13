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
        bool outsideBorders => x < -width || x > game.width + width || y < -height || y > game.height + height;
        public Enemy() : base("colors.png")
        {
            player = game.FindObjectOfType<Player>();
            SetOrigin(width / 2, height / 2);
            SetPattern();
        }
        private void Update()
        {
            Move(1, 0);
            delta = x - player.x;
            if (pattern == "LeftNRight")
            {

            }
            else if (pattern == "Chasing")
            {

            }
            else if (pattern == "Fleeing")
            {
                if (delta > 0)
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
            int patternRNG = Utils.Random(0, 4);
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
            }
            if (patternRNG == 3)
            {
                pattern = "Charging";
            }
        }
    }
}
