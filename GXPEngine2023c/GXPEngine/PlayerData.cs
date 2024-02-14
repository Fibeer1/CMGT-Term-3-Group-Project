using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class PlayerData
    {
        const float movementSpeed = 5;
        const float heightJump = 25;
        const float gravityStrength = 1;

        const float biteCDTime = 0.5f;
        const float hornCDTime = 3;

        public float speed
        {
            get
            {
                return movementSpeed;
            }
        }

        public float jumpHeight
        {
            get
            {
                return heightJump;
            }
        }

        public float gravity
        {
            get
            {
                return gravityStrength;
            }
        }

        public float biteCD
        {
            get
            {
                return biteCDTime;
            }
        }

        public float hornCD
        {
            get
            {
                return hornCDTime;
            }
        }

        public PlayerData()
        {

        }
    }
}
