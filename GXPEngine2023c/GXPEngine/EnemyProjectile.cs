using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class EnemyProjectile : Sprite
    {
        EnemyData data;

        string direction;

        public EnemyProjectile(string pdirection) : base ("HornProjectile.png", false, true)
        {
            data = ((MyGame)game).enemyData;

            direction = pdirection;
            collider.isTrigger = true;

            rotation = 90;

            SetOrigin(width/2, height/2);
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            float dx = 0;

            if (direction == "left")
            {
                dx = -data.projectileSpeed;
            }
            else if (direction == "right")
            {
                dx = data.projectileSpeed;
            }

            Collision colInfo = MoveUntilCollision(dx, 0);

            if (colInfo != null)
            {
                if (colInfo.other is Player)
                {
                    Player player = (Player)colInfo.other;
                    player.canTakeDamage = true;
                    if (player.canTakeDamage)
                    {
                        player.colorIndicationRGB[0] = 1;
                        player.colorIndicationRGB[1] = 0;
                        player.colorIndicationRGB[2] = 0;
                        player.stamina -= data.shooterDamage;
                        player.showColorIndicator = true;
                        player.playHurtSound = true;
                    }
                }

                this.LateDestroy();
            }
        }
    }
}
