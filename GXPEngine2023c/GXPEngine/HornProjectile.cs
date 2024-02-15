using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class HornProjectile : Sprite
    {
        float speed = 10;
        public float lifeTime = 3;
        Player player;
        bool outsideBorders => x < width / 2 || x > game.width - width / 2 || y < height / 2 || y > game.height - height / 2;
        public HornProjectile() : base("HornProjectile.png")
        {
            collider.isTrigger = true;
            player = game.FindObjectOfType<Player>();
            SetOrigin(width / 2, height / 2);
            player = game.FindObjectOfType<Player>();
            SetXY(player.hornArrow.x, player.hornArrow.y);
            rotation = player.hornArrow.rotation;
            x += player.facingRight ? 10 : -10;

            
        }
        private void Update()
        {
            lifeTime -= 0.025f;
            if (lifeTime <= 0)
            {
                LateRemove();
                LateDestroy();
            }
            Move(0, -speed);
        }
        void OnCollision(GameObject other)
        {
            bool shouldDestroy = false;
            if (other is Enemy)
            {
                player.score += 1;
                Enemy enemy = other as Enemy;
                enemy.Die();
                player.target = null;
                player.stamina += 150;
                shouldDestroy = true;
            }
            if (other is CollisionTile)
            {
                shouldDestroy = true;
            }
            if (shouldDestroy)
            {
                LateRemove();
                LateDestroy();
            }
        }
    }
}
