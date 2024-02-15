﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Player : Sprite
    {
        PlayerData data;

        //General variables
        public int score;
        public float stamina; // need to come up with stamina values, how much you lose and gain in each case
        bool isDead = false;

        //Movement variables       
        float speedY = 0;
        bool canJump = false;

        public bool facingRight = true;

        //Bite attack variables
        public float biteCDTimer = 0;
        public float biteCD; //Default value is 0.5f

        //Horn attack variables
       
        public List<Enemy> enemies = new List<Enemy>();
        public Enemy target;
        public float hornCDTimer = 0;
        public float hornCD; //Default value is 3f
        Sprite horn;
        public Sprite hornArrow;

        float spawnX;
        float spawnY;

        public Camera camera;
        public Level level;

        bool outsideBorders => x < width / 2 || x > game.width - width / 2 || y < height / 2 || y > game.height - height / 2;

        public Player() : base("Unicorn.png")
        {
            data = ((MyGame)game).playerData;

            stamina = data.stamina;

            biteCD = data.biteCD;
            hornCD = data.hornCD;

            SetOrigin(width / 2, height / 2);            
            horn = new Sprite("Horn.png", false, false);
            horn.SetXY(32, -36);
            AddChild(horn);
            hornArrow = new Sprite("HornArrow.png", false, false);
            hornArrow.SetOrigin(hornArrow.width / 2, hornArrow.height / 2);
            SetScaleXY(data.scale, data.scale);
            spawnX = x;
            spawnY = y;

            //if (level == null)
            //{
            //    level = game.FindObjectOfType<Level>();
            //    hornArrow.parent = level; //Remove this as soon as we come up with a better way to find an already instantiated object in a newly instantiated object :))))))))))))))
            //}
            //level.AddChild(hornArrow);
            //foreach (Enemy enemy in level.GetChildren()) //Gets all enemies in the level
            //{
            //    enemies.Add(enemy);
            //}
        }
        private void Update()
        {            
            camera.SetXY(x, y);
            Movement();
            StaminaManagement();
            HandleBiteAttack();
            HandleHornAttack();
        }

        private void Movement()
        {
            float dx = 0;
            float dy = 0;

            //side to side movement
            if (Input.GetKey('A'))
            {
                dx -= data.speed;
                scaleX = -data.scale;
                facingRight = false;
            }
            else if (Input.GetKey('D'))
            {
                dx += data.speed;
                scaleX = data.scale;
                facingRight = true;
            }

            //jumping
            if (Input.GetKeyDown('W') && canJump)
            {
                speedY -= data.jumpHeight;
            }

            //gravity
            speedY += data.gravity;

            dy += speedY;

            MoveUntilCollision(dx, 0);
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
                    canJump = true;
                }
                if (colInfo is CollisionTile)
                {
                    

                }
            }
            else
            {
                canJump = false;
            }

        }

        private void StaminaManagement()
        {
            float maxStamina = data.stamina;

            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }

            if (stamina > 0)
            {
                stamina -= Time.deltaTime / 1000;
            }
            else if(stamina < 0)
            {
                stamina = 0;
            }
        }

        private void HandleBiteAttack()
        {
            if (Input.GetMouseButtonDown(0) && biteCDTimer <= 0)
            {
                BiteParticle biteParticle = new BiteParticle();
                AddChild(biteParticle);
                biteCDTimer = biteCD;
            }
            if (biteCDTimer > 0)
            {
                biteCDTimer -= 0.0175f; //Basically Time.deltatime in unity
            }
        }
        private void HandleHornAttack()
        {
            horn.alpha = hornCDTimer > 0 ? 0 : 1;
            hornArrow.alpha = target == null || hornCDTimer > 0 ? 0 : 1;
            foreach (Enemy enemy in enemies)
            {
                float delta = DistanceTo(enemy);
                if (delta < data.hornRadius)
                {
                    target = enemy;
                    break;
                }
            }
            if (target != null && hornCDTimer <= 0)
            {
                hornArrow.SetXY(x + (facingRight ? 30 : -30), y - 30);
                float xPos = target.x - hornArrow.x;
                float yPos = target.y - hornArrow.y;
                float rotationModifier = 90;
                float angle = Mathf.Atan2(yPos, xPos) * 360 / ((float)Math.PI * 2) + rotationModifier;
                hornArrow.rotation = angle;
            }
            if (Input.GetMouseButtonDown(1) && hornCDTimer <= 0 && target != null)
            {
                HornProjectile hornProjectile = new HornProjectile();
                level.AddChild(hornProjectile);
                hornCDTimer = hornCD;
            }
            if (hornCDTimer > 0)
            {
                hornCDTimer -= 0.0175f;
            }
        }
        void SetSpawnPosition()
        {
            SetXY(spawnX, spawnY);
        }
    }
}
