using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class Terrain : Sprite
    {
        public Terrain() : base("colors.png", false, true)
        {
            width = 20;
            height = 20;
        }

        public Terrain(float px, float py, int pwidth, int pheight) : base("colors.png", false, true)
        {
            width = pwidth;
            height = pheight;

            x = px - width/2;
            y = py - height/2;
        }
    }
}
