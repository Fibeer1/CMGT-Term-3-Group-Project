using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;

namespace GXPEngine
{
    class Enemy : AnimationSprite
    {
        //Movement pattern parameters
        string pattern;
        public string type; //Can be Normal or Crisp

        Player player;
        EnemyData data;
        PlayerData playerData;

        Sound death;

        public Level level;
        float speedY = 0;
        float speedX = 3;

        float crispMoveTimer = 0;
        int crispMoveDirection = 1;

        float shooterCdTimer = 0;

        bool outsideBorders => x < width / 2 || x > game.width - width / 2 || y < height / 2 || y > game.height - height / 2;
        public Enemy() : base("Enemies.png", 2, 1)
        {
            
        }
        public void Start()
        {
            data = ((MyGame)game).enemyData;
            playerData = ((MyGame)game).playerData;

            death = new Sound(data.deathSound, false, false);

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
                collider.isTrigger = true;
            }
            else
            {
                type = "Normal";
            }
            level = parent as Level;
            player = level.player;
            player.enemies.Add(this);
            if (((MyGame)game).completedLevelIndices.Count >= 2)
            {
                SetFrame(0);
            }
            else
            {
                SetFrame(1);
            }
            SetOrigin(width / 2, height / 2);
            SetScaleXY(0.5f, 0.5f);
        }
        private void Update()
        {
            HandleMovement();

            if (type == "Shooter")
            {
                ShooterAttack();
            }
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
                                playerData.currentStamina -= data.normalDamage;
                            }
                            else
                            {
                                playerData.currentStamina -= data.burningDamage;
                            }
                            player.showColorIndicator = true;
                            player.playHurtSound = true;
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
                                playerData.currentStamina -= data.normalDamage;
                            }
                            else
                            {
                                playerData.currentStamina -= data.burningDamage;
                            }                           
                            player.showColorIndicator = true;
                            player.playHurtSound = true;
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

        void ShooterAttack()
        {
            if (shooterCdTimer > 0)
            {
                shooterCdTimer -= Time.deltaTime;
            }
            else if (shooterCdTimer <= 0)
            {
                if (level.player != null)
                {
                    if (level.player.x <= x)
                    {
                        EnemyProjectile enemyProjectile = new EnemyProjectile("left");
                        AddChild(enemyProjectile);
                    }
                    else if (level.player.x > x)
                    {
                        EnemyProjectile enemyProjectile = new EnemyProjectile("right");
                        AddChild(enemyProjectile);
                    }
                }
                shooterCdTimer = data.shooterShotCd;
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
                    playerData.currentStamina += data.normalStaminaRegen;
                }
                else if (type == "Crisp")
                {
                    playerData.currentStamina += data.burningStaminaRegen;
                }
                else if (type == "Shooter")
                {
                    playerData.currentStamina += data.shooterStaminaRegen;
                }
                playerData.playerScore += 1;
                Die();
            }
            else if (other is HornProjectile)
            {
                playerData.playerScore += 2;
                player.target = null;
                Die();
            }
        }
        public void Die()
        {
            death.Play();
            player.enemies.Remove(this);
            LateRemove();
            LateDestroy();
        }
    }
}
