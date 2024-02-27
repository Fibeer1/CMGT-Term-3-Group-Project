using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;
using TiledMapParser;

namespace GXPEngine
{
    class Player : Sprite
    {
        PlayerData data;
        EnemyData enemy;

        Sound running;
        Sound bite;
        Sound shoot;
        Sound hurt;
        Sound death;

        SoundChannel runningSound;

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

        //Color Indicator values
        public float[] colorIndicationRGB = new float[3];
        float colorIndicatorTimer = 0.1f;
        public bool showColorIndicator;
        public bool playHurtSound = false;
        public bool canTakeDamage = true;

        private float biteSizeModifier = 2f;

        bool outsideBorders => x < width / 2 || x > game.width - width / 2 || y < height / 2 || y > game.height - height / 2;

        public Player() : base("Unicorn.png")
        {
            data = ((MyGame)game).playerData;
            enemy = ((MyGame)game).enemyData;

            running = new Sound(data.runSound, true, false);
            bite = new Sound(data.biteSound, false, false);
            shoot = new Sound(data.shootSound, false, false);
            hurt = new Sound(data.damageSound, false, false);
            death = new Sound(data.dieSound, false, false);

            stamina = data.stamina;

            biteCD = data.biteCD;
            hornCD = data.hornCD;

            SetOrigin(width / 2, height / 2);            
            horn = new Sprite("Horn.png", false, false);
            horn.SetXY(75, -75);
            AddChild(horn);
            hornArrow = new Sprite("HornArrow.png", false, false);
            hornArrow.SetOrigin(hornArrow.width / 2, hornArrow.height / 2);
            SetScaleXY(data.scale, data.scale);

            runningSound = running.Play();
        }
        private void Update()
        {
            if (level == null)
            {
                level = parent as Level;
                hornArrow.parent = level; //Remove this as soon as we come up with a better way to find an already instantiated object in a newly instantiated object :))))))))))))))
            }
            camera.SetXY(x, y);
            Movement();
            StaminaManagement();
            HandleBiteAttack();
            HandleHornAttack();
            HandleColorIndication();
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

                runningSound.Mute = false;
            }
            else if (Input.GetKey('D'))
            {
                dx += data.speed;
                scaleX = data.scale;
                facingRight = true;

                runningSound.Mute = false;
            }

            //jumping
            if (Input.GetKeyDown('W') && canJump)
            {
                speedY -= data.jumpHeight;
            }

            //gravity
            speedY += data.gravity;

            dy += speedY;

            Collision colInfoX = MoveUntilCollision(dx, 0);
            if (colInfoX != null)
            {
                if (colInfoX.other is Enemy)
                {
                    Enemy enemyColl = colInfoX.other as Enemy;
                    if (canTakeDamage)
                    {
                        colorIndicationRGB[0] = 1;
                        colorIndicationRGB[1] = 0;
                        colorIndicationRGB[2] = 0;
                        if (enemyColl.type == "Normal")
                        {
                            stamina -= enemy.normalDamage;
                        }
                        else if (enemyColl.type == "Crisp")
                        {
                            stamina -= enemy.burningDamage;
                        }
                        playHurtSound = true;
                        showColorIndicator = true;
                    }
                }
                if (colInfoX.other is CollisionTile)
                {
                    CollisionTile tile = colInfoX.other as CollisionTile;
                    if (tile.type == "Death")
                    {
                        Restart();
                    }
                }
                if (colInfoX.other is Finish)
                {
                    runningSound.Stop();
                    ((MyGame)game).StartLevel(Utils.Random(0, 5));
                }
                if (colInfoX.other is EnemyTrigger)
                {
                    EnemyTrigger trigger = colInfoX.other as EnemyTrigger;
                    level.NewEnemies();
                    trigger.LateDestroy();
                }
            }

            Collision colInfoY = MoveUntilCollision(0, dy);
            if (colInfoY != null)
            {
                if (colInfoY.normal.y > 0)
                {
                    speedY = 0;
                }
                else if (colInfoY.normal.y < 0)
                {
                    speedY = 0;
                    canJump = true;
                }
                
                if (colInfoY.other is CollisionTile)
                {
                    CollisionTile tile = colInfoY.other as CollisionTile;
                    if (tile.type == "Death")
                    {
                        Restart();
                    }
                }
                if (colInfoY.other is Enemy)
                {
                    Enemy enemyColl = colInfoY.other as Enemy;
                    if (canTakeDamage)
                    {
                        colorIndicationRGB[0] = 1;
                        colorIndicationRGB[1] = 0;
                        colorIndicationRGB[2] = 0;
                        if (enemyColl.type == "Normal")
                        {
                            stamina -= enemy.normalDamage;
                        }
                        else
                        {
                            stamina -= enemy.burningDamage;
                        }
                        playHurtSound = true;
                        showColorIndicator = true;
                    }
                }
                if (colInfoY.other is Finish)
                {
                    runningSound.Stop();
                    ((MyGame)game).StartLevel(Utils.Random(0, 5));
                }
            }
            else
            {
                canJump = false;
            }

            HandleSounds(dx, canJump);
        }

        private void HandleSounds(float dx, bool grounded)
        {
            if (playHurtSound)
            {
                hurt.Play();
                playHurtSound = false;
            }

            if (dx == 0)
            {
                runningSound.Mute = true;
            }
            if (!grounded)
            {
                runningSound.Mute = true;
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
                stamina -= Time.deltaTime / data.staminaRate;
            }
            else if (stamina < 0)
            {
                Restart();
            }
        }

        private void HandleBiteAttack()
        {
            if (Input.GetMouseButtonDown(0) && biteCDTimer <= 0)
            {
                BiteParticle biteParticle = new BiteParticle(biteSizeModifier);
                AddChild(biteParticle);
                biteCDTimer = biteCD;

                bite.Play();
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
                hornArrow.SetXY(x + (facingRight ? 80 * scaleX : -50), y - 30 * scaleY / 2);
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
                shoot.Play();
            }
            if (hornCDTimer > 0)
            {
                hornCDTimer -= 0.0175f;
            }
        }

        private void HandleColorIndication()
        {
            //The player changes color for a part of a second when taking damage or being healed
            if (showColorIndicator)
            {
                canTakeDamage = false;
                SetColor(colorIndicationRGB[0], colorIndicationRGB[1], colorIndicationRGB[2]);
                if (horn.alpha > 0)
                {
                    horn.SetColor(colorIndicationRGB[0], colorIndicationRGB[1], colorIndicationRGB[2]);
                }
                colorIndicatorTimer -= 0.01f;
                if (colorIndicatorTimer <= 0)
                {
                    SetColor(1, 1, 1);
                    if (horn.alpha > 0)
                    {
                        horn.SetColor(1, 1, 1);
                    }
                    colorIndicatorTimer = 0.1f;
                    showColorIndicator = false;
                    canTakeDamage = true;
                }
            }
        }
        public void SetSpawnPoint()
        {
            spawnX = x;
            spawnY = y;
        }
        void SetSpawnPosition()
        {
            SetXY(spawnX, spawnY);
        }
        void Restart()
        {
            runningSound.Stop();
            death.Play();
            MyGame mainGame = game.FindObjectOfType<MyGame>();
            mainGame.StartLevel(mainGame.currentLevelIndex);
        }
    }
}
