using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class EnemyData
    {
        const float normalMarshmallowSpeed = 0;
        const float normalMarshmallowJumpHeightMin = 5;
        const float normalMarshmallowJumpHeightMax = 15;
        const float normalMarshmallowDamage = 20;
        const float normalMarshmallowStaminaRegen = 50;

        const float burningMarshmallowSpeed = 4;
        const float burningMarshmallowSpeedMin = 2;
        const float burningMarshmallowSpeedMax = 6;
        const float burningMarshmallowJumpHeight = 0;
        const float burningMarshmallowDamage = 40;
        const float burningMarshmallowStaminaRegen = 25;
        const float burningMarshMallowMaxMovement = 200;

        const float shooterMarshmallowDamage = 80;

        const float gravityStrength = 0.5f;
        const float jumpPower = 10;

        public float normalSpeed
        {
            get
            {
                return normalMarshmallowSpeed;
            }
        }

        public float normalJumpHeightMin
        {
            get
            {
                return normalMarshmallowJumpHeightMin;
            }
        }
        public float normalJumpHeightMax
        {
            get
            {
                return normalMarshmallowJumpHeightMax;
            }
        }

        public float normalDamage
        {
            get
            {
                return normalMarshmallowDamage;
            }
        }

        public float normalStaminaRegen
        {
            get
            {
                return normalMarshmallowStaminaRegen;
            }
        }

        public float burningSpeed
        {
            get
            {
                return burningMarshmallowSpeed;
            }
        }

        public float burningSpeedMin
        {
            get
            {
                return burningMarshmallowSpeedMin;
            }
        }
        public float burningSpeedMax
        {
            get
            {
                return burningMarshmallowSpeedMax;
            }
        }

        public float burningJumpHeight
        {
            get
            {
                return burningMarshmallowJumpHeight;
            }
        }

        public float burningDamage
        {
            get
            {
                return burningMarshmallowDamage;
            }
        }

        public float burningStaminaRegen
        {
            get
            {
                return burningMarshmallowStaminaRegen;
            }
        }

        public float burningMaxMovement
        {
            get
            {
                return burningMarshMallowMaxMovement;
            }
        }

        public float shooterDamage
        {
            get
            {
                return shooterMarshmallowDamage;
            }
        }

        public float gravity
        {
            get
            {
                return gravityStrength;
            }
        }
        public float jumpHeight
        {
            get
            {
                return jumpPower;
            }
        }
        public EnemyData()
        {

        }
    }
}
