using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class HUD : EasyDraw
    {
        Player player;
        public HUD() : base(800, 600, false) //size is the same as the game window
        {
            Start();
        }
        void Start()
        {
            player = game.FindObjectOfType(typeof(Player)) as Player;
        }
    }
}
