using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class Button : EasyDraw
    {
        string text;
        string type;
        MyGame mainGame;
        public Button(string pText, string pType, float pX, float pY) : base(400, 100)
        {
            SetXY(pX, pY);
            text = pText;
            type = pType;
            TextAlign(CenterMode.Center, CenterMode.Min);
            TextFont("Concert One", 20);
            Text(text);
            mainGame = game.FindObjectOfType<MyGame>();
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    if (type == "Start Game")
                    {
                        Menu menu = parent as Menu;
                        menu.DestroyAll();
                        mainGame.StartLevel(mainGame.currentLevelIndex);
                    }
                    else if (type == "Restart")
                    {
                        Menu menu = parent as Menu;
                        menu.DestroyAll();
                        mainGame.playerData = new PlayerData();
                        mainGame.FindObjectOfType<MyGame>().StartLevel(Utils.Random(0, 5));
                    }
                    else if (type == "Main Menu")
                    {
                        Menu menu = parent as Menu;
                        menu.DestroyAll();
                        mainGame.playerData = new PlayerData();
                        mainGame.StartMenu("Main Menu");
                    }
                    //Screens: Main menu, game over
                }
            }
        }
    }
}
