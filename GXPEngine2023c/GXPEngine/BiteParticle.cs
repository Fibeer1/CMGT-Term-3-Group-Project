using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class BiteParticle : AnimationSprite
    {
        float lifeTime = 0.75f;
        Player player;
        public BiteParticle() : base("BiteParticle.png", 8, 1)
        {            
            SetOrigin(width / 2, height / 2);
            player = game.FindObjectOfType<Player>();
            SetXY(50, -5);
            collider.isTrigger = true;
        }
        private void Update()
        {
            HandleExploding();
        }
        private void HandleExploding()
        {
            Animate(0.5f);
            lifeTime -= 0.05f;
            if (lifeTime <= 0)
            {
                LateRemove();
                LateDestroy();
            }
        }
        void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                Enemy enemy = other as Enemy;                
                player.enemies.Remove(enemy);
                enemy.Die();
                if (player.target == enemy)
                {
                    player.target = null;
                }
                player.score += 1;
            }
        }
    }
}
