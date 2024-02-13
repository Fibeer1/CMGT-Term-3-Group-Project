using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Player : Sprite
    {
        //General variables
        public int score;
        public int healthPoints = 5;
        int maxHealth;
        float stamina = 100;// need to come up with stamina values, how much you lose and gain in each case
        bool isDead = false;        

        Transformable spawnPoint;

        //Movement variables       
        float speedX = 0;
        float speedY = 0;
        float speedAcceleration = 0.5f;
        float maxSpeed = 5;
        bool canJump = true;

        bool outsideBorders => x < width / 2 || x > game.width - width / 2 || y < height / 2 || y > game.height - height / 2;

        public Player() : base("Square.png")
        {
            SetOrigin(width / 2, height / 2);
            spawnPoint = game.FindObjectOfType<MyGame>().spawnPoint;
            SetPosition();
            maxHealth = healthPoints;
        }
        private void Update()
        {
            //HandleGravity();
            HandleMovement();
            HandleJumping();
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
            }
            if (Input.GetKey(Key.D) && speedX < maxSpeed)
            {
                speedX += speedAcceleration;
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
                    Translate(-scaleX / 6, 0);
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
        void SetPosition()
        {
            speedX = 0;
            speedY = 0;
            SetXY(spawnPoint.x, spawnPoint.y);
        }
    }
}
