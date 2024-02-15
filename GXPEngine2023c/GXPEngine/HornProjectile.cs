using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class HornProjectile : Sprite
    {
        PlayerData data;

        public float lifeTime = 3;
        Player player;
        bool outsideBorders => x < width / 2 || x > game.width - width / 2 || y < height / 2 || y > game.height - height / 2;
        public HornProjectile() : base("HornProjectile.png")
        {
            data = ((MyGame)game).playerData;            
            collider.isTrigger = true;
            player = game.FindObjectOfType<Player>();
            SetOrigin(width / 2, height / 2);
            SetXY(player.hornArrow.x, player.hornArrow.y);
            player.stamina -= data.hornStaminaDrain;
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
            Move(0, -data.hornSpeed);
        }
        void OnCollision(GameObject other)
        {
            bool shouldDestroy = false;
            if (other is Enemy)
            {
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
