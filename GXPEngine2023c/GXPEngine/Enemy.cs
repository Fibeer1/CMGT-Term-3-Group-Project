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
        public string type; //Can be Normal or Crisp
        Player player;
        EnemyData data;
        public Level level;
        float speedY = 0;
        float speedX = 3;

        float crispMoveTimer = 0;
        int crispMoveDirection = 1;

        bool outsideBorders => x < width / 2 || x > game.width - width / 2 || y < height / 2 || y > game.height - height / 2;
        public Enemy() : base("Enemy.png")
        {
            
        }
        public void Start()
        {
            data = ((MyGame)game).enemyData;

            int patternRNG = Utils.Random(0, 2);
            if (patternRNG == 0)
            {
                pattern = "Horizontal";
            }
            else
            {
                pattern = "Vertical";
            }

            int typeRNG = Utils.Random(0, 3);
            if (typeRNG == 0)
            {
                type = "Crisp";
                SetColor(0.75f, 0.25f, 0.25f);
                collider.isTrigger = true;
            }
            else if (typeRNG == 1)
            {
                type = "Shooter";
                pattern = "";
                SetColor(0.25f, 0.25f, 0.75f);
            }
            else
            {
                type = "Normal";
            }
            level = parent as Level;
            player = level.player;
            player.enemies.Add(this);
            SetOrigin(width / 2, height / 2);
            SetScaleXY(1.75f, 1.75f);
        }
        private void Update()
        {
            HandleMovement();
        }
        private void HandleMovement()
        {
            if (pattern == "Vertical")
            {
                float dy = 0;
                //gravity
                speedY += data.gravity;

                dy += speedY;

                Collision colInfo = MoveUntilCollision(0, dy);
                if (colInfo != null)
                {
                    
                    if (colInfo.normal.y < 0)
                    {
                        speedY = 0;
                    }
                    speedY -= Utils.Random(data.normalJumpHeightMin, data.normalJumpHeightMax);
                    if (colInfo.normal.y > 0)
                    {
                        speedY = 0;
                    }
                    if (colInfo.other is Player)
                    {
                        if (player.canTakeDamage)
                        {
                            player.colorIndicationRGB[0] = 1;
                            player.colorIndicationRGB[1] = 0;
                            player.colorIndicationRGB[2] = 0;
                            if (type == "Normal")
                            {
                                player.stamina -= data.normalDamage;
                            }
                            else
                            {
                                player.stamina -= data.burningDamage;
                            }
                            player.showColorIndicator = true;
                        }
                        speedY = 0;
                    }
                }
            }
            if (pattern == "Horizontal")
            {
                float dx = 0;

                if (crispMoveTimer <= 0)
                {
                    crispMoveTimer = data.burningMaxMovement;
                    crispMoveDirection *= -1;
                    scaleX = -scaleX;
                }
                else if (crispMoveTimer > 0)
                {
                    dx += speedX * crispMoveDirection;
                    crispMoveTimer -= speedX;
                }
                Collision colInfo = MoveUntilCollision(dx, 0);
                if (colInfo != null)
                {
                    if (colInfo.other is Player)
                    {
                        if (player.canTakeDamage)
                        {
                            player.colorIndicationRGB[0] = 1;
                            player.colorIndicationRGB[1] = 0;
                            player.colorIndicationRGB[2] = 0;
                            if (type == "Normal")
                            {
                                player.stamina -= data.normalDamage;
                            }
                            else
                            {
                                player.stamina -= data.burningDamage;
                            }                           
                            player.showColorIndicator = true;
                        }
                    }
                    if (colInfo.normal.x != 0)
                    {
                        crispMoveTimer = data.burningMaxMovement - crispMoveTimer;
                        crispMoveDirection *= -1;
                        speedX = Utils.Random(data.burningSpeedMin, data.burningSpeedMax);
                        scaleX = -scaleX;
                    }
                }
            }
        }
        void OnCollision(GameObject other)
        {
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
