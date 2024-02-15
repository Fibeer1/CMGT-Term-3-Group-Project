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
        const float normalMarshmallowJumpHeight = 15;
        const float normalMarshmallowDamage = 20;
        const float normalMarshmallowStaminaRegen = 50;

        const float burningMarshmallowSpeed = 5;
        const float burningMarshmallowJumpHeight = 0;
        const float burningMarshmallowDamage = 40;
        const float burningMarshmallowStaminaRegen = 25;

        const float shooterMarshmallowDamage = 80;

        public float normalSpeed
        {
            get
            {
                return normalMarshmallowSpeed;
            }
        }

        public float normalJumpHeight
        {
            get
            {
                return normalMarshmallowJumpHeight;
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

        public float shooterDamage
        {
            get
            {
                return shooterMarshmallowDamage;
            }
        }

        public EnemyData()
        {

        }
    }
}
