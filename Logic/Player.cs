using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//SERVERSIDE
//Contains player data
//Modifies player coordinates based on the keys depressed by the player

namespace SpaceShip
{
    class Player:GameObject
    {
        int id;
        int reload = 0;
        string name;
 
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
            reload = reload-1;
        }

         public void accelerate()
         {
             //called whenever the user has 'up' key depressed
             double accelerationRate = GameScene.getInstance().getConfig().getAccelerationRate();
             speedX += (int) (Math.Sin(angle) * accelerationRate);
             speedY += (int) (Math.Cos(angle) * accelerationRate);
          }
          public void brake()
          {
               //called whenever the user has 'down' key depressed
              double brakeRate = GameScene.getInstance().getConfig().getBrakeRate();
              speedX = (int)(speedX * brakeRate);
              speedX = (int)(speedX * brakeRate);
          }
          public void rotateLeft()
          {
               //called whenever the player has the 'right' key depressed
              angle -= GameScene.getInstance().getConfig().getRotationRate();
          }
          public void rotateRight()
          {
               //called whenever the player has the 'left' key depressed
              angle += GameScene.getInstance().getConfig().getRotationRate();
          }
          public void fire()
          {
              //called whenever the player has the 'space' key depressed
              if (this.reload<1){
              //If the player isn't currently reloading...
              Bullet newBullet;
              //give the bullet the player's initial speed, plus the base bullet speed
              newBullet.speedX = this.speedX + Math.Sin(this.angle)*GameScene.getInstance().getConfig().getBulletSpeed();
              newBullet.speedY = this.speedY + Math.Cos(this.angle)*GameScene.getInstance().getConfig().getBulletSpeed();
              //move the bullet to the edge of the player's ship
              newBullet.x = this.x + Math.Sin(this.angle)*this.radius
              newBullet.y = this.y + Math.Cos(this.angle)*this.radius
              //set the player's reload timer
              this.reload = GameScene.getInstance().getConfig().getReloadRate();
              }
          }
    }
}
