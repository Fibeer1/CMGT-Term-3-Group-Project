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
                Sprite title = new Sprite("title.png", false, false);
                EasyDraw controls = new EasyDraw(360, 480, false);
                Button startButton = new Button("Start Game", game.width / 2 - 150 / 2, 350);
                Button quitButton = new Button("Quit Game", game.width / 2 - 150 / 2, 450);
                title.SetXY(game.width / 2 - title.width / 2, 50);
                controls.TextFont("Concert One", 15);
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
                gameOverText.TextFont("Concert One", 15);
                gameOverText.TextSize(25);
                gameOverText.TextAlign(CenterMode.Center, CenterMode.Center);
                gameOverText.SetXY(game.width / 2 - gameOverText.width / 2, 50);
                gameOverText.Text("Game over!");
                score.TextFont("Concert One", 15);
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
                winText.TextFont("Concert One", 15);
                winText.TextSize(25);
                winText.TextAlign(CenterMode.Center, CenterMode.Center);
                winText.SetXY(game.width / 2 - winText.width / 2, 50);
                winText.Text("You win!");
                score.TextFont("Concert One", 15);
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
