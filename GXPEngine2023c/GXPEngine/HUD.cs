using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GXPEngine
{
    class HUD : EasyDraw
    {
        Player player;
        public Level level;
        string scoreText;
        Sprite score = new Sprite("UI_score.png");
        Sprite stamina = new Sprite("UI_stamina_back.png");
        Sprite staminaBar = new Sprite("UI_stamina_rainbowline.png");        
        Sprite staminaOverlay = new Sprite("UI_stamina_overlay.png");
        AnimationSprite staminaBarEffect = new AnimationSprite("cloud_frames.png", 4, 1);
        AnimationSprite biteCD = new AnimationSprite("UI_bitecd.png", 4, 2);
        AnimationSprite hornCD = new AnimationSprite("UI_horncd.png", 4, 2);
        Font uiFont = new Font("Concert One", 15);
        HUDData data;
        PlayerData playerData;

        public HUD() : base(1366, 768, false) //size is the same as the game window
        {
            data = ((MyGame)game).hudData;
            playerData = ((MyGame)game).playerData;
        }

        public void Start()
        {
            player = level.player;
            stamina.SetXY(5, 5);
            AddChild(stamina);           
            staminaBar.SetOrigin(0, staminaBar.y / 2);
            staminaBar.SetXY(12, 50);
            AddChild(staminaBar);
            AddChild(staminaBarEffect);
            staminaOverlay.SetXY(7, 45);
            AddChild(staminaOverlay);
            score.SetXY(game.width / 2 - score.width / 2, 10);
            AddChild(score);
            biteCD.SetXY(90, 95);
            biteCD.SetFrame(biteCD.frameCount - 1);
            AddChild(biteCD);
            hornCD.SetXY(165, 95);
            hornCD.SetFrame(hornCD.frameCount - 1);
            AddChild(hornCD);
        }
        private void Update()
        {
            graphics.Clear(Color.Empty);
            HandleStamina();
            //Score
            scoreText = playerData.playerScore.ToString();
            graphics.DrawString(scoreText,
                uiFont,
                Brushes.White, 
                score.x + score.width / 2 - 9,
                score.y + score.height + 5);
            //Lives
            graphics.DrawString("Lives: " + playerData.currentLives, uiFont,
                Brushes.White,
                230, 90);
            HandleBiteCD();
            HandleHornCD();
        }
        private void HandleStamina()
        {
            if (playerData.stamina > 0)
            {
                staminaBar.scaleX = playerData.currentStamina / 1000;
                staminaBarEffect.SetXY(staminaBar.x + staminaBar.width - staminaBarEffect.width / 2, staminaBar.y);
                staminaBarEffect.Animate(0.15f);
            }
        }
        private void HandleBiteCD()
        {
            if (player.biteCDTimer > 0)
            {
                biteCD.Animate(playerData.biteCD / 2);
            }
            else
            {
                biteCD.SetFrame(biteCD.frameCount - 1);
            }
        }
        private void HandleHornCD()
        {
            if (player.hornCDTimer > 0)
            {
                hornCD.Animate(playerData.hornCD / 75);
            }
            else
            {
                hornCD.SetFrame(hornCD.frameCount - 1);
            }
        }
    }
}
