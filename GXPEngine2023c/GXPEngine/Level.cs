using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    class Level : GameObject
    {
        public Player player;
        public Sprite spawnPoint;
        Camera camera;
        HUD hud;
        public Level(int index) : base()
        {
            Map levelData = MapParser.ReadMap("Level " + index + ".tmx");
            Console.WriteLine(levelData.Layers.Length);
            SpawnTiles(levelData);
            SpawnObjects(levelData);
            Start();
            camera = new Camera(0, 0, game.width, game.height);
            AddChild(camera);
            player.camera = camera;

            //HUD gets added last
            hud = new HUD();
            hud.level = this;
            camera.AddChild(hud);
            hud.SetXY(camera.x - game.width / 2, camera.y - game.height / 2);
            hud.Start();
        }
        private void Start()
        {            
            //spawn the player, after that spawn enemies at specific places depending on the level
           
            //Need to find a way to add the level as a parent before the start method is called                        
        }
        private void SpawnTiles(Map leveldata)
        {
            if (leveldata.Layers == null || leveldata.Layers.Length == 0)
            {
                return;
            }
            Layer tileLayer = leveldata.Layers[0];
            short[,] tileNumbers = tileLayer.GetTileArray();
            for (int row = 0; row < tileLayer.Height; row++)
            {
                for (int col = 0; col < tileLayer.Width; col++)
                {
                    int tileNumber = tileNumbers[col, row];
                    if (tileNumber > 0)
                    {
                        CollisionTile tile = new CollisionTile("TileSet1.png", 3, 1);
                        tile.SetFrame(tileNumber - 1);
                        tile.x = col * tile.width;
                        tile.y = row * tile.height;
                        AddChild(tile);
                    }
                }
            }
        }
        private void SpawnObjects(Map leveldata)
        {
            if (leveldata.ObjectGroups == null || leveldata.ObjectGroups.Length == 0)
            {
                return;
            }
            ObjectGroup objectGroup = leveldata.ObjectGroups[0];
            if (objectGroup.Objects == null || objectGroup.Objects.Length == 0)
            {
                return;
            }
            foreach (TiledObject obj in objectGroup.Objects)
            {
                switch (obj.Name)
                {
                    case "Player":
                        player = new Player();
                        player.SetXY(obj.X, obj.Y);
                        player.level = this;
                        AddChild(player);
                        break;
                    case "Spawn":
                        spawnPoint = new Sprite("Empty", false, false);
                        spawnPoint.SetXY(obj.X, obj.Y);
                        AddChild(spawnPoint);
                        break;
                    case "Enemy":
                        Enemy enemy = new Enemy();
                        enemy.SetXY(obj.X, obj.Y);
                        AddChild(enemy);
                        enemy.Start();
                        break;
                    default:
                        break;
                }
            }           
        }
    }
}
