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

        public BiteParticle(float pscale) : base("BiteParticle.png", 8, 1)
        {
            SetOrigin(width / 2, height / 2);
            player = game.FindObjectOfType<Player>();
            SetXY(50, -5);
            collider.isTrigger = true;
            SetScaleXY(pscale);
        }
        private void Update()
        {
            HandleLifeCycle();
        }
        private void HandleLifeCycle()
        {
            Animate(0.5f);
            lifeTime -= 0.05f;
            if (lifeTime <= 0)
            {
                LateRemove();
                LateDestroy();
            }
        }
    }
}
