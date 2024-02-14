using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;

namespace GXPEngine
{
    class Player : Sprite
    {
        PlayerData data;

        //General variables
        public int score;
        public int healthPoints = 5;
        int maxHealth;
        public float stamina = 100; // need to come up with stamina values, how much you lose and gain in each case
        bool isDead = false;

        //Movement variables       
        float speedX = 0;
        float speedY = 0;
        float speedAcceleration = 0.5f;
        float maxSpeed = 5;
        bool canJump = false;

        public bool facingRight = true;

        //Bite attack variables
        public float biteCDTimer = 0;
        public float biteCD = 0.5f; //Default value is 0.5f

        //Horn attack variables
       
        public List<Enemy> enemies = new List<Enemy>();
        public Enemy target;
        public float hornCDTimer = 0;
        public float hornCD = 3; //Default value is 3f
        float hornRadius = 300;
        Sprite horn;
        public Sprite hornArrow;
        

        public Level level;

        bool outsideBorders => x < width / 2 || x > game.width - width / 2 || y < height / 2 || y > game.height - height / 2;

        public Player() : base("Unicorn.png")
        {
            data = ((MyGame)game).playerData;

            SetOrigin(width / 2, height / 2);
            SetPosition();
            maxHealth = healthPoints;
            horn = new Sprite("Horn.png", false, false);
            horn.SetXY(32, -36);
            AddChild(horn);
            hornArrow = new Sprite("HornArrow.png", false, false);
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

            Movement();
            //HandleGravity();
            //HandleMovement();
            //HandleJumping();
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
                scaleX = -1;
                facingRight = false;
            }
            else if (Input.GetKey('D'))
            {
                dx += data.speed;
                scaleX = 1;
                facingRight = true;
            }

            //jumping
            if (Input.GetKey('W') && canJump)
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
            }
            else
            {
                canJump = false;
            }

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
                if (delta < hornRadius)
                {
                    target = enemy;
                    break;
                }
            }
            if (target != null && hornCDTimer <= 0)
            {
                hornArrow.SetXY(x + (facingRight ? 30 : -30), y - 30);
                float xPos = target.x - x;
                float yPos = target.y - y;
                float rotationModifier = facingRight ? 95 : 85;
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
        void SetPosition()
        {
            speedX = 0;
            speedY = 0;
            //SetXY(level.spawnPoint.x, level.spawnPoint.y);
            SetXY(game.width / 2, game.height / 2);
        }
    }
}
