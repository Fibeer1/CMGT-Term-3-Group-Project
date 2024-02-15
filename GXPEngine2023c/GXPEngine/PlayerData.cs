using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class PlayerData
    {
        const float maxStamina = 1000;

        //higher value means slower stamina reduction
        const float staminaReduceRate = 10;

        const float movementSpeed = 5;
        const float heightJump = 17.5f;
        const float gravityStrength = 1;
        const float spriteScale = 0.5f;

        const float biteCDTime = 0.5f;
        const float hornCDTime = 3;
        const float hornFiringRange = 300;

        public float stamina
        {
            get
            {
                return maxStamina;
            }
        }

        public float staminaRate
        {
            get
            {
                return staminaReduceRate;
            }
        }

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
        public float scale
        {
            get
            {
                return spriteScale;
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

        public float hornRadius
        {
            get
            {
                return hornFiringRange;
            }
        }

        public PlayerData()
        {

        }
    }
}
