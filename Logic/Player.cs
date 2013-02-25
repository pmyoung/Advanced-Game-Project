using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//SERVERSIDE
//Contains player data
//Modifies player coordinates based on the keys depressed by the player

namespace SpaceShip.Logic
{
    class Player : GameObject
    {
        public int id;
        int bulletId = 0;
        //IMPLEMENT INVISIBILITY
        int invisible;
        public string name;
        int reload = 0;



        public Player(int id, string name, int spriteId)
        {
            this.id = id;
            this.name = name;
            this.spriteID = spriteId;
        }

        public void accelerate()
        {
            //called whenever the user has 'up' key depressed
            speedX += (int)(Math.Sin(angle) * MatchConfig.accelerationRate);
            speedY += (int)(Math.Cos(angle) * MatchConfig.accelerationRate);
        }
        public void brake()
        {
            //called whenever the user has 'down' key depressed
            speedX = (int)(speedX * MatchConfig.brakeRate);
            speedY = (int)(speedY * MatchConfig.brakeRate);
        }
        public void rotateLeft()
        {
            //called whenever the player has the 'right' key depressed
            angle -= MatchConfig.rotationRate;
        }
        public void rotateRight()
        {
            //called whenever the player has the 'left' key depressed
            angle += MatchConfig.rotationRate;
        }
        public void fire()
        {

            //called whenever the player has the 'space' key depressed
            if (this.reload < 1)
            {
                //If the player isn't currently reloading...
                //give the bullet the player's initial speed, plus the base bullet speed
                float bSpeedX = (float)(this.speedX + Math.Sin(this.angle) * MatchConfig.bulletSpeed);
                float bSpeedY = (float)(this.speedY + Math.Cos(this.angle) * MatchConfig.bulletSpeed);
                //move the bullet to the edge of the player's ship
                float bX = (float)(this.x + Math.Sin(this.angle) * this.radius);
                float bY = (float)(this.y + Math.Cos(this.angle) * this.radius);
                //set the player's reload timer
                this.reload = MatchConfig.reloadRate;
                //create a new bullet object
                new Bullet(bX, bY, bSpeedX, bSpeedY, id, bulletId);
            }


        }
    }
}
