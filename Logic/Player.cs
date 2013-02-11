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
        int id;
        int bulletId = 0;
        int invisible;
        string name;
        int reload = 0;

        

        public Player(int id, string name, int spriteId)
        {
            this.id = id;
            this.name = name;
            setSpriteId(spriteId);
        }

        public int getId()
        {
            return this.id;
        }

        public string getName()
        {
            return this.name;
        }
        
        public void decrementReload()
        {
            //Should be called on each iteration of the main game loop
            this.reload = this.reload-1;
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
              if (this.reload<1){
              //If the player isn't currently reloading...
              //give the bullet the player's initial speed, plus the base bullet speed
              bSpeedX = (float)(this.speedX + Math.Sin(this.angle) * MatchConfig.bulletSpeed);
              bSpeedY = (float)(this.speedY + Math.Cos(this.angle) * MatchConfig.bulletSpeed);
              //move the bullet to the edge of the player's ship
              float bX = (float)(this.x + Math.Sin(this.angle) * this.radius);
              float bY = (float)(this.y + Math.Cos(this.angle) * this.radius);
              //set the player's reload timer
              this.reload = GameScene.getInstance().getConfig().getReloadRate();
              //create a new bullet object
              new Bullet(bX,bY,bSpeedX,bSpeedY, id, bulletId);
          }

        
    }
}
