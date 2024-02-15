using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;

namespace GXPEngine
{
    class Enemy : Sprite
    {
        //Movement pattern parameters
        string pattern;
        string type; //Can be Normal or Crisp
        Player player;
        EnemyData data;
        public Level level;
        float speedY = 0;


        bool outsideBorders => x < width / 2 || x > game.width - width / 2 || y < height / 2 || y > game.height - height / 2;
        public Enemy() : base("Enemy.png")
        {
            
        }
        public void Start()
        {
            data = ((MyGame)game).enemyData;
            int patternRNG = Utils.Random(0, 3);
            if (patternRNG == 0)
            {
                type = "Crisp";
                SetColor(0.75f, 0.25f, 0.25f);
                collider.isTrigger = true;
            }     
            else
            {
                type = "Normal";
            }
            level = parent as Level;
            player = level.player;
            player.enemies.Add(this);
            SetOrigin(width / 2, height / 2);
        }
        private void Update()
        {
            HandleMovement();
        }
        private void HandleMovement()
        {
            if (type == "Normal")
            {
                float dy = 0;
                //gravity
                speedY += data.gravity;

                dy += speedY;

                Collision colInfo = MoveUntilCollision(0, dy);
                if (colInfo != null)
                {                  
                    if (colInfo.normal.y > 0)
                    {
                        speedY = 0;
                    }
                    else if (colInfo.normal.y < 0)
                    {
                        speedY = 0;
                    }
                    speedY -= data.jumpHeight;
                    if (colInfo.other is Player)
                    {
                        if (player.canTakeDamage)
                        {
                            player.colorIndicationRGB[0] = 1;
                            player.colorIndicationRGB[1] = 0;
                            player.colorIndicationRGB[2] = 0;
                            player.stamina -= data.normalDamage;
                            player.showColorIndicator = true;
                        }                        
                        speedY = 0;
                    }
                }
            }
            if (type == "Crisp")
            {
                float dx = 0;

                Collision colInfo = MoveUntilCollision(dx, 0);
                if (colInfo != null)
                {
                    if (colInfo.normal.y > 0)
                    {
                        speedY = 0;
                    }
                    else if (colInfo.normal.y < 0)
                    {
                        speedY = 0;
                    }
                    speedY -= data.jumpHeight;
                }
            }
        }
        void OnCollision(GameObject other)
        {
            if (other is Player)
            {                
                if (type == "Crisp")
                {
                    if (player.canTakeDamage)
                    {
                        player.colorIndicationRGB[0] = 1;
                        player.colorIndicationRGB[1] = 0;
                        player.colorIndicationRGB[2] = 0;
                        player.stamina -= data.burningDamage;
                        player.showColorIndicator = true;
                    }
                }
            }
            if (other is BiteParticle)
            {   
                if (player.target == this)
                {
                    player.target = null;
                }
                if (type == "Normal")
                {
                    player.stamina += data.normalStaminaRegen;
                }
                else if (type == "Crisp")
                {
                    player.stamina += data.burningStaminaRegen;
                }
                player.score += 1;
                Die();
            }
            else if (other is HornProjectile)
            {
                player.score += 2;
                player.target = null;
                Die();
            }
        }
        public void Die()
        {
            player.enemies.Remove(this);
            LateRemove();
            LateDestroy();
        }
    }
}
