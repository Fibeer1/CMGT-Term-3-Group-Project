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
        Font uiFont = new Font(FontFamily.GenericSansSerif, 15);

        HUDData data;
        PlayerData playerData;

        public HUD() : base(1366, 768, false) //size is the same as the game window
        {
            data = ((MyGame)game).hudData;
            playerData = ((MyGame)game).playerData;
        }

        public void Start()
        {
            //level = game.FindObjectOfType<Level>();
            player = level.player;
        }
        private void Update()
        {
            graphics.Clear(Color.Empty);
            //Stamina
            graphics.DrawString("Stamina: ", uiFont, Brushes.White, 10, 10);
            graphics.FillRectangle(new SolidBrush(Color.White), 10 + TextWidth("Stamina: "), 10, data.staminaLength * (player.stamina / playerData.stamina), 20);
            graphics.DrawRectangle(new Pen(Color.White), 10 + TextWidth("Stamina: "), 10, data.staminaLength, 20);
            //Score
            graphics.DrawString("Score: " + playerData.playerScore, uiFont, Brushes.White, 10, 35);           
            //Bite CD
            HandleBiteCD();
            //Horn CD
            HandleHornCD();
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
