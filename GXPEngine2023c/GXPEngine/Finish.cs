using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class Finish : Sprite
    {


        public Finish() : base("checkers.png", false, true)
        {
            collider.isTrigger = true;
        }
    }
}
