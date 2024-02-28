using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class Menu : GameObject
    {
        public int gameOverScore;
        string type;
        public Menu(string pType) : base()
        {
            type = pType;
            if (type == "Game Over")
            {
                gameOverScore = game.FindObjectOfType<Player>().score;
            }
            Start();
        }
        void Start()
        {
            if (type == "Main Menu")
            {
                EasyDraw title = new EasyDraw(400, 150, false);
                EasyDraw controls = new EasyDraw(360, 480, false);
                Button startButton = new Button("Start Game", game.width / 2 - 150 / 2, 250);
                Button quitButton = new Button("Quit Game", game.width / 2 - 150 / 2, 350);
                title.TextAlign(CenterMode.Center, CenterMode.Center);
                title.SetXY(game.width / 2 - title.width / 2, 50);
                title.TextSize(15);
                title.Text("Sugar, Spice \n & \n Everything not \n NICE");
                controls.SetXY(5, 50);
                controls.Text("Controls: \n" +
                "A - move left \n" +
                "D - move right \n" +
                "W - jump \n" +
                "LMB - bite ability \n" +
                "RMB - horn ability \n");
                AddChild(title);
                AddChild(controls);
                AddChild(startButton);
                AddChild(quitButton);
            }
            else if (type == "Game Over")
            {
                EasyDraw gameOverText = new EasyDraw(250, 75, false);
                EasyDraw wave = new EasyDraw(300, 50, false);
                EasyDraw score = new EasyDraw(300, 50, false);
                Button restartButton;
                Button quitButton;
                gameOverText.TextSize(25);
                gameOverText.TextAlign(CenterMode.Center, CenterMode.Center);
                gameOverText.SetXY(game.width / 2 - gameOverText.width / 2, 50);
                gameOverText.Text("Game over!");
                wave.TextAlign(CenterMode.Center, CenterMode.Center);
                wave.SetXY(game.width / 2 - wave.width / 2, 150);
                score.TextAlign(CenterMode.Center, CenterMode.Center);
                score.SetXY(game.width / 2 - score.width / 2, 175);
                score.Text("Score: " + gameOverScore);
                restartButton = new Button("Restart", game.width / 2 - 150 / 2, 425);
                quitButton = new Button("Quit Game", game.width / 2 - 150 / 2, 500);
                AddChild(gameOverText);
                AddChild(wave);
                AddChild(score);
                AddChild(restartButton);
                AddChild(quitButton);
            }
        }
        public void DestroyAll()
        {
            for (int i = 0; i < GetChildCount(); i++)
            {
                GetChildren()[i].LateDestroy();
            }
        }
    }
}
