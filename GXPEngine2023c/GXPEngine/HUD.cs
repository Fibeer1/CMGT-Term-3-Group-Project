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
            HandleBiteCD();
            HandleHornCD();
        }
        private void HandleStamina()
        {
            if (playerData.stamina > 0)
            {
                staminaBar.scaleX = player.stamina / 1000;
                staminaBarEffect.SetXY(staminaBar.x + staminaBar.width - staminaBarEffect.width / 2, staminaBar.y);
                staminaBarEffect.Animate(0.15f);
            }
        }
        private void HandleBiteCD()
        {
            if (player.biteCDTimer > 0)
            {
                float angle = 0;
                if (angle < 360)
                {
                    angle = player.biteCDTimer / player.biteCD * 360;
                }
                float textX = 10;
                float textY = 85;
                string text = "Bite CD: ";
                graphics.DrawString(text, uiFont, Brushes.White, textX, textY);
                graphics.FillPie(new SolidBrush(Color.White), textX + text.Length * uiFont.Size - 55, textY + 5, 15, 15, 0, angle);
            }
        }
        private void HandleHornCD()
        {
            if (player.hornCDTimer > 0)
            {
                float angle = 0;
                if (angle < 360)
                {
                    angle = player.hornCDTimer / player.hornCD * 360;
                }
                float textX = 10;
                float textY = 110;
                string text = "Horn CD: ";
                graphics.DrawString(text, uiFont, Brushes.White, textX, textY);
                graphics.FillPie(new SolidBrush(Color.White), textX + text.Length * uiFont.Size - 45, textY + 5, 15, 15, 0, angle);
            }
        }
    }
}
