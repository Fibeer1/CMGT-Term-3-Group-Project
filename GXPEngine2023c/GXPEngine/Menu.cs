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
                Button startButton = new Button("Press the nose to start", "Start Game", game.width / 2 - 400 / 2, 350);
                title.SetXY(game.width / 2 - title.width / 2, 50);
                AddChild(title);
                AddChild(startButton);
            }
            else if (type == "Game Over")
            {
                EasyDraw gameOverText = new EasyDraw(250, 75, false);
                EasyDraw score = new EasyDraw(300, 50, false);
                Button restartButton = new Button("Press the nose to restart", "Restart", game.width / 2 - 400 / 2, 425);
                Button menuButton = new Button(" Press the horn to\nreturn to the menu", "Main Menu", game.width / 2 - 400 / 2, 500);
                gameOverText.TextFont("Concert One", 15);
                gameOverText.TextSize(25);
                gameOverText.TextAlign(CenterMode.Center, CenterMode.Center);
                gameOverText.SetXY(game.width / 2 - gameOverText.width / 2, 50);
                gameOverText.Text("Game over!");
                score.TextFont("Concert One", 15);
                score.TextAlign(CenterMode.Center, CenterMode.Center);
                score.SetXY(game.width / 2 - score.width / 2, 175);
                score.Text("Score: " + ((MyGame)game).playerData.playerScore);
                AddChild(gameOverText);
                AddChild(score);
                AddChild(restartButton);
                AddChild(menuButton);
            }
            else if (type == "Win Screen")
            {
                EasyDraw winText = new EasyDraw(250, 75, false);
                EasyDraw score = new EasyDraw(300, 50, false);
                Button restartButton = new Button("Press the nose to restart", "Restart", game.width / 2 - 400 / 2, 425);
                Button menuButton = new Button(" Press the horn to\nreturn to the menu", "Main Menu", game.width / 2 - 400 / 2, 500);
                winText.TextFont("Concert One", 15);
                winText.TextSize(25);
                winText.TextAlign(CenterMode.Center, CenterMode.Center);
                winText.SetXY(game.width / 2 - winText.width / 2, 50);
                winText.Text("You win!");
                score.TextFont("Concert One", 15);
                score.TextAlign(CenterMode.Center, CenterMode.Center);
                score.SetXY(game.width / 2 - score.width / 2, 175);
                score.Text("Score: " + ((MyGame)game).playerData.playerScore);
                AddChild(winText);
                AddChild(score);
                AddChild(restartButton);
                AddChild(menuButton);
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
