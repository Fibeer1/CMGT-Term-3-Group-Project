using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class Powerup : Sprite
    {
        public string type; //Can be Bite or Horn
        public string powerupName;
        string description;
        EasyDraw nameText = new EasyDraw(175, 25, false);
        EasyDraw descriptionText = new EasyDraw(400, 250, false);
        //if type is Bite, can be Size, Longer Lifetime and Stamina Gain
        //if type is Horn, can be Size, Velocity, Less Stamina Consumption
        public Powerup(string pType, string pPowerupName, float xPos, float yPos) : base("PowerupBox.png")
        {
            SetOrigin(width / 2, height / 2);
            type = pType;
            powerupName = pPowerupName;
            x = xPos;
            y = yPos;
            if (type == "Stat")
            {
                int randomStat = Utils.Random(0, 8);
                if (randomStat == 0)
                {
                    powerupName = "Explosion CD";
                }
                if (randomStat == 1)
                {
                    powerupName = "Ram CD";
                }
                if (randomStat == 2)
                {
                    powerupName = "Speed";
                }
                if (randomStat == 3)
                {
                    powerupName = "Max Speed";
                }
                if (randomStat == 4)
                {
                    powerupName = "Ram Speed";
                }
                if (randomStat == 5)
                {
                    powerupName = "Max HP";
                }
                if (randomStat == 6)
                {
                    powerupName = "Bullet Range";
                }
                if (randomStat == 7)
                {
                    powerupName = "Health Taken";
                }
            }
            //Explosion
            if (powerupName == "Large")
            {
                description = "Bigger explosion size";
            }
            else if (powerupName == "Burst")
            {
                description = "Explosion count x3\n " +
                    "Longer explosion cooldown\n " +
                    "Overwrites previous explosion powerups";
            }
            else if (powerupName == "Shrapnel Release")
            {
                description = "Upon exploding, release a flurry of bullets all around you\n" +
                    "Overwrites previous explosion powerups";
            }
            //Ram
            else if (powerupName == "Boost")
            {
                description = "Charge ram speed faster";
            }
            else if (powerupName == "Claymores")
            {
                description = "Emit an explosion when ramming an enemy\n" +
                    "Explosion is affected by the current explosion powerup\n" +
                    "Overwrites previous ram powerups";
            }
            else if (powerupName == "Rifles")
            {
                description = "Fire rifles when releasing built up speed\n" +
                    "Overwrites previous ram powerups";
            }
            //Bullet
            else if (powerupName == "Bouncy")
            {
                description = "Bullets fired now bounce off the borders of the map";
            }
            else if (powerupName == "Shotgun")
            {
                description = "Bullet count increased by 10 for each fired bullet\n" +
                    "Bullet range decreased\n" +
                    "Bullets have spread\n" +
                    "Overwrites previous bullet powerups\n" +
                    "Does not stack";
            }
            //Stat
            else if (powerupName == "Explosion CD")
            {
                description = "Explosion cooldown decreased";
            }
            else if (powerupName == "Ram CD")
            {
                description = "Ram cooldown decreased";
            }
            else if (powerupName == "Speed")
            {
                description = "Increased acceleration";
            }
            else if (powerupName == "Max Speed")
            {
                description = "Increased max speed";
            }
            else if (powerupName == "Max Ram Speed")
            {
                description = "Increased max ram speed";
            }
            else if (powerupName == "Max HP")
            {
                description = "Increased max health";
            }
            else if (powerupName == "Bullet Range")
            {
                description = "Increased Bullet Range";
            }
            else if (powerupName == "Health Taken")
            {
                description = "Increased health from Health Pickups";
            }
            nameText.TextAlign(CenterMode.Center, CenterMode.Center);
            nameText.Text(powerupName, nameText.x + nameText.width / 2, nameText.y + nameText.height / 2);
            nameText.SetXY(width - nameText.width / 1.5f, height - nameText.height * 3.25f);
            AddChild(nameText);
            descriptionText.TextSize(10);
            descriptionText.TextAlign(CenterMode.Center, CenterMode.Min);
            descriptionText.Text(description, descriptionText.x + descriptionText.width / 2, descriptionText.y + descriptionText.height / 2);
            descriptionText.SetXY(width - descriptionText.width / 1.7f, height - descriptionText.height / 2f);
            AddChild(descriptionText);
        }
    }
}
