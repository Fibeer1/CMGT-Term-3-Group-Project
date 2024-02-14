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
        Level level;
        Font uiFont = new Font(FontFamily.GenericSansSerif, 15);
        float biteTime = 0;
        float biteCD = 0;
        float hornTime = 0;
        float hornCD = 0;
        public HUD() : base(800, 600, false) //size is the same as the game window
        {
            
        }
        public void Start()
        {
            level = parent as Level;
            player = level.player;
        }
        private void Update()
        {
            graphics.Clear(Color.Empty);
            //Health
            graphics.DrawString("Health: " + player.healthPoints, uiFont, Brushes.White, 10, 10);
            //Score
            graphics.DrawString("Score: " + player.score, uiFont, Brushes.White, 10, 35);
            //Stamina
            graphics.DrawString("Stamina: " + player.stamina, uiFont, Brushes.White, 10, 60);
            //Bite CD
            HandleBiteCD();
            //Horn CD
            HandleHornCD();
        }
        private void HandleBiteCD()
        {
            if (player.biteCDTimer > 0)
            {
                if (biteTime <= 0)
                {
                    biteCD = player.biteCD;
                }
                float angle = 0;
                if (angle < 360)
                {
                    angle = biteTime / biteCD * 360;
                    biteTime += 0.0175f;
                }
                float textX = 10;
                float textY = 85;
                string text = "Bite CD: ";
                graphics.DrawString(text, uiFont, Brushes.White, textX, textY);
                graphics.FillPie(new SolidBrush(Color.White), textX + text.Length * uiFont.Size - 55, textY + 5, 15, 15, 0, angle);
            }
            else
            {
                biteTime = 0;
                biteCD = 0;
            }
        }
        private void HandleHornCD()
        {
            if (player.hornCDTimer > 0)
            {
                if (hornTime <= 0)
                {
                    hornCD = player.hornCD;
                }
                float angle = 0;
                if (angle < 360)
                {
                    angle = hornTime / hornCD * 360;
                    hornTime += 0.0175f;
                }
                float textX = 10;
                float textY = 110;
                string text = "Horn CD: ";
                graphics.DrawString(text, uiFont, Brushes.White, textX, textY);
                graphics.FillPie(new SolidBrush(Color.White), textX + text.Length * uiFont.Size - 45, textY + 5, 15, 15, 0, angle);
            }
            else
            {
                hornTime = 0;
                hornCD = 0;
            }
        }
    }
}
