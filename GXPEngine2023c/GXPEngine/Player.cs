﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;

namespace GXPEngine
{
    class Player : Sprite
    {
        //General variables
        public int score;
        public int healthPoints = 5;
        int maxHealth;
        float stamina = 100; // need to come up with stamina values, how much you lose and gain in each case
        bool isDead = false;

        //Movement variables       
        float speedX = 0;
        float speedY = 0;
        float speedAcceleration = 0.5f;
        float maxSpeed = 5;
        bool canJump = true;

        public bool facingRight = true;

        //Bite attack variables


        //Horn attack variables
        List<Enemy> enemies;
        public Enemy target;
        float hornCD = 0; //Default value is 0.5f
        Sprite horn;
        public Sprite hornArrow;

        public Level level;

        bool outsideBorders => x < width / 2 || x > game.width - width / 2 || y < height / 2 || y > game.height - height / 2;

        public Player() : base("Unicorn.png")
        {
            SetOrigin(width / 2, height / 2);
            SetPosition();
            maxHealth = healthPoints;
            horn = new Sprite("Horn.png");
            horn.SetXY(32, -36);
            AddChild(horn);
            hornArrow = new Sprite("HornArrow.png");
            hornArrow.SetOrigin(hornArrow.width / 2, hornArrow.height / 2);
            //level.AddChild(hornArrow);
            //foreach (Enemy enemy in level.GetChildren()) //Gets all enemies in the level
            //{
            //    enemies.Add(enemy);
            //}
        }
        private void Update()
        {
            if (level == null)
            {
                level = game.FindObjectOfType<Level>();
                hornArrow.parent = level; //Remove this as soon as we come up with a better way to find an already instantiated object in a newly instantiated object :))))))))))))))
            }
            //HandleGravity();
            HandleMovement();
            HandleJumping();
            HandleBiteAttack();
            HandleHornAttack();
        }
        private void HandleGravity()
        {
            speedY += 1;
            if (y > game.height - height / 2)
            {
                y = game.height - height / 2;
                speedY = 0;
                canJump = true;
            }
        }
        private void HandleMovement()
        {
            if (Input.GetKey(Key.A) && speedX > -maxSpeed)
            {
                speedX -= speedAcceleration;
                if (scaleX != -1)
                {
                    scaleX = -1;
                    facingRight = false;
                }
            }
            if (Input.GetKey(Key.D) && speedX < maxSpeed)
            {
                speedX += speedAcceleration;
                if (scaleX != 1)
                {
                    scaleX = 1;
                    facingRight = true;
                }
            }
            else if (!Input.GetKey(Key.A) && !Input.GetKey(Key.D) && speedX != 0)
            {
                //speed has to interpolate to 0
                speedX += speedAcceleration / 2 * (0 - speedX);
                //Console.WriteLine(speed);
            }
            if (outsideBorders)
            {
                speedX = 0;
                speedY = 0;
                if (x < width / 2) //left border
                {
                    Translate(scaleX / 6, 0);
                }
                else if (x > game.width - width) //right border
                {
                    Translate(scaleX / 6, 0);
                }
                else if (y < height / 2) //top border
                {
                    Translate(0, scaleY / 6);
                }
                else if (y > game.height) //bottom border
                {
                    Translate(0, -scaleY / 6);
                }
            }           
            Move(speedX, speedY);
        }
        private void HandleJumping()
        {
            if (Input.GetKeyDown(Key.SPACE) && canJump)
            {
                speedY = -15;
                canJump = false;
            }
        }
        private void HandleBiteAttack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                BiteParticle biteParticle = new BiteParticle();
                AddChild(biteParticle);
            }
        }
        private void HandleHornAttack()
        {
            horn.alpha = hornCD > 0 ? 0 : 1;
            if (target == null || hornCD > 0)
            {
                hornArrow.alpha = 0;
            }
            else
            {
                hornArrow.alpha = 1;
            }
            if (target != null && hornCD <= 0)
            {
                hornArrow.SetXY(x, y);
                float xPos = target.x - x;
                float yPos = target.y - y;
                float rotationModifier = 90;
                float angle = Mathf.Atan2(yPos, xPos) * 360 / ((float)Math.PI * 2) + rotationModifier;
                hornArrow.rotation = angle;
            }
            if (Input.GetMouseButtonDown(1) && hornCD <= 0)
            {
                HornProjectile hornProjectile = new HornProjectile();
                level.AddChild(hornProjectile);
                hornCD = 0.5f;
            }
            if (hornCD > 0)
            {
                hornCD -= 0.0175f;
            }
        }
        void SetPosition()
        {
            speedX = 0;
            speedY = 0;
            //SetXY(level.spawnPoint.x, level.spawnPoint.y);
            SetXY(game.width / 2, game.height / 2);
        }
    }
}
