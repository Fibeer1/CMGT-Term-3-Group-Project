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
        const float staminaReduceRate = 25;

        const float movementSpeed = 7.5f;
        const float heightJump = 17.5f;
        const float gravityStrength = .6f;
        const float spriteScale = 0.5f;

        const float biteCDTime = 0.5f;
        const float hornCDTime = 3;
        const float hornFiringRange = 300;
        const float hornProjectileSpeed = 15;
        const float hornStaminaNum = 25;

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

        public float hornSpeed
        {
            get
            {
                return hornProjectileSpeed;
            }
        }

        public float hornStaminaDrain
        {
            get
            {
                return hornStaminaNum;
            }
        }

        public PlayerData()
        {

        }
    }
}
