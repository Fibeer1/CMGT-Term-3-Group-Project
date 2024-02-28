using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class Menu : GameObject
    {
        string type;
        public Menu(string pType) : base()
        {
            type = pType;
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
                EasyDraw score = new EasyDraw(300, 50, false);
                Button restartButton;
                Button quitButton;
                gameOverText.TextSize(25);
                gameOverText.TextAlign(CenterMode.Center, CenterMode.Center);
                gameOverText.SetXY(game.width / 2 - gameOverText.width / 2, 50);
                gameOverText.Text("Game over!");
                score.TextAlign(CenterMode.Center, CenterMode.Center);
                score.SetXY(game.width / 2 - score.width / 2, 175);
                score.Text("Score: " + ((MyGame)game).playerData.playerScore);
                restartButton = new Button("Restart", game.width / 2 - 150 / 2, 425);
                quitButton = new Button("Quit Game", game.width / 2 - 150 / 2, 500);
                AddChild(gameOverText);
                AddChild(score);
                AddChild(restartButton);
                AddChild(quitButton);
            }
            else if (type == "Win Screen")
            {
                EasyDraw winText = new EasyDraw(250, 75, false);
                EasyDraw score = new EasyDraw(300, 50, false);
                Button restartButton;
                Button quitButton;
                winText.TextSize(25);
                winText.TextAlign(CenterMode.Center, CenterMode.Center);
                winText.SetXY(game.width / 2 - winText.width / 2, 50);
                winText.Text("You win!");
                score.TextAlign(CenterMode.Center, CenterMode.Center);
                score.SetXY(game.width / 2 - score.width / 2, 175);
                score.Text("Score: " + ((MyGame)game).playerData.playerScore);
                restartButton = new Button("Restart", game.width / 2 - 150 / 2, 425);
                quitButton = new Button("Quit Game", game.width / 2 - 150 / 2, 500);
                AddChild(winText);
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
